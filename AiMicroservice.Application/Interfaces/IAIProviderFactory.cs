namespace AiMicroservice.Application.Interfaces;

public interface IAIProviderFactory
{
    IAIProvider GetProvider(string capability);
}