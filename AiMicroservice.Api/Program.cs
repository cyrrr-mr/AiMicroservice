using FluentValidation;
using FluentValidation.AspNetCore;
using AiMicroservice.Application.Validators;
using AiMicroservice.Infrastructure.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using AiMicroservice.Application.Interfaces;
using AiMicroservice.Application.Services;
using AiMicroservice.Infrastructure.Factories;
using AiMicroservice.Infrastructure.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AIRequestValidator>();
builder.Services.AddHttpClient<OllamaProvider>(client =>
{
    client.BaseAddress = new Uri("http://localhost:11434");
    client.Timeout = TimeSpan.FromSeconds(60);
});
builder.Services.AddHttpClient<OllamaHealthCheck>(client =>
{
    client.BaseAddress = new Uri("http://localhost:11434");
    client.Timeout = TimeSpan.FromSeconds(5);
});

builder.Services.AddHealthChecks()
    .AddCheck<OllamaHealthCheck>("ollama", tags: new[] { "ready" });
builder.Services.AddScoped<IAIProviderFactory, AIProviderFactory>();
builder.Services.AddScoped<AIGenerationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();