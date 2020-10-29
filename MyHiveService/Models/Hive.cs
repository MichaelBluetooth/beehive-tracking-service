using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models
{
    public class Hive : ModelBase
    {
        [Required]
        public string label { get; set; }
        public DateTime? lastInspected { get; set; }
        public DateTime? queenLastSeen { get; set; }
        public ICollection<HiveInspection> inspections { get; set; }
        public ICollection<HivePart> parts { get; set; }

        //TODO: nearby plants
    }
}