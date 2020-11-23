using System.Collections.Generic;

namespace MyHiveService.Models.DB
{
    public interface IInspectionable<InspectionType>
        where InspectionType: InspectionBase
    {
        ICollection<InspectionType> inspections { get; set; }
    }
}