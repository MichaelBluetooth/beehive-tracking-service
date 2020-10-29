using System;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models.DTO
{
    public class FrameInspectionDTO : InspectionBaseDTO
    {
        [Required]
        public Guid? frameId { get; set; }
        public Frame frame { get; set; }
    }
}