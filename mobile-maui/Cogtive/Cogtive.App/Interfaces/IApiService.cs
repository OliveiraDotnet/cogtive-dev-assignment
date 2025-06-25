using Cogtive.App.Models;

namespace Cogtive.App.Interfaces
{
    public interface IApiService
    {
        Task<List<Machine>> GetMachinesAsync();
        Task<List<ProductionData>> GetMachineProductionDataAsync(int machineId);
        Task<ProductionData> PostProductionDataAsync(ProductionData data);
    }
}
