namespace AiMicroservice.Domain.Models;

public class AIResponse
{
    public string RequestId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // "success" ou "error"
    public string? Model { get; set; }
    public string? Content { get; set; }
    public AIUsage? Usage { get; set; }
    public long ProcessingTimeMs { get; set; }
    public AIError? Error { get; set; }
}

public class AIUsage
{
    public int InputTokens { get; set; }
    public int OutputTokens { get; set; }
    public int TotalTokens { get; set; }
}

public class AIError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}