using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Helpers;
using Real_Time_Chat_Application.Interface;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.ViewModels.Messages;

namespace Real_Time_Chat_Application.Service
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public MessageService(ApplicationDbContext context , ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

      
        public async Task<ChatViewModel> GetMessages(string selectedUserId)
        {
           
            var currentUserId = _currentUserService.UserId;

            
            var selectedUser = await _context.Users.FirstOrDefaultAsync(i=>i.Id== selectedUserId);
            var selectedUserName = "";
            if (selectedUser != null)
            {
                selectedUserName = selectedUser.UserName;
            }

            
            var chatViewModel = new ChatViewModel()
            {
                CurrentUserId = currentUserId,
                ReceiverId = selectedUserId,
                ReceiverUserName = selectedUserName,
            };

            
            var messages = await _context.Messages
               
                .Where(i=> (i.SenderId == currentUserId || i.SenderId == selectedUserId)
               
                && ( i.ReceiverId == currentUserId || i.ReceiverId == selectedUserId))
                .Select(i=>new UserMessagesListViewModel
            {
               Id = i.Id,
               Text = i.Text,
               Date = i.Date.ToShortDateString(),
               Time = i.Date.ToShortTimeString(),
               IsCurrentUserSendMessage = i.SenderId==currentUserId,
            }).ToListAsync();

            chatViewModel.Messages = messages;
            return chatViewModel;
        }

        
        public async Task<IEnumerable<MessagesUsersListViewModel>> GetUser()
        {
            
            var currentUserId = _currentUserService.UserId;

            
            var users = await _context.Users
                .Where(i => i.Id != currentUserId)
                .Select(i => new MessagesUsersListViewModel()
                {
                Id = i.Id,
                UserName = i.UserName,
                
                LastMessage = _context.Messages
             
                .Where(m => (m.SenderId == currentUserId || m.SenderId == i.Id)
                
                && (m.ReceiverId == i.Id)).OrderByDescending(m => m.Id).Select(m => m.Text).FirstOrDefault()    
                }).ToListAsync();
  

            return users;
        }
    }
}
