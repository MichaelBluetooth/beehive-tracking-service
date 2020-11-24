using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models.DB
{
    public class HivePart : ModelBase, IInspectionable<HivePartInspection>
    {
        [Required]
        public string label { get; set; }
        public int order { get; set; }
        public DateTime? dateAdded { get; set; }
        public ICollection<HivePartInspection> inspections { get; set; }
        public string type { get; set; }

        [Required]
        public Guid? hiveId { get; set; }
        public Hive hive { get; set; }

        public ICollection<Frame> frames { get; set; }
    }
}