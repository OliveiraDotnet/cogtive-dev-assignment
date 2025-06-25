using Cogtive.App.Data;
using Cogtive.App.Interfaces;
using Cogtive.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Cogtive.App.Services
{
    public class LocalDataService : ILocalDataService
    {
        private readonly AppDbContext _context;

        public LocalDataService(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<List<Machine>> GetMachinesAsync()
        {
            return await _context.Machines.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task SaveMachinesAsync(IEnumerable<Machine> machines)
        {
            foreach (var machine in machines)
            {
                var existing = await _context.Machines.FirstOrDefaultAsync(m => m.Id == machine.Id);
                if (existing == null)
                {
                    await _context.Machines.AddAsync(machine);
                }
                else
                {
                    existing.Name = machine.Name;
                    existing.SerialNumber = machine.SerialNumber;
                    existing.Type = machine.Type;
                    existing.IsActive = machine.IsActive;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveProductionDataAsync(ProductionData data)
        {
            await _context.ProductionData.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductionData>> GetUnsyncedProductionDataAsync()
        {
            return await _context.ProductionData.Where(d => !d.IsSynced).ToListAsync();
        }

        public async Task MarkProductionDataAsSyncedAsync(int id)
        {
            var record = await _context.ProductionData.FindAsync(id);
            if (record != null)
            {
                record.IsSynced = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
