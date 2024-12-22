using System.Net.Http.Json;

namespace SoftverLogistikaFront.Services
{
    public class AuthService
    {

        private readonly HttpClient _httpClient;

        public bool IsLoggedIn { get; private set; } = false;

        public event Action? OnChange;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ToggleLogin()
        {
            var response = await _httpClient.PostAsync("/login", null);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                IsLoggedIn = result?.IsLoggedIn ?? false;
                NotifyStateChanged();
            }
        }

        public async Task CheckLoginStatus()
        {
            var result = await _httpClient.GetFromJsonAsync<AuthResponse>("/login-status");
            IsLoggedIn = result?.IsLoggedIn ?? false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        private class AuthResponse
        {
            public bool IsLoggedIn { get; set; }
        }
    }
}
