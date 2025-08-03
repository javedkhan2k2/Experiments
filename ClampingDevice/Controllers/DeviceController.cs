using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;
using ClampingDevice.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClampingDevice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceController(IDeviceService deviceService) : ControllerBase
{
    // region for all get methods
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeviceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAllDevicesAsync()
    {
        var result = await deviceService.GetAllAsync();
        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{serialNumber}")]
    [ProducesResponseType(typeof(DeviceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeviceDto>> GetDeviceBySerialNumberAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest(new Error("InvalidSerialNumber", "The provided serial number is invalid."));

        var result = await deviceService.GetBySerialNumberAsync(serialNumber);
        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{serialNumber}/status")]
    [ProducesResponseType(typeof(DeviceStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeviceStatusDto>> GetDeviceStatusAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest(new Error("InvalidSerialNumber", "The provided serial number is invalid."));

        var result = await deviceService.GetStatusAsync(serialNumber);
        if (result.IsFailure) return NotFound(result.Error);
        return Ok(result.Value);
    }

    [HttpPut("{serialNumber}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(string serialNumber, [FromBody] UpdateDeviceDto dto)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest(new Error("InvalidSerialNumber", "The provided serial number is invalid."));
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await deviceService.UpdateAsync(serialNumber, dto);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpDelete("{serialNumber}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest(new Error("InvalidSerialNumber", "The provided serial number is invalid."));

        var result = await deviceService.DeleteAsync(serialNumber);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPatch("{serialNumber}/toggle-active")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ToggleActiveAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest(new Error("InvalidSerialNumber", "The provided serial number is invalid."));

        var result = await deviceService.ToggleActiveAsync(serialNumber);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeviceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeviceDto>> RegisterDeviceAsync([FromBody] CreateDeviceDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await deviceService.RegisterAsync(dto);
        if (result.IsFailure || result.Value is null) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetDeviceBySerialNumberAsync), new { serialNumber = result.Value.SerialNumber }, result.Value);
    }

}
