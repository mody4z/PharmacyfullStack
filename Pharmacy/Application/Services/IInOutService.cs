using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Services
{
    public interface IInOutService
    {
        Task<IEnumerable<InOutDto>> GetAllAsync();
        Task<InOutDto?> GetByIdAsync(int id);
        Task<IEnumerable<InOutDto>> GetByMedicineIdAsync(int medicineId);
        Task<InOutDto> CreateTransactionAsync(CreateInOutDto dto);
        Task DeleteAsync(int id);
    }
}
