using System;
using System.ComponentModel.DataAnnotations;
using MyHiveService.Models.DB;

namespace MyHiveService.Models.DTO
{
    public class HivePartInspectionDTO : InspectionBaseDTO
    {
        [Required]
        public Guid? hivePartId { get; set; }
        public HivePart hivePart { get; set; }
    }
}