using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TalkApiClient.Model;
using TalkApiClient.Services;

namespace TalkApiClient.Ui.WebApp.Areas.Manager.Controllers
{
    public class ChatController : ManagerBaseController
    {
        private readonly ChatChannelService _chatChannelService;

        public ChatController(ChatChannelService chatChannelService)
        {
            _chatChannelService = chatChannelService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListChatChannels()
        {
            var channels = await _chatChannelService.GetAllAsync<ChatChannelResponse>("/api/chat-channels");

            return View(channels);
        }

        [HttpGet]
        public IActionResult CreateChatChannel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChatChannel(ChatChannelRequest value)
        {
            await _chatChannelService.CreateAsync(value, "/api/chat-channels");

            return RedirectToAction("ListChatChannels");
        }

        public async Task<IActionResult> ListChatMessages()
        {
            var messages = await _chatChannelService.GetAllAsync<ChatMessageResponse>("/api/chat-messages");

            return View(messages);
        }

        public IActionResult CreateChatMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChatMessage(ChatMessageRequest value)
        {
            await _chatChannelService.CreateAsync(value, "/api/chat-messages");

            return RedirectToAction("ListChatMessages");
        }
    }
}
