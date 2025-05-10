using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HttpClientExample
{
    internal class RestClientExample


    {
        private readonly RestClient _client = new RestClient();
        private readonly string postEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public async Task Read()
        {
            RestRequest request = new RestRequest(postEndpoint,Method.Get);
            var response = await _client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var JsonStr =response.Content!;
                Console.WriteLine(JsonStr);
            }
        }
        public async Task Edit(int id)
        {
            RestRequest request = new RestRequest($"{postEndpoint}/{id}", Method.Get);
            var response = await _client.GetAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await Console.Out.WriteLineAsync("No Data Found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                var JsonStr = response.Content!;
                Console.WriteLine(JsonStr);
            }
        }

        public async Task Create(int userID, string title, string body)
        {
            RestRequest request = new RestRequest(postEndpoint, Method.Post);
            PostModel requestModel = new PostModel()
            {
                userId = userID,
                title = title,
                body = body
            };
           request.AddJsonBody(requestModel);
            var response = await _client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.Out.WriteLine( response.Content!);
            }
        }

        public async Task Update(int id, int userID, string title, string body)
        {
            RestRequest request = new RestRequest($"{postEndpoint}/{id}", Method.Put);
            PostModel requestModel = new PostModel()
            {
                id = id,
                userId = userID,
                title = title,
                body = body
            };
           request.AddJsonBody(requestModel);
            var response = await _client.PatchAsync(request);
            if (response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync( response.Content!);
            }
        }

        public async Task Delete(int id)
        {
            RestRequest request=new RestRequest($"{postEndpoint}/{id}", Method.Delete);

            var response = await _client.DeleteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                var jsonStr =  response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
    }
}








