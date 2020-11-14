using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Persistence;
using Application.Feature.RecipientAddress.Commands.CreateRecipientAddress;
using MediatR;
using Newtonsoft.Json;

namespace Application.Feature.RecipientAddress.Commands.CreateByFile
{
    public class RecordAnaliseJob
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public RecordAnaliseJob(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        private const int Pools = 10;
        private const string RequestUri = @"https://address.pochta.ru/validate/api/v7_1";

        public async Task PushRow(string json)
        {
            //ОТПРАВКА ЗАПРОСА НА API 
            // USING HTTP
            // await Task.Delay(1000);
            // Console.WriteLine("JOB DONE");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("AuthCode", "53fb9daa-7f06-481f-aad6-c6a7a58ec0bb");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            Console.WriteLine(json);

            var response =
                await client.PostAsync(RequestUri, new StringContent(json));

            Console.WriteLine(response);
            // await _mediator.Send(new CreateRecipientAddressCommand {Address = row.Address});
        }

        public async Task GenerateRows(IEnumerable<ApiRequest> requests, string currentUserId)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("AuthCode", "53fb9daa-7f06-481f-aad6-c6a7a58ec0bb");
            
            Console.WriteLine("SEND REQUEST TO API");
            
            var tasks = requests.Select(apiRequest => GetResponseAsync(apiRequest, client)).ToList();

            var resultTask = (await Task.WhenAll(tasks));
            
            await _mediator.Send(new CreateRecipientAddressCommand
                {Addresses = resultTask, CurrentUserId = currentUserId});
        }

        private static async Task<ApiResponse> GetResponseAsync(ApiRequest apiRequest, HttpClient client)
        {
            var json = JsonConvert.SerializeObject(apiRequest);
            var response = await client.PostAsync(RequestUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse>(content);
            return deserializeObject;
        }

        public async Task PushRows(IList<ApiRequest> requests, string currentUserId)
        {
            Console.WriteLine("adaw");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("AuthCode", "53fb9daa-7f06-481f-aad6-c6a7a58ec0bb");

            var tasks = new List<Task<IList<ApiResponse>>>();
            for (var i = 0; i < requests.Count / Pools + 1; i++)
            {
                tasks.Add(GetApiResponse(requests.Skip(i * Pools).Take(Pools), client));
                Console.WriteLine("adaw");
            }

            var resultTask = (await Task.WhenAll(tasks)).SelectMany(list => list).ToList();

            await _mediator.Send(new CreateRecipientAddressCommand
                {Addresses = resultTask, CurrentUserId = currentUserId});
        }

        private static async Task<IList<ApiResponse>> GetApiResponse(
            IEnumerable<ApiRequest> requests,
            HttpClient client)
        {
            var resultList = new List<ApiResponse>();
            foreach (var apiRequest in requests)
            {
                var json = JsonConvert.SerializeObject(apiRequest);
                var response = await client.PostAsync(RequestUri,
                    new StringContent(json, Encoding.UTF8, "application/json"));

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("SEND REQUEST TO API");
                var deserializeObject = JsonConvert.DeserializeObject<ApiResponse>(content);
                resultList.Add(deserializeObject);
            }

            return resultList;
        }
    }
}