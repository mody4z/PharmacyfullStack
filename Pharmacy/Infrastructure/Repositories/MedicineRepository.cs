using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Interfaces;

namespace Pharmacy.Infrastructure.Repositories
{
    public class MedicineRepository : Repository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(PharmacyDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Medicine>> GetLowStockAsync(int threshold)
        {
            return await _dbSet
                .Where(m => m.StockQuantity <= threshold)
                .ToListAsync();
        }
    }
}
