namespace AiMicroservice.Domain.Models;

public class AIRequest
{
    public string RequestId { get; set; } = string.Empty;
    public string Capability { get; set; } = string.Empty;
    public List<AIMessage> Messages { get; set; } = new();
}

public class AIMessage
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}