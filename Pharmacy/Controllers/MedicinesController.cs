using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Services;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetMedicines()
        {
            var medicines = await _medicineService.GetAllAsync();
            return Ok(medicines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDto>> GetMedicine(int id)
        {
            var medicine = await _medicineService.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            return Ok(medicine);
        }

        [HttpGet("LowStock")]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetLowStockMedicines([FromQuery] int threshold = 10)
        {
            var medicines = await _medicineService.GetLowStockAsync(threshold);
            return Ok(medicines);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MedicineDto>> PostMedicine(CreateMedicineDto dto)
        {
            var medicine = await _medicineService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMedicine), new { id = medicine.Id }, medicine);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, UpdateMedicineDto dto)
        {
            try
            {
                await _medicineService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            await _medicineService.DeleteAsync(id);
            return NoContent();
        }
    }
}
