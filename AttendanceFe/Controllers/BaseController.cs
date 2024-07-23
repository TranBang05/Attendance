using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceFe.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public async Task<T?> GetData<T>(string url, string? accepttype = null)
        {
            T? result = default(T);
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (responseMessage.Content is not null)
                    result = responseMessage.Content.ReadFromJsonAsync<T>().Result;
                return result;
            }
            return default(T);
        }

        public async Task<HttpStatusCode> PushData<T>(string url, T value, string? accepttype = null)
        {
            HttpClient client = new HttpClient();
            var jsonStr = JsonSerializer.Serialize(value);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            return responseMessage.StatusCode;
        }
    }
}
