using Application.DTO;
using AutoMapper;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;

namespace Application.AutoMapper
{
    public class ReceiverProfile : Profile
    {
        public ReceiverProfile()
        {
            CreateMap<Receiver, ReceiverDto>();
            CreateMap<Receiver, ReceiverModel>();
            CreateMap<ReceiverModel, ReceiverDto>();
            CreateMap<ReceiverCreateDto, Receiver>();
            CreateMap<ReceiverUpdateDto, Receiver>();
            CreateMap<ReceiverCreateDto, ReceiverModel>();
            CreateMap<ReceiverUpdateDto, ReceiverModel>();
        }
    }
}
