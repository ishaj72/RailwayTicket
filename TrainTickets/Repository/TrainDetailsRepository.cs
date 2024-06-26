using AutoMapper;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Repository
{
    public class TrainDetailsRepository : ITrainDetailsInterface
    {
        private readonly ReservationDbContext _context;
        private readonly IMapper _mapper;

        public TrainDetailsRepository(ReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TrainDetailsDto AddTrains(TrainDetailsDto trainDto)
        {
            var train = _mapper.Map<TrainDetails>(trainDto);

            _context.Add(train);
            _context.SaveChanges();

            return _mapper.Map<TrainDetailsDto>(train);
        }

        public TrainDetailsDto GetTrainByNumber(int trainid)
        {
            var train = _context.Trains.SingleOrDefault(t => t.TrainNumber == trainNumber);
            return _mapper.Map<TrainDetailsDto>(train);
        }

        public TrainDetailsDto UpdateTrain(int trainid, TrainDetailsDto trainDto)
        {
            var train = _mapper.Map<TrainDetails>(trainDto);
            var existingTrain = _context.Trains.Find(trainid);

            if (existingTrain != null)
            {
                existingTrain.TrainName = train.TrainName;
                existingTrain.Source = train.Source;
                existingTrain.Destination = train.Destination;
                existingTrain.SourceArrival = train.SourceArrival;
                existingTrain.SourceDeparture = train.SourceDeparture;
                existingTrain.DestinationArrival = train.DestinationArrival;
                existingTrain.DestinationDeparture = train.DestinationDeparture;

                _context.SaveChanges();

                return _mapper.Map<TrainDetailsDto>(existingTrain);
            }
            return null;
        }

        public bool Delete(int trainNumber)
        {
            var trainToDelete = _context.Trains.SingleOrDefault(t => t.TrainNumber == trainNumber);

            if (trainToDelete != null)
            {
                _context.Trains.Remove(trainToDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

    }
}
