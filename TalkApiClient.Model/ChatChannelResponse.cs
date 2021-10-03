using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkApiClient.Model
{
    public class ChatChannelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
