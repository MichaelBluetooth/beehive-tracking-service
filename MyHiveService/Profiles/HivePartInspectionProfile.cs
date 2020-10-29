using System;
using AutoMapper;
using MyHiveService.Models;
using MyHiveService.Models.DTO;

namespace MyHiveService.Profiles
{
    public class HivePartInspectionProfile : Profile
    {
        public HivePartInspectionProfile()
        {
            CreateMap<HivePartInspectionDTO, HivePartInspection>();
            CreateMap<HivePartInspectionDTO, HivePartInspection>()
                .ForMember(dto => dto.photo,
                                    e => e.MapFrom(o => Convert.FromBase64String(o.photoBase64)));
        }
    }
}