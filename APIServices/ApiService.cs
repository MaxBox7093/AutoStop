using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AutoStop.Services
{
    class ApiService
    {
        protected readonly HttpClient _httpClient;

        public ApiService()
        {
            string baseAddress;

#if ANDROID
            baseAddress = "http://192.168.0.87:5083/api/";
#else
            baseAddress = "http://192.168.0.87:5083/api/";
            #endif

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }
    }
}
