using ClampingDevice.Data;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClampingController(AppDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ClampingDataDto>>> GetAllData()
        {
            // Simulate fetching data from a database
            return Ok(await dbContext.ClampingsData.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Clamp(ClampingDataDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Simulate adding clamping data to a database
                await AddClampingDataToDatabase(dto);
                return Ok(new { message = "Clamping data added successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }

        private async Task AddClampingDataToDatabase(ClampingDataDto dto)
        {
            // Simulate adding data to a database
            // In a real application, this would involve database operations
            Console.WriteLine($"Clamping Data Added: Id={dto.DeviceId}, Force={dto.ClampingForceN}, Temperature={dto.TemperatureC}, Timestamp={dto.Timestamp}");
            dto.Timestamp = DateTime.UtcNow; // Set the timestamp to the current time
            dbContext.ClampingsData.Add(new ClampingData
            {
                DeviceId = dto.DeviceId,
                ClampingForceN = dto.ClampingForceN,
                TemperatureC = dto.TemperatureC,
                Timestamp = dto.Timestamp
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
