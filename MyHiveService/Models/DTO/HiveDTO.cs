using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyHiveService.Models.DB;

namespace MyHiveService.Models.DTO
{
    public class HiveDTO: ModelDTOBase
    {
        [Required]
        public string label { get; set; }
        public DateTime? lastInspected { get; set; }
        public DateTime? queenLastSeen { get; set; }
        public ICollection<HiveInspection> inspections { get; set; }
        public ICollection<HivePart> parts { get; set; }
        
        public string photoBase64 { get; set; }
    }
}