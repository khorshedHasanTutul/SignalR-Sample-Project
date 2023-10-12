using Microsoft.AspNetCore.SignalR;

namespace SignalRProject.Hubs
{
    public class UserHub : Hub 
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;
        private readonly Dictionary<string , string> UserConnectionMapping = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            //string userID = Context.User.Identity.Name;
            //string connectionID = Context.ConnectionId;
            //UserConnectionMapping[userID] = connectionID;
            TotalUsers++;
            Clients.All.SendAsync("UpdateTotalUsers" , TotalUsers).GetAwaiter().GetResult();    
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            //string userId = Context.User.Identity.Name;

            //if (UserConnectionMapping.ContainsKey(userId))
            //{
            //    UserConnectionMapping.Remove(userId);
            //}

            TotalUsers--;
            Clients.All.SendAsync("UpdateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("UpdateTotalViews",TotalViews);
        }

        public async void SendMessageToUser(string userId , string message)
        {
            if (UserConnectionMapping.ContainsKey(userId))
            {
                string connectionId = UserConnectionMapping[userId];
                await Clients.Client(connectionId).SendAsync("UpdateTotalViews" , message);
            }
        }

    }
}
