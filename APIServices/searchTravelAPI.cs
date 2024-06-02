using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class searchTravelAPI : ApiService
    {
        public async Task<List<Travel>> GetTravels(PassengerSearch ps)
        {
            try
            {
                // Формируем строку запроса
                string queryString = $"searchTravel?startCity={Uri.EscapeDataString(ps.startCity)}&endCity={Uri.EscapeDataString(ps.endCity)}&numberPassenger={ps.numberPassenger}&date={ps.date:yyyy-MM-dd}";

                // Создаем запрос
                var request = new HttpRequestMessage(HttpMethod.Get, queryString);

                // Отправляем запрос
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                // Логирование статуса ответа
                Console.WriteLine("Response status code: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа как строку
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Логирование содержимого ответа
                    Console.WriteLine("Response body: " + responseBody);

                    // Десериализация строки JSON в список объектов Travel
                    List<Travel> travels = JsonConvert.DeserializeObject<List<Travel>>(responseBody);

                    return travels;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
