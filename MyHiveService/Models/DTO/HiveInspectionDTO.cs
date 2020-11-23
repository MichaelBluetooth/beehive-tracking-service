using System;
using System.ComponentModel.DataAnnotations;
using MyHiveService.Models.DB;

namespace MyHiveService.Models.DTO
{
    public class HiveInspectionDTO : InspectionBaseDTO
    {
        [Required]
        public Guid? hiveId { get; set; }
        public Hive hive { get; set; }
    }
}