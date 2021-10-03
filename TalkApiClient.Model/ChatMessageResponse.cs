using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkApiClient.Model
{
    public class ChatMessageResponse
    {
        public int Id { get; set; }
        public string Author { get; set; }

        public string Message { get; set; }

        public string Channel { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
