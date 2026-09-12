using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AiMicroservice.Infrastructure.HealthChecks;

public class OllamaHealthCheck : IHealthCheck
{
    private readonly HttpClient _httpClient;

    public OllamaHealthCheck(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync("/", cancellationToken);

            return response.IsSuccessStatusCode
                ? HealthCheckResult.Healthy("Ollama est joignable")
                : HealthCheckResult.Unhealthy($"Ollama a répondu avec le code {response.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Impossible de contacter Ollama", ex);
        }
    }
}