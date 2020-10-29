using System;
using AutoMapper;
using MyHiveService.Models;
using MyHiveService.Models.DTO;

namespace MyHiveService.Profiles
{
    public class HiveInspectionProfile : Profile
    {
        public HiveInspectionProfile()
        {
            CreateMap<HiveInspectionDTO, HiveInspection>()
                .ForMember(dto => dto.photo,
                                    e => e.MapFrom(o => Convert.FromBase64String(o.photoBase64.Replace("data:image/jpeg;base64,", "")))); ;

        }
    }
}