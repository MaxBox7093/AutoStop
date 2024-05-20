using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AutoStop.Services
{
    class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            string baseAddress;

            #if ANDROID
            baseAddress = "https://192.168.0.105:5000/api/"; // Специальный IP-адрес для доступа к localhost с эмулятора Android через HTTPS
            #else
            baseAddress = "https://localhost:7082/api/"; // Локальный адрес для Windows эмулятора через HTTPS
            #endif

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }


        public async Task<int> GetUserIdAsync()
        {
            return await _httpClient.GetFromJsonAsync<int>("User");
        }
    }
}
