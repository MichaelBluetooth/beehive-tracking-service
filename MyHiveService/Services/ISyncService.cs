using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyHiveService.Models;
using MyHiveService.Models.DB;
using MyHiveService.Models.DTO;

namespace MyHiveService.Services
{
    public interface ISyncService
    {
        Hive syncHive(HiveDTO hive);
        HivePart syncPart(HivePart box);
        Frame syncFrame(Frame frame);
        HiveInspection syncInspection(HiveInspectionDTO inspection);
        HivePartInspection syncInspection(HivePartInspectionDTO inspection);
        FrameInspection syncInspection(FrameInspectionDTO inspection);
    }
}