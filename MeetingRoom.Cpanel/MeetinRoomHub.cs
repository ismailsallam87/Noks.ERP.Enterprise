using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MeetingRoom.Cpanel.Hubs
{
    public class MeetinRoomHub : Hub
    {
        //[HubMethodName("broadcastData")]          
        public static void BroadcastData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MeetinRoomHub>();
            context.Clients.All.ReservationsOnCurrentDay();
        }
    }
}