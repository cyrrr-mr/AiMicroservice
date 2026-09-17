namespace AiMicroservice.Domain.Models;

public class PromptTemplate
{
    public string Name { get; set; } = string.Empty;
    public int Version { get; set; }
    public string Content { get; set; } = string.Empty;
    public List<string> RequiredVariables { get; set; } = new();
    public string Capability { get; set; } = string.Empty;
    public string Status { get; set; } = "active"; // "active", "deprecated", "draft"
}