using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Threading.Tasks;
namespace cms
{
    public class ync : Hub
    {
        private readonly TimeSpan BroadcastInterval =
            TimeSpan.FromMilliseconds(2000);        
        private Timer _broadcastLoop;
        private bool isRunning = false;
        
        public async Task JoinRoom(string roomName, string user_id,string user_name, string contents, string price)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).addChatMessage(roomName,user_id, user_name, contents, price);
        }
        public async Task broadCast(string roomName, string username, string contents, string price)
        {
            await Groups.Add(Context.ConnectionId, roomName);            
            Clients.Group(roomName).broadCast(roomName,username, contents, price);
        }
        public async Task noticeNews(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).noticeNews(roomName);
        }
        public async Task LeaveRoom(string roomName)
        {
            await Groups.Remove(Context.ConnectionId, roomName);
        }
        public async Task LeaveRoomComment(string token)
        {
            await Groups.Remove(Context.ConnectionId, token);
        }
        
    }
}