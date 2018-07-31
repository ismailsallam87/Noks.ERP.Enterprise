using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
namespace Nox_OTS
{
    public class TaskStatusHub : Hub
    {
        //[HubMethodName("broadcastData")]          
        public static void CurrentTasksStatusOnDay()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TaskStatusHub>();
            context.Clients.All.TasksThisDay();
        }
    }
}