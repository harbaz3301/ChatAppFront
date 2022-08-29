using Microsoft.AspNetCore.SignalR;

namespace ChatAppFront.Server.hubs
{
    public class chathub : Hub
    {
        private static Dictionary<string, string> Users = new Dictionary<string, string>();
        public override async Task OnConnectedAsync()
        {
            string username = Context.GetHttpContext().Request.Query["username"];
            Users.Add(Context.ConnectionId, username);
            await sendMessagetoChat(string.Empty, $"{username} : joined chat");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await sendMessagetoChat(string.Empty, $"{username} left!");
        }
        public async Task sendMessagetoChat(string user, string message)
        {
            await Clients.All.SendAsync("GetThatMessage",user,message);
        }
    }
}
