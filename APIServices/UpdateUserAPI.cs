using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class UpdateUserAPI : ApiService
    {
        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                // Преобразуем объект в JSON с использованием Newtonsoft.Json
                string json = JsonConvert.SerializeObject(user);
                Console.WriteLine("JSON: " + json);  // Логирование JSON для отладки
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем PATCH-запрос
                HttpResponseMessage response = await _httpClient.PatchAsync("User", content);

                // Логирование статуса ответа
                Console.WriteLine("Response status code: " + response.StatusCode);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
