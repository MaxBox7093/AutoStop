using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class GetNameByPhoneNumAPI : ApiService
    {
        public async Task<string> GetName(string ph)
        {
            try
            {
                // Отправляем GET-запрос
                HttpResponseMessage response = await _httpClient.GetAsync($"GetName?phone={ph}");

                // Логирование статуса ответа
                Console.WriteLine("Response status code: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа как строку
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Логирование содержимого ответа
                    Console.WriteLine("Response body: " + responseBody);

                    return responseBody;
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
