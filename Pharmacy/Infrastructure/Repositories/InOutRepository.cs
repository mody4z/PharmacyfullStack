using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Interfaces;

namespace Pharmacy.Infrastructure.Repositories
{
    public class InOutRepository : Repository<InOut>, IInOutRepository
    {
        public InOutRepository(PharmacyDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<InOut>> GetAllAsync()
        {
            return await _dbSet
                .Include(io => io.Medicine)
                .Include(io => io.Employee)
                .Include(io => io.Client)
                .ToListAsync();
        }

        public override async Task<InOut?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(io => io.Medicine)
                .Include(io => io.Employee)
                .Include(io => io.Client)
                .FirstOrDefaultAsync(io => io.Id == id);
        }

        public async Task<IEnumerable<InOut>> GetByMedicineIdAsync(int medicineId)
        {
            return await _dbSet
                .Include(io => io.Medicine)
                .Include(io => io.Employee)
                .Include(io => io.Client)
                .Where(io => io.MedicineId == medicineId)
                .ToListAsync();
        }

        public async Task<InOut> AddTransactionAsync(InOut transaction)
        {
            await _dbSet.AddAsync(transaction);
            await _context.SaveChangesAsync();
            
            // Reload with related entities
            return await GetByIdAsync(transaction.Id) ?? transaction;
        }
    }
}
