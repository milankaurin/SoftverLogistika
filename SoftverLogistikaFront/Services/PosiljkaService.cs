using System.Net.Http.Json;
using DeljeniPodaci;

namespace SoftverLogistikaFront.Services
{
    public class PosiljkaService
    {
        private readonly HttpClient _http;

        public PosiljkaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Posiljka>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Posiljka>>("posiljke"); 
        }

        public async Task<Posiljka?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<Posiljka>($"posiljke/{id}");
        }

        public async Task<bool> CreateAsync(Posiljka posiljka)
        {
            var response = await _http.PostAsJsonAsync("posiljke", posiljka); 
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Guid id, Posiljka posiljka)
        {
            var response = await _http.PutAsJsonAsync($"posiljke/{id}", posiljka); 
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"posiljke/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
