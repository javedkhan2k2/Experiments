using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;
using ClampingDevice.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClampingDevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClampingController(IClampingDataService clampingDataService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ClampingDataDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClampingDataDto>>> GetAllAsync()
        {
            var result = await clampingDataService.GetAllAsync();
            if(result.IsFailure) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpGet("{id:int}", Name = "GetClampingDataByIdAsync")]
        [ProducesResponseType(typeof(ClampingDataDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClampingDataDto>> GetClampingDataByIdAsync(int id)
        {
            if(id < 1) return BadRequest(new Error("InvalidId", "The provided ID is invalid."));

            var result = await clampingDataService.GetByIdAsync(id);
            if (result.IsFailure)
            {
                if (result.Error.Code == "NotFound")
                    return NotFound(result.Error);
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClampingDataDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClampingDataDto>> CreateAsync(CreateClampingDataDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await clampingDataService.CreateAsync(dto);
            if (result.IsFailure) return BadRequest(result.Error);
            
            var value = result.Value!;
            return CreatedAtRoute("GetClampingDataByIdAsync", new {id = value.Id}, value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1) return BadRequest(new Error("InvalidId", "The provided ID is invalid."));
            var result = await clampingDataService.DeleteAsync(id);
            if (result.IsFailure) return BadRequest(result.Error);
            return NoContent();
        }

        [HttpGet("device/{serialNumber}")]
        [ProducesResponseType(typeof(IEnumerable<ClampingDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClampingDataDto>>> GetByDeviceSerialAsync(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber)) return BadRequest(new Error("InvalidSerialNumber", "The provided serial number is invalid."));

            var result = await clampingDataService.GetByDeviceSerialAsync(serialNumber);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.Value);
        }

    }
}
