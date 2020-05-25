using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfWebApiClient.Models;

namespace WpfWebApiClient
{
    public static class ServiceProvider
    {
        private static HttpClient HttpClient = new HttpClient();

        public static async Task<Student[]> GetStudentGratherThan(float mark)
        {
            string markString = mark.ToString().Replace(',', '.');
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44313/GetStudentGratherThan?mark=" + markString);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(true);
            string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Student[] result = await ParseJsonAsync(jsonResponse);
            requestMessage.Dispose();
            return result;
        }

        public static async Task<Student[]> GetStudentLowerThan(float mark)
        {
            string markString = mark.ToString().Replace(',', '.');
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44313/GetStudentLowerThan?mark=" + markString);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(true);
            string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Student[] result = await ParseJsonAsync(jsonResponse);
            requestMessage.Dispose();
            return result;
        }

        public static async Task<Student[]> GetStudentInRange(float minMark, float maxMark)
        {
            string minMarkString = minMark.ToString().Replace(',', '.');
            string maxMarkString = maxMark.ToString().Replace(',', '.');
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44313/GetStudentInRange");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = new StringContent("{ Item1: " + minMarkString + ", Item2: " + maxMarkString + " }");
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(true);
            string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Student[] result = await ParseJsonAsync(jsonResponse);
            requestMessage.Dispose();
            return result;
        }
        
        private static async Task<Student[]> ParseJsonAsync(string json)
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<Student[]>(json);
            }).ConfigureAwait(true);
        }

    }
}
