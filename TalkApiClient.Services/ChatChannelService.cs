using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TalkApiClient.Model;
using TalkApiClient.Services.Abstractions;

namespace TalkApiClient.Services
{
    public class ChatChannelService : IChatChannelApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatChannelService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<T>> GetAllAsync<T>(string route)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var result = await httpClient.GetAsync(route);
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadFromJsonAsync<IList<T>>();
        }

        public async Task<T> CreateAsync<T>(T request, string route)    // T request = ChatChannelRequest
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var result = await httpClient.PostAsJsonAsync(route, request);
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsAsync<T>();
        }
    }
}
