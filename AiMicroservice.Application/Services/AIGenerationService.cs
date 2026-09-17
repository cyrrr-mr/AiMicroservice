using AiMicroservice.Application.Interfaces;
using AiMicroservice.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AiMicroservice.Application.Services;

public class AIGenerationService
{
    private readonly IAIProviderFactory _providerFactory;
    private readonly ILogger<AIGenerationService> _logger;

    public AIGenerationService(IAIProviderFactory providerFactory, ILogger<AIGenerationService> logger)
    {
        _providerFactory = providerFactory;
        _logger = logger;
    }

    public async Task<AIResponse> GenerateAsync(AIRequest request)
    {
        _logger.LogInformation(
            "Requête reçue: {RequestId} pour capability {Capability} avec {MessageCount} message(s)",
            request.RequestId, request.Capability, request.Messages.Count);

        var provider = _providerFactory.GetProvider(request.Capability);
        var response = await provider.GenerateAsync(request);

        if (response.Status == "success")
        {
            _logger.LogInformation(
                "Requête {RequestId} traitée avec succès en {ProcessingTimeMs}ms (modèle: {Model}, tokens: {TotalTokens})",
                request.RequestId, response.ProcessingTimeMs, response.Model, response.Usage?.TotalTokens);
        }
        else
        {
            _logger.LogWarning(
                "Requête {RequestId} en échec: {ErrorCode} - {ErrorMessage}",
                request.RequestId, response.Error?.Code, response.Error?.Message);
        }

        return response;
    }
}