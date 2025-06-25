using Cogtive.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Cogtive.App.Data.Repository
{
    public class ProductionDataRepository
    {
        private readonly AppDbContext _db;

        public ProductionDataRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProductionData>> GetAllAsync() =>
            await _db.ProductionData.ToListAsync();

        public async Task AddAsync(ProductionData data)
        {
            _db.ProductionData.Add(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ProductionData>> GetNotSyncedAsync() =>
            await _db.ProductionData.Where(p => !p.IsSynced).ToListAsync();
    }
}
