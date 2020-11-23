using System;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models.DB
{
    public class FrameInspection : InspectionBase
    {
        [Required]
        public Guid? frameId { get; set; }
        public Frame frame { get; set; }
    }
}