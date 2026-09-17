using AiMicroservice.Application.Interfaces;
using AiMicroservice.Domain.Models;

namespace AiMicroservice.Infrastructure.Prompts;

public class PromptService : IPromptService
{
    // Pour l'instant : templates codés en dur en mémoire.
    // Plus tard, ça pourrait venir d'une base de données (PostgreSQL, comme mentionné dans la spec).
    private readonly List<PromptTemplate> _templates = new()
    {
        new PromptTemplate
        {
            Name = "general-chat-default",
            Version = 1,
            Content = "Tu es un assistant utile et concis. Réponds à la conversation suivante :\n{conversation}",
            RequiredVariables = new List<string> { "conversation" },
            Capability = "general-chat",
            Status = "active"
        }
    };

    public string BuildPrompt(string capability, AIRequest request)
    {
        var template = _templates.FirstOrDefault(t => t.Capability == capability && t.Status == "active");

        if (template == null)
        {
            // Pas de template configuré pour cette capability : on retombe sur un format brut simple.
            return string.Join("\n", request.Messages.Select(m => $"{m.Role}: {m.Content}"));
        }

        var conversation = string.Join("\n", request.Messages.Select(m => $"{m.Role}: {m.Content}"));
        return template.Content.Replace("{conversation}", conversation);
    }
}