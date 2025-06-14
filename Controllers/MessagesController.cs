using Microsoft.AspNetCore.Mvc;
using Real_Time_Chat_Application.Interface;

namespace Real_Time_Chat_Application.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _messageService.GetUser();
            return View(users);
        }

        public async Task<IActionResult> Chat (string selectedUserID)
        {
            var chatViewModel = await _messageService.GetMessages(selectedUserID);
            return View(chatViewModel);
        }
    }
}
