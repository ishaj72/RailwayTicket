using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface IUserLoginInterface
    {
        UserDetails Login(string userid, string password);
    }
}
