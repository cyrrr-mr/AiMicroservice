using AiMicroservice.Application.Interfaces;
using AiMicroservice.Domain.Models;

namespace AiMicroservice.Infrastructure.Providers;

public class MockAIProvider : IAIProvider
{
    public async Task<AIResponse> GenerateAsync(AIRequest request)
    {
        // Simule un petit délai réseau, comme un vrai appel à un provider IA
        await Task.Delay(200);

        var lastMessage = request.Messages.LastOrDefault()?.Content ?? "";

        return new AIResponse
        {
            RequestId = request.RequestId,
            Status = "success",
            Model = "mock-model-v1",
            Content = $"[Réponse simulée] Tu as demandé : \"{lastMessage}\"",
            Usage = new AIUsage
            {
                InputTokens = 10,
                OutputTokens = 15,
                TotalTokens = 25
            },
            ProcessingTimeMs = 200
        };
    }
}