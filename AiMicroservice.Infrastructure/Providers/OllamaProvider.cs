using System.Diagnostics;
using System.Text;
using System.Text.Json;
using AiMicroservice.Application.Interfaces;
using AiMicroservice.Domain.Models;

namespace AiMicroservice.Infrastructure.Providers;

public class OllamaProvider : IAIProvider
{
    private readonly HttpClient _httpClient;
    private const string ModelName = "llama3.2";

    public OllamaProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AIResponse> GenerateAsync(AIRequest request)
    {
        var stopwatch = Stopwatch.StartNew();

        // On concatène les messages en un seul prompt simple pour l'instant
        var prompt = string.Join("\n", request.Messages.Select(m => $"{m.Role}: {m.Content}"));

        var ollamaRequest = new OllamaRequest
        {
            Model = ModelName,
            Prompt = prompt,
            Stream = false
        };

        var json = JsonSerializer.Serialize(ollamaRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var httpResponse = await _httpClient.PostAsync("/api/generate", content);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new AIResponse
                {
                    RequestId = request.RequestId,
                    Status = "error",
                    Error = new AIError
                    {
                        Code = "AI_PROVIDER_ERROR",
                        Message = $"Ollama a répondu avec le code {httpResponse.StatusCode}"
                    }
                };
            }

            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            var ollamaResponse = JsonSerializer.Deserialize<OllamaResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            stopwatch.Stop();

            return new AIResponse
            {
                RequestId = request.RequestId,
                Status = "success",
                Model = ollamaResponse?.Model ?? ModelName,
                Content = ollamaResponse?.Response ?? "",
                Usage = new AIUsage
                {
                    InputTokens = ollamaResponse?.PromptEvalCount ?? 0,
                    OutputTokens = ollamaResponse?.EvalCount ?? 0,
                    TotalTokens = (ollamaResponse?.PromptEvalCount ?? 0) + (ollamaResponse?.EvalCount ?? 0)
                },
                ProcessingTimeMs = stopwatch.ElapsedMilliseconds
            };
        }
        catch (HttpRequestException ex)
        {
            return new AIResponse
            {
                RequestId = request.RequestId,
                Status = "error",
                Error = new AIError
                {
                    Code = "AI_PROVIDER_UNAVAILABLE",
                    Message = $"Impossible de contacter Ollama : {ex.Message}"
                }
            };
        }
    }
}