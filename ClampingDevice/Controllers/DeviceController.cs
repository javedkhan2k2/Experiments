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
    public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAllDevicesAsync()
    {
        var result = await deviceService.GetAllAsync();
        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{serialNumber}")]
    public async Task<ActionResult<DeviceDto>> GetDeviceBySerialNumberAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest("Serial number cannot be null or empty.");

        var result = await deviceService.GetBySerialNumberAsync(serialNumber);
        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{serialNumber}/status")]
    public async Task<ActionResult<DeviceDto>> GetDeviceStatusAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest("Serial number cannot be null or empty.");

        var result = await deviceService.GetStatusAsync(serialNumber);
        if (result.IsFailure) return NotFound(result.Error);
        return Ok(result.Value);
    }

    [HttpPut("{serialNumber}")]
    public async Task<IActionResult> UpdateAsync(string serialNumber, [FromBody] UpdateDeviceDto dto)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest("Serial number cannot be null or empty.");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await deviceService.UpdateAsync(serialNumber, dto);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpDelete("{serialNumber}")]
    public async Task<IActionResult> DeleteAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest("Serial number cannot be null or empty.");

        var result = await deviceService.DeleteAsync(serialNumber);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPatch("{serialNumber}/toggle-active")]
    public async Task<IActionResult> ToggleActiveAsync(string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest("Serial number cannot be null or empty.");

        var result = await deviceService.ToggleActiveAsync(serialNumber);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<DeviceDto>> RegisterDeviceAsync([FromBody] CreateDeviceDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await deviceService.RegisterAsync(dto);
        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetDeviceBySerialNumberAsync), new {serialNumber = result.Value.SerialNumber}, result.Value);
    }




}
