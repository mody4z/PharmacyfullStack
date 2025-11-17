using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Services;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InOutsController : ControllerBase
    {
        private readonly IInOutService _inOutService;

        public InOutsController(IInOutService inOutService)
        {
            _inOutService = inOutService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InOutDto>>> GetInOuts()
        {
            var inOuts = await _inOutService.GetAllAsync();
            return Ok(inOuts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InOutDto>> GetInOut(int id)
        {
            var inOut = await _inOutService.GetByIdAsync(id);
            if (inOut == null)
                return NotFound();

            return Ok(inOut);
        }

        [HttpGet("Medicine/{medicineId}")]
        public async Task<ActionResult<IEnumerable<InOutDto>>> GetInOutsByMedicine(int medicineId)
        {
            var inOuts = await _inOutService.GetByMedicineIdAsync(medicineId);
            return Ok(inOuts);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<InOutDto>> PostInOut(CreateInOutDto dto)
        {
            try
            {
                var inOut = await _inOutService.CreateTransactionAsync(dto);
                return CreatedAtAction(nameof(GetInOut), new { id = inOut.Id }, inOut);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInOut(int id)
        {
            await _inOutService.DeleteAsync(id);
            return NoContent();
        }
    }
}
