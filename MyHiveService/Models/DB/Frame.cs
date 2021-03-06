using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models.DB
{
    public class Frame : ModelBase, IInspectionable<FrameInspection>
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