using Cogtive.App.Models;

namespace Cogtive.App.Interfaces
{
    public interface ILocalDataService
    {
        Task<List<Machine>> GetMachinesAsync();
        Task SaveMachinesAsync(IEnumerable<Machine> machines);
        Task<List<ProductionData>> GetUnsyncedProductionDataAsync();
        Task SaveProductionDataAsync(ProductionData data);
        Task MarkProductionDataAsSyncedAsync(int id);
    }
}
