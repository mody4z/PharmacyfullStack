using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Interfaces;

namespace Pharmacy.Application.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;

        public MedicineService(IMedicineRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MedicineDto>> GetAllAsync()
        {
            var medicines = await _repository.GetAllAsync();
            return medicines.Select(m => new MedicineDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Category = m.Category,
                Price = m.Price,
                StockQuantity = m.StockQuantity,
                ExpiryDate = m.ExpiryDate,
                Manufacturer = m.Manufacturer
            });
        }

        public async Task<MedicineDto?> GetByIdAsync(int id)
        {
            var medicine = await _repository.GetByIdAsync(id);
            if (medicine == null) return null;

            return new MedicineDto
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                Category = medicine.Category,
                Price = medicine.Price,
                StockQuantity = medicine.StockQuantity,
                ExpiryDate = medicine.ExpiryDate,
                Manufacturer = medicine.Manufacturer
            };
        }

        public async Task<IEnumerable<MedicineDto>> GetLowStockAsync(int threshold)
        {
            var medicines = await _repository.GetLowStockAsync(threshold);
            return medicines.Select(m => new MedicineDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Category = m.Category,
                Price = m.Price,
                StockQuantity = m.StockQuantity,
                ExpiryDate = m.ExpiryDate,
                Manufacturer = m.Manufacturer
            });
        }

        public async Task<MedicineDto> CreateAsync(CreateMedicineDto dto)
        {
            var medicine = new Medicine
            {
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ExpiryDate = dto.ExpiryDate,
                Manufacturer = dto.Manufacturer
            };

            var created = await _repository.AddAsync(medicine);

            return new MedicineDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Category = created.Category,
                Price = created.Price,
                StockQuantity = created.StockQuantity,
                ExpiryDate = created.ExpiryDate,
                Manufacturer = created.Manufacturer
            };
        }

        public async Task UpdateAsync(int id, UpdateMedicineDto dto)
        {
            var medicine = await _repository.GetByIdAsync(id);
            if (medicine == null) throw new KeyNotFoundException($"Medicine with id {id} not found");

            medicine.Name = dto.Name;
            medicine.Description = dto.Description;
            medicine.Category = dto.Category;
            medicine.Price = dto.Price;
            medicine.StockQuantity = dto.StockQuantity;
            medicine.ExpiryDate = dto.ExpiryDate;
            medicine.Manufacturer = dto.Manufacturer;

            await _repository.UpdateAsync(medicine);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
