using Microsoft.AspNetCore.SignalR;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Helpers;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Hubs
{
    
    public class ChatHub:Hub
    {
        private readonly ApplicationDbContext _context;
        public ICurrentUserService _CurrentUserService { get; }

        public ChatHub(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _CurrentUserService = currentUserService;
        }


        public async Task SendMessage (string receivedId, string message)
        {
            var NowData = DateTime.Now;
            var date = NowData.ToShortDateString();
            var time = NowData.ToShortTimeString();

            
            string senderId = _CurrentUserService.UserId;

            var messageToAdd = new Message()
            {
                Text = message,
                Date = NowData,
                SenderId = senderId,
                ReceiverId = receivedId,
            };

            await _context.AddAsync(messageToAdd);
            await _context.SaveChangesAsync();

            
            List<string> users = new List<string>()
            {
                receivedId,senderId
            };

            
            await Clients.Users(users).SendAsync("ReceivedMessage", message, date, time, senderId);
        }
       
    }
}
