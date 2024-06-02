using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class DeletePassFromTravelAPI : ApiService
    {
        public async Task<bool> DeletePass(Passenger ps)
        {
            try
            {

                // Преобразуем объект в JSON с использованием Newtonsoft.Json
                string json = JsonConvert.SerializeObject(ps);
                Console.WriteLine("JSON: " + json);  // Логирование JSON для отладки
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Создаем HttpRequestMessage для DELETE-запроса
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("passenger", UriKind.Relative),
                    Content = content
                };

                // Отправляем запрос
                HttpResponseMessage response = await _httpClient.SendAsync(request);

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
