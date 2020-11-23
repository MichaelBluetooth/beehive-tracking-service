using System;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models.DB
{
    public class HivePartInspection : InspectionBase
    {
        [Required]
        public Guid? hivePartId { get; set; }
        public HivePart hivePart { get; set; }
    }
}