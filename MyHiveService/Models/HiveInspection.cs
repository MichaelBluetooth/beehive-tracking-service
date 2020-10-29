using System;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models
{
    public class HiveInspection : InspectionBase
    {
        [Required]
        public Guid? hiveId { get; set; }
        public Hive hive { get; set; }
    }
}