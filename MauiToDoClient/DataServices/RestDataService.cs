using Android.OS;
using MauiToDoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Google.Crypto.Tink.Proto;

namespace MauiToDoClient.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOption;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5209/" : "http://localhost:5109";
            _url = $"{_baseAddress}/api";

            _jsonSerializeOption = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddToDoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                System.Diagnostics.Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializeOption);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage responce = await _httpClient.PostAsync($"{_url}/todo", content);

                if (responce.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"---> Successfuly created todo...");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("---> Not http 2xx responce...");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"---> Whoops exception {ex.Message}...");
            }

            return;
        }

        public async Task DeleteToDoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                System.Diagnostics.Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                HttpResponseMessage responce = await _httpClient.DeleteAsync($"{_url}/todo/{id}");

                if (responce.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"---> Successfuly deleted todo...");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("---> Not http 2xx responce...");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"---> Whoops exception {ex.Message}...");
            }

            return;
        }

        public async Task<List<ToDo>> GetAllToDosAsync()
        {
            List<ToDo> todos = new List<ToDo>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                System.Diagnostics.Debug.WriteLine("---> No internet access...");
                return todos;
            }

            try
            {
                HttpResponseMessage responce = await _httpClient.GetAsync($"{_url}/todo");

                if (responce.IsSuccessStatusCode)
                {
                    string context = await responce.Content.ReadAsStringAsync();

                    todos = JsonSerializer.Deserialize<List<ToDo>>(context, _jsonSerializeOption);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("---> Not http 2xx responce...");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"---> Whoops exception { ex.Message }...");
            }

            return todos;
        }

        public async Task UpdateToDoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                System.Diagnostics.Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializeOption);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage responce = await _httpClient.PutAsync($"{_url}/todo", content);


                if (responce.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"---> Successfuly updated todo...");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("---> Not http 2xx responce...");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"---> Whoops exception {ex.Message}...");
            }

            return;
        }
    }
}
