using Pharmacy.Domain.Entities;

namespace Pharmacy.Domain.Interfaces
{
    public interface IInOutRepository : IRepository<InOut>
    {
        Task<IEnumerable<InOut>> GetByMedicineIdAsync(int medicineId);
        Task<InOut> AddTransactionAsync(InOut transaction);
    }
}
