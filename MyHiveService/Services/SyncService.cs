using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;
using MyHiveService.Models.DTO;

namespace MyHiveService.Services
{
    public class SyncService: ISyncService
    {
        private readonly MyHiveDbContext _ctx;
        private readonly ILogger<SyncService> _logger;
        private readonly IMapper _mapper;

        public SyncService(MyHiveDbContext ctx, ILogger<SyncService> logger, IMapper mapper)
        {
            _ctx = ctx;
            _logger = logger;
            _mapper = mapper;
        }

        public Hive syncHive(Hive hive)
        {
            Hive existing = _ctx.Hives.Where(h => h.clientId == hive.clientId).FirstOrDefault();
            if (null == hive.id || null == existing)
            {
                _logger.LogInformation("Syncing new hive", hive);
                _ctx.Hives.Add(hive);
            }
            else
            {
                if (existing.lastModified > hive.lastModified)
                {
                    _logger.LogInformation("Server has newer hive data", hive, existing);
                    hive = existing;
                }
                else
                {
                    _logger.LogInformation("Server has old hive data", hive, existing);
                    existing.lastModified = hive.lastModified;
                    existing.label = hive.label;
                    existing.queenLastSeen = hive.queenLastSeen;
                    _ctx.Hives.Update(existing);
                    hive = existing;
                }
            }
            return hive;
        }

        public HivePart syncPart(HivePart box)
        {
            HivePart existing = _ctx.HiveParts.Where(b => b.clientId == box.clientId).FirstOrDefault(); ;
            if (null == box.id || null == existing)
            {
                _logger.LogInformation("Syncing new part", box);
                _ctx.HiveParts.Add(box);
            }
            else
            {
                if (existing.lastModified > box.lastModified)
                {
                    _logger.LogInformation("Server has newer part data", box, existing);
                    box = existing;
                }
                else
                {
                    _logger.LogInformation("Server has older part data", box, existing);
                    existing.lastModified = box.lastModified;
                    existing.label = box.label;
                    existing.order = box.order;
                    existing.dateAdded = box.dateAdded;
                    _ctx.HiveParts.Update(existing);
                    box = existing;
                }
            }
            return box;
        }

        public Frame syncFrame(Frame frame)
        {
            Frame existing = _ctx.Frames.Where(b => b.clientId == frame.clientId).FirstOrDefault(); ;
            if (null == frame.id || null == existing)
            {
                _logger.LogInformation("Syncing new frame", frame);
                _ctx.Frames.Add(frame);
            }
            else
            {
                if (existing.lastModified > frame.lastModified)
                {
                    _logger.LogInformation("Server has newer frame data", frame, existing);
                    frame = existing;
                }
                else
                {
                    _logger.LogInformation("Server has older frame data", frame, existing);
                    existing.lastModified = frame.lastModified;
                    existing.label = frame.label;
                    existing.lastInspected = frame.lastInspected;
                    _ctx.Frames.Update(existing);
                    frame = existing;
                }
            }
            return frame;
        }

        private InspectionBase _syncInspection<InspectionType, InspectionDTOType>(InspectionDTOType inspectionDTO, DbSet<InspectionType> inspections)
            where InspectionType: InspectionBase
            where InspectionDTOType: InspectionBaseDTO
        {
            InspectionType existing = inspections.Where(b => b.clientId == inspectionDTO.clientId).FirstOrDefault(); ;
            InspectionType inspection = _mapper.Map<InspectionType>(inspectionDTO);

            if (null == inspection.id || null == existing)
            {
                _logger.LogInformation("Syncing inspection frame", inspection);
                inspections.Add(inspection);
            }
            else
            {
                if (existing.lastModified > inspection.lastModified)
                {
                    _logger.LogInformation("Server has older inspection data", inspection, existing);
                    inspection = existing;
                }
                else
                {
                    _logger.LogInformation("Server has older inspection data", inspection, existing);
                    existing.lastModified = inspection.lastModified;
                    existing.larvaSpotted = inspection.larvaSpotted;
                    existing.orientationFlights = inspection.orientationFlights;
                    existing.photo = inspection.photo;
                    existing.queenCellsSpotted = inspection.queenCellsSpotted;
                    existing.queenSpotted = inspection.queenSpotted;
                    existing.supercedureCellsSpotted = inspection.supercedureCellsSpotted;
                    existing.swarmCellsSpotted = inspection.swarmCellsSpotted;
                    existing.activityLevel = inspection.activityLevel;
                    existing.broodSpotted = inspection.broodSpotted;
                    existing.date = inspection.date;
                    existing.details = inspection.details;
                    existing.eggsSpotted = inspection.eggsSpotted;                    
                    inspections.Update(existing);
                    inspection = existing;
                }
            }
            return inspection;
        }

        public HiveInspection syncInspection(HiveInspectionDTO inspection)
        {
            return _syncInspection<HiveInspection, HiveInspectionDTO>(inspection, _ctx.HiveInspections) as HiveInspection;
        }

        public HivePartInspection syncInspection(HivePartInspectionDTO inspection)
        {
            return _syncInspection<HivePartInspection, HivePartInspectionDTO>(inspection, _ctx.HivePartInspections) as HivePartInspection;
        }

        public FrameInspection syncInspection(FrameInspectionDTO inspection)
        {
            return _syncInspection<FrameInspection, FrameInspectionDTO>(inspection, _ctx.FrameInspections) as FrameInspection;
        }
    }
}