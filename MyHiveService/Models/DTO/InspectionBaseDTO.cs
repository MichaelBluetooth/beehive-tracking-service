using System;

namespace MyHiveService.Models.DTO
{
    public class InspectionBaseDTO : ModelDTOBase
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

        public string photoBase64 { get; set; }
    }
}