using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyHiveService.Models.DB
{
    public class Hive : ModelBase, IInspectionable<HiveInspection>
    {
        [Required]
        public string label { get; set; }
        public DateTime? lastInspected { get; set; }
        public DateTime? queenLastSeen { get; set; }
        public ICollection<HiveInspection> inspections { get; set; }
        public ICollection<HivePart> parts { get; set; }

        [JsonIgnore]
        public byte[] photo { get; set; }

        //TODO: nearby plants
    }
}