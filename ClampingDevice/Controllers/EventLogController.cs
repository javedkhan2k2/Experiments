using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;
using ClampingDevice.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClampingDevice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventLogController(IEventLogService eventLogService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EventLogDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EventLogDto>>> GetAllAsync()
    {
        var result = await eventLogService.GetAllAsync();
        if(result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EventLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventLogDto>> GetByIdAsync(int id)
    {
        if (id < 1) return BadRequest(new Error("InvalidId", "Id must be greater than zero"));
        var result = await eventLogService.GetByIdAsync(id);
        if (result.IsFailure)
        {
            if (result.Error.Code == "NotFound")
                return NotFound(result.Error);
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EventLogDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventLogDto>> CreateAsync(CreateEventLogDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await eventLogService.CreateAsync(dto);
        if (result.IsFailure) return BadRequest(result.Error);
        
        var value = result.Value!;
        return CreatedAtAction(nameof(GetByIdAsync), new { id = value.Id }, value);
    }
}
