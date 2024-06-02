using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoStop.Services;
using AutoStop.Models;

namespace AutoStop.APIServices
{
    internal class SendMessageAPI : ApiService
    {
        public async Task<bool> Send(Message mess)
        {
            try
            {
                // Преобразуем объект в JSON с использованием Newtonsoft.Json
                string json = JsonConvert.SerializeObject(mess);
                Console.WriteLine("JSON: " + json);  // Логирование JSON для отладки
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем POST-запрос
                HttpResponseMessage response = await _httpClient.PostAsync("message", content);

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
