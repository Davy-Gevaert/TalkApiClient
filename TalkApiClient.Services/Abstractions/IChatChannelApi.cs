using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TalkApiClient.Model;

namespace TalkApiClient.Services.Abstractions
{
    public interface IChatChannelApi
    {
        Task<IList<T>> GetAllAsync<T>(string route);
        Task<T> CreateAsync<T>(T request, string route);    // T request = ChatChannelRequest
    }
}
