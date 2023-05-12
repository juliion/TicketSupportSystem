using AutoMapper;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;

namespace TicketSupportSystem.Common.Mappings
{
    public class TicketSupportSystemProfile : Profile
    {
        public TicketSupportSystemProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<CreateTicketDTO, Ticket>();
            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.AssignedToEmail, opt => opt.MapFrom(src => src.AssignedTo == null ? null : src.AssignedTo.Email))
                .ForMember(dest => dest.AssignedToName, opt => opt.MapFrom(src => src.AssignedTo == null ? null : src.AssignedTo.Name));
        }

    }
}
