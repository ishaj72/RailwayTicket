using AutoMapper;
using TrainTicket.Models;

namespace TrainTicket.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TrainDetailsDto, TrainDetails>()
            .ForMember(dest => dest.TrainNumber, opt => opt.MapFrom(src => src.TrainNumber));
            CreateMap<SeatDetailsDto, SeatDetails>();
            CreateMap<TrainDetails, TrainDetailsDto>();
            CreateMap<SeatDetails, SeatDetailsDto>();
        }
    }
}
