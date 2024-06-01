using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class GetMessagesAPI : ApiService
    {
        public async Task<List<Message>> GetMessages(int idChat)
        {
            try
            {
                // Отправляем GET-запрос
                HttpResponseMessage response = await _httpClient.GetAsync($"message?idChat={idChat}");

                // Логирование статуса ответа
                Console.WriteLine("Response status code: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа как строку
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Логирование содержимого ответа
                    Console.WriteLine("Response body: " + responseBody);

                    // Десериализация строки JSON в список объектов Car
                    List<Message> messes = JsonConvert.DeserializeObject<List<Message>>(responseBody);

                    return messes;
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
