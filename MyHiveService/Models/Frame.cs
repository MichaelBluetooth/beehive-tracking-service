using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models
{
    public class Frame : ModelBase
    {
        [Required]
        public string label { get; set; }
        public DateTime lastInspected { get; set; }
        public ICollection<FrameInspection> inspections { get; set; }

        [Required]
        public Guid? hivePartId { get; set; }
        public HivePart hivePart { get; set; }
    }
}