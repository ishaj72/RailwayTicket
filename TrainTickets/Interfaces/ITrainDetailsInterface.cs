using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface ITrainDetailsInterface
    {
        TrainDetailsDto AddTrains(TrainDetailsDto trains);
        TrainDetailsDto GetTrainByNumber(int trainNumber);
        TrainDetailsDto UpdateTrain(int id, TrainDetailsDto trains);
        bool Delete(int trainNumber);
    }
}
