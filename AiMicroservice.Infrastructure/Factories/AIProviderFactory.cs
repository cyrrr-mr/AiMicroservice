using AiMicroservice.Application.Interfaces;
using AiMicroservice.Infrastructure.Providers;

namespace AiMicroservice.Infrastructure.Factories;

public class AIProviderFactory : IAIProviderFactory
{
    private readonly OllamaProvider _ollamaProvider;

    public AIProviderFactory(OllamaProvider ollamaProvider)
    {
        _ollamaProvider = ollamaProvider;
    }

    public IAIProvider GetProvider(string capability)
    {
        // Pour l'instant, toutes les capabilities passent par Ollama.
        // Le vrai routing par config viendra au Niveau 5.
        return _ollamaProvider;
    }
}