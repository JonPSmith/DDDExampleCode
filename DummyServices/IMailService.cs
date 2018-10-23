using DataLayer;

namespace DummyServices
{
    public interface IMailService
    {
        void SendMail(User user, string message);
    }
}