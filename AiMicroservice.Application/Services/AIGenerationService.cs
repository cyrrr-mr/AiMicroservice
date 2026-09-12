using AiMicroservice.Application.Interfaces;
using AiMicroservice.Domain.Models;

namespace AiMicroservice.Application.Services;

public class AIGenerationService
{
    private readonly IAIProviderFactory _providerFactory;

    public AIGenerationService(IAIProviderFactory providerFactory)
    {
        _providerFactory = providerFactory;
    }

    public async Task<AIResponse> GenerateAsync(AIRequest request)
    {
        var provider = _providerFactory.GetProvider(request.Capability);
        return await provider.GenerateAsync(request);
    }
}