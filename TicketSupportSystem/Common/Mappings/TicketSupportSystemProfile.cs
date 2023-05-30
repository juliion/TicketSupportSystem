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
            CreateMap<UserRegistrationDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.TicketTitle, opt => opt.MapFrom(src => src.Ticket.Title));


            CreateMap<CreateTicketDTO, Ticket>();
            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.AssignedToEmail, opt => opt.MapFrom(src => src.AssignedTo == null ? null : src.AssignedTo.Email))
                .ForMember(dest => dest.AssignedToName, opt => opt.MapFrom(src => src.AssignedTo == null ? null : src.AssignedTo.Name));
            CreateMap<Ticket, TicketDetailsDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.AssignedToEmail, opt => opt.MapFrom(src => src.AssignedTo == null ? null : src.AssignedTo.Email))
                .ForMember(dest => dest.AssignedToName, opt => opt.MapFrom(src => src.AssignedTo == null ? null : src.AssignedTo.Name));
        }

    }
}
