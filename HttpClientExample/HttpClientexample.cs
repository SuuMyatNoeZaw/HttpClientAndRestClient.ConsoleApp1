using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HttpClientExample
{
    internal class HttpClientexample
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string postEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public async Task Read()
        {
            var response = await _client.GetAsync(postEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var JsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JsonStr);
            }
        }
        public async Task Edit(int id)
        {
            var response = await _client.GetAsync($"{postEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await Console.Out.WriteLineAsync("No Data Found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                var JsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JsonStr);
            }
        }

        public async Task Create(int userID,string title,string body)
        {
            PostModel requestModel = new PostModel()
            {
                userId = userID,
                title = title,
                body = body
            };
            var jsonStr=JsonConvert.SerializeObject(requestModel);
            var content=new StringContent(jsonStr,Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync(postEndpoint, content);
            if (response.IsSuccessStatusCode)
            {
                 Console.Out.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task Update(int id,int userID,string title,string body)
        {
            PostModel requestModel = new PostModel()
            {
                id = id,
                userId = userID,
                title = title,
                body = body
            };
            var JsonStr=JsonConvert.SerializeObject(requestModel);
            var content=new StringContent(JsonStr,Encoding.UTF8, Application.Json);
            var response= await _client.PatchAsync(postEndpoint,content);
            if (response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task Delete(int id)
        {
            var response=await _client.DeleteAsync($"{postEndpoint}/{id}");
            if(response.StatusCode==System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
          if (response.IsSuccessStatusCode)
            {
                var jsonStr=await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }
    }
}



public class PostModel
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}
