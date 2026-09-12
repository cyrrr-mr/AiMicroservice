using AiMicroservice.Domain.Models;

namespace AiMicroservice.Application.Interfaces;

public interface IAIProvider
{
    Task<AIResponse> GenerateAsync(AIRequest request);
}