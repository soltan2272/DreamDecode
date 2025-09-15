using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DreamDecode.Application.Dream.DTOs;
using DreamDecode.Domain.Interpretation.Entities.DreamDecode.Domain.Entities;

namespace DreamDecode.Application.Interpretation.Mappings
{
    public class InterpretationProfile :Profile
    {
        public InterpretationProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<InterpretationEntity,DreamDto>();
        }
    }
}
