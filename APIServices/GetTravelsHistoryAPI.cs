﻿using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class GetTravelsHistoryAPI : ApiService
    {
        public async Task<List<Travel>> Get(string ph, bool isDriver)
        {
            if (isDriver)
            {
                try
                {
                    // Отправляем GET-запрос
                    HttpResponseMessage response = await _httpClient.GetAsync($"Travel?phoneDriver={ph}");

                    // Логирование статуса ответа
                    Console.WriteLine("Response status code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        // Читаем содержимое ответа как строку
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Логирование содержимого ответа
                        Console.WriteLine("Response body: " + responseBody);

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
            } else
            {
                try
                {
                    // Отправляем GET-запрос
                    HttpResponseMessage response = await _httpClient.GetAsync($"Travel/passenger?phonePassenger={ph}");

                    // Логирование статуса ответа
                    Console.WriteLine("Response status code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        // Читаем содержимое ответа как строку
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Логирование содержимого ответа
                        Console.WriteLine("Response body: " + responseBody);

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
}
