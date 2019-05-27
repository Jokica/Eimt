using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Emit.Web.Hubs
{
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {

        }
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("Login", "Test");
        }

    }
}
