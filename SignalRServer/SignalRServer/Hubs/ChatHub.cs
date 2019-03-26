using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRServer.Hubs
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public void SendMessage(string sender, string message)
        {
            if (DatabaseHelpers.FindPlayer(sender) != null)
            {
                Clients.Others.ReceiveMessage(sender, message);

                DatabaseHelpers.AddPlayerMessage(new Models.PlayerMessage()
                {
                    Username = sender,
                    MessageSent = message,
                    DateSent = DateTime.UtcNow
                });
            }
        }

        public void Join(string sender)
        {
            if (DatabaseHelpers.FindPlayer(sender) != null)
                Clients.Others.PlayerJoined(sender);
        }

        public void Leave(string sender)
        {
            if (DatabaseHelpers.FindPlayer(sender) != null)
                Clients.Others.PlayerLeft(sender);
        }
    }
}