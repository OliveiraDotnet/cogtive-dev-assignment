using Cogtive.App.Data;
using Cogtive.App.Enum;
using Cogtive.App.Interfaces;
using Cogtive.App.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Cogtive.App.Services
{
    internal class SyncService : ISyncService
    {
        private readonly AppDbContext _context;
        private readonly IApiService _apiService;

        public SyncService(AppDbContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        public async Task SyncPendingOperationsAsync()
        {
            var pendings = await _context.PendingOperations.Where(p => p.EntityType == nameof(ProductionData))
                                                           .ToListAsync();

            foreach (var pending in pendings)
            {
                var data = JsonSerializer.Deserialize<ProductionData>(pending.PayloadJson);

                if (pending.OperationType == OperationType.Create)
                    await _apiService.PostProductionDataAsync(data);

                _context.PendingOperations.Remove(pending);
            }

            await _context.SaveChangesAsync();
        }
    }
}
