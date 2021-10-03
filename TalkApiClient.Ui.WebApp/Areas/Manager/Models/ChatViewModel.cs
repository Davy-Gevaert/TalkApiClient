using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkApiClient.Model;

namespace TalkApiClient.Ui.WebApp.Areas.Manager.Models
{
    public class ChatViewModel
    {
        public IList<ChatChannelResponse> MyChannels { get; set; }

        public IList<ChatMessageResponse> GetMessagesFromSelectedChannel { get; set; }
        public string SelectedChannel { set; get; }
    }
}
