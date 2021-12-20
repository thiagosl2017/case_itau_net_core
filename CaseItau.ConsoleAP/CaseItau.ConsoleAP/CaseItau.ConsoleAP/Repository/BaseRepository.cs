using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.ConsoleAP.Repository
{
    public class BaseRepository
    {
        public static string urlApi = "https://localhost:44378/api/";
        public static HttpResponseMessage _responseMessage;

        public async Task<String> PostHttpDataStringAsync(string url, string createdOject)
        {
            try
            {
                var content = new StringContent(createdOject, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    _responseMessage = await client.PostAsync(urlApi + url, content);
                }
                return await _responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<String> PutHttpDataStringAsync(string url, string createdOject)
        {
            try
            {
                var content = new StringContent(createdOject, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    _responseMessage = await client.PutAsync(urlApi + url, content);
                }
                return await _responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<String> DeleteHttpDataStringAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    _responseMessage = await client.DeleteAsync(urlApi + url);
                }
                return await _responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
