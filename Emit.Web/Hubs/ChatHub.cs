using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emit.Web.Hubs
{
    public class ChatHub:Hub
    {
        public ChatHub()
        {

        }
        public async Task SendMessage(string Message,string User)
        {
            await Clients.Others.SendAsync("chatMessage", new { message = Message, user = User });
        }
    }
}
