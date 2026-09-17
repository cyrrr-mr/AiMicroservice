using AiMicroservice.Domain.Models;

namespace AiMicroservice.Application.Interfaces;

public interface IPromptService
{
    string BuildPrompt(string capability, AIRequest request);
}