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
            CreateMap<DreamEntity,PendingDreamsDto>();


        }
    }
}
