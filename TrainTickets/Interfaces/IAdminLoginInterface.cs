using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface IAdminLoginInterface
    {
        Admin AdminLogin(string id, string name, string password);
    }
}
