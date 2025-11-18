using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DreamDecode.Application.Dream.DTOs;
using DreamDecode.Domain.Dream.Entities;

namespace DreamDecode.Application.Dream.Mappings
{
    public class DreamProfile:Profile
    {
        public DreamProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<DreamEntity,PendingDreamsDto>()
                 .ForMember(dest => dest.DreamTitle, opt => opt.MapFrom(src => src.Title))
    .ForMember(dest => dest.DreamDescription, opt => opt.MapFrom(src => src.Description))
    .ForMember(dest => dest.isInterpreted, opt => opt.MapFrom(src => src.IsInterpreted))
    .ForMember(dest => dest.isPaid, opt => opt.MapFrom(src => src.IsPaid));
            ;


        }
    }
}
