using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class TravelDeleteAPI : ApiService
    {
        public async Task<bool> Delete(Travel travel)
        {
            try
            {
                // Отправляем GET-запрос
                HttpResponseMessage response = await _httpClient.DeleteAsync($"Travel?idTravel={travel.idTravel}");

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
