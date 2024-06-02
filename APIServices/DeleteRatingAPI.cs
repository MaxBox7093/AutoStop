using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class DeleteRatingAPI : ApiService
    {
        public async Task<bool> Delete(int idRating)
        {
            try
            {
                // Отправляем GET-запрос
                HttpResponseMessage response = await _httpClient.DeleteAsync($"rating?idComment={idRating}");

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
