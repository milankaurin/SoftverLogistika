using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace SoftverLogistikaFront.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        private const string AuthTokenKey = "authToken"; // Ključ za token u sessionStorage

        public bool IsLoggedIn { get; private set; } = false;

        public event Action? OnChange;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }


        // Proverava status prijave citanjem tokena iz `sessionStorage`

        public async Task CheckLoginStatusAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", AuthTokenKey);

            if (!string.IsNullOrEmpty(token))
            {
                // Proveri validnost tokena na backendu
                var response = await _httpClient.PostAsJsonAsync("/validate-token", new { Token = token });

                if (response.IsSuccessStatusCode)
                {
                    IsLoggedIn = true;
                }
                else
                {
                    // Ako je token nevažeći, obriši ga iz sessionStorage
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", AuthTokenKey);
                    IsLoggedIn = false;
                }
            }
            else
            {
                IsLoggedIn = false;
            }

            NotifyStateChanged();
        }


        // Prijava korisnika - čuvanje tokena u `sessionStorage`

        public async Task<bool> LoginAsync()
        {
            var response = await _httpClient.PostAsync("/login", null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (result != null && !string.IsNullOrEmpty(result.Token))
                {
                    // Čuvanje tokena u sessionStorage
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", AuthTokenKey, result.Token);
                    IsLoggedIn = true;
                    NotifyStateChanged();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Odjava korisnika - brisanje tokena iz `sessionStorage` i slanje zahteva serveru
        /// </summary>
        public async Task LogoutAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", AuthTokenKey);

            if (!string.IsNullOrEmpty(token) && Guid.TryParse(token, out var parsedToken))
            {
                var response = await _httpClient.PostAsJsonAsync("/logout", parsedToken);

                if (response.IsSuccessStatusCode)
                {
                    // Brisanje tokena iz sessionStorage
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", AuthTokenKey);
                    IsLoggedIn = false;
                    NotifyStateChanged();
                }
                else
                {
                    Console.WriteLine("Neispravan token za odjavu.");
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        private class AuthResponse
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}
