using System;
using Newtonsoft.Json;

namespace MyHiveService.Models
{
    public class InspectionBase : ModelBase
    {
        public DateTime date { get; set; }

        public bool queenSpotted { get; set; }
        public bool broodSpotted { get; set; }
        public bool eggsSpotted { get; set; }
        public bool larvaSpotted { get; set; }
        public bool queenCellsSpotted { get; set; }
        public bool supercedureCellsSpotted { get; set; }
        public bool swarmCellsSpotted { get; set; }
        public bool orientationFlights { get; set; }
        public string activityLevel { get; set; }
        public string details { get; set; }

        [JsonIgnore]
        public byte[] photo { get; set; }

        //TODO: pests
    }
}