using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Time_Chat_Application.Models
{
    public class AppUser:IdentityUser
    {
        [InverseProperty(nameof(Message.Sender))]
        public virtual ICollection<Message> SendMessages { get; set; }
        [InverseProperty(nameof(Message.Receiver))]
        public virtual ICollection<Message> ReceivedMessags { get; set; }
    }
}
