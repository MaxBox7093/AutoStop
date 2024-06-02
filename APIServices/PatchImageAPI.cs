using AutoStop.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoStop.APIServices
{
    internal class PatchImageAPI : ApiService
    {
        public async Task<bool> UpdateImageAsync(string phone, Stream imageStream)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                content.Add(new StreamContent(imageStream), "image", "image.jpg");
                content.Add(new StringContent(phone), "phone");

                System.Net.Http.HttpResponseMessage response = await _httpClient.PatchAsync($"img?phone={phone}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public async Task<Stream> GetImageAsync(string phone)
        {
            try
            {
                System.Net.Http.HttpResponseMessage response = await _httpClient.GetAsync($"img/{phone}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }
    }
}
