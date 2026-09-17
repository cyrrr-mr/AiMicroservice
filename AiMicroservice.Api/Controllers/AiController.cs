using AiMicroservice.Application.Services;
using AiMicroservice.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AiMicroservice.Api.Controllers;

[ApiController]
[Route("api/v1/ai")]
[Authorize]
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