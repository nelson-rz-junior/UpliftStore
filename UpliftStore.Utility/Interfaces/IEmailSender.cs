using System.Threading.Tasks;

namespace UpliftStore.Utility.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string name, string email, string subject, string htmlMessage);
    }
}
