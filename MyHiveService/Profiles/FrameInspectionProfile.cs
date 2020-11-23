using System;
using AutoMapper;
using MyHiveService.Models.DB;
using MyHiveService.Models.DTO;

namespace MyHiveService.Profiles
{
    public class FrameInspectionProfile : Profile
    {
        public FrameInspectionProfile()
        {
            CreateMap<FrameInspectionDTO, FrameInspection>();
            CreateMap<FrameInspectionDTO, FrameInspection>()
                .ForMember(dto => dto.photo,
                                    e => e.MapFrom(o => Convert.FromBase64String(o.photoBase64.Replace("data:image/jpeg;base64,", ""))));
        }
    }
}