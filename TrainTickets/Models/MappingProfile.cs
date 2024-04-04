using AutoMapper;
using TrainTicket.Models;

namespace TrainTicket.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TrainDetails, TrainDetailsDto>();
            CreateMap<TrainDetailsDto, TrainDetails>();

            CreateMap<SeatDetails, SeatDetailsDto>().ReverseMap();
        }
    }
}
