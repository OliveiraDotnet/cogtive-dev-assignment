using Cogtive.App.Interfaces;
using Cogtive.App.Models;
using System.Text;
using System.Text.Json;

namespace Cogtive.App.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://10.0.2.2:5211"; 

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Machine>> GetMachinesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiBaseUrl}/api/machines");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Machine>>(content, _jsonOptions);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductionData>> GetMachineProductionDataAsync(int machineId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiBaseUrl}/api/machines/{machineId}/production-data");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ProductionData>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductionData> PostProductionDataAsync(ProductionData data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{ApiBaseUrl}/api/production-data", content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ProductionData>(responseContent, _jsonOptions);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
