using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Wdp.Api
{
    [Authorize]
    public class ChatRoomHub : Hub
    {
        public Task SendPublicMessage(string message)
        {
            //string connId = this.Context.ConnectionId;
            //string msg = $"{connId}{DateTime.Now}:{message}";
            string name = this.Context.User!.FindFirst(ClaimTypes.Name)!.Value;
            string msg = $"{name}{DateTime.Now}:{message}";
            return Clients.All.SendAsync("ReceivePublicMessage", msg);
        }

        public async Task<string> SendPrivateMessage(string userName,string message)
        {
            string name = this.Context.User!.FindFirst(ClaimTypes.Name)!.Value;
            string time = DateTime.Now.ToShortTimeString();
            await this.Clients.User(userName).SendAsync("ReceivePrivateMessage", name, time, message);
            return "OK";
        }
    }
}
