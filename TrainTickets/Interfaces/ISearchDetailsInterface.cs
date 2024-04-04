using TrainTicket.Models;
namespace TrainTicket.Interfaces
{
    public interface ISearchDetailsInterface
    {
        TrainDetails SearchDetails(string source, string destination);
    }
}
