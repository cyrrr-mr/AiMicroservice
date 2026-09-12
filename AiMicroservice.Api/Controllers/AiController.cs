using AiMicroservice.Application.Services;
using AiMicroservice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AiMicroservice.Api.Controllers;

[ApiController]
[Route("api/v1/ai")]
public class AiController : ControllerBase
{
    private readonly AIGenerationService _generationService;

    public AiController(AIGenerationService generationService)
    {
        _generationService = generationService;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<AIResponse>> Generate([FromBody] AIRequest request)
    {
        var response = await _generationService.GenerateAsync(request);
        return Ok(response);
    }
}