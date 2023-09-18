using AutoMapper;
using MISA.WebFresher052023.Application.Dto.Receiver;
using MISA.WebFresher052023.Domain.Entity.Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Mapper
{
    public class ReceiverProfile : Profile
    {
        public ReceiverProfile()
        {
            CreateMap<ReceiverEntity, ReceiverDto>().ReverseMap();
        }
    }
}
