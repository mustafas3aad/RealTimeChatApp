using Real_Time_Chat_Application.ViewModels.Messages;

namespace Real_Time_Chat_Application.Interface
{
    public interface IMessageService
    {
        Task<IEnumerable<MessagesUsersListViewModel>> GetUser();
        Task<ChatViewModel> GetMessages(string selectedUserId);
    }
}
