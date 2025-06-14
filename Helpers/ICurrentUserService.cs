using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Helpers
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Task<AppUser> GetUser();
    }
}
