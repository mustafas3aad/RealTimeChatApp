namespace Real_Time_Chat_Application.ViewModels.Messages
{
    public class UserMessagesListViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool IsCurrentUserSendMessage { get; set; }
    }
}
