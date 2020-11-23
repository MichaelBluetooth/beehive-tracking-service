using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyHiveService.Models.DB;
using Newtonsoft.Json;

namespace MyHiveService.Services.Download
{
    public class DownloadService : IDownloadService
    {
        private readonly MyHiveDbContext _ctx;
        public DownloadService(MyHiveDbContext ctx)
        {
            _ctx = ctx;
        }

        public byte[] downloadZip()
        {
            List<Hive> hives = _getFullHives();
            byte[] zip = null;

            using (var compressedFileStream = new MemoryStream())
            {
                using (ZipArchive zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (Hive hive in hives)
                    {
                        if (null != hive.photo && hive.photo.Length > 0)
                        {
                            _appendHivePhoto(hive, zipArchive);
                        }

                        _appendInspectionPhotos("hive", hive, zipArchive);
                        foreach (HivePart part in hive.parts)
                        {
                            _appendInspectionPhotos("hivepart", part, zipArchive);
                            foreach (Frame frame in part.frames)
                            {
                                _appendInspectionPhotos("frame", frame, zipArchive);
                            }
                        }
                    }

                    string jsonString = JsonConvert.SerializeObject(hives, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });
                    byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
                    var jsonDataZipEntry = zipArchive.CreateEntry("data.json");
                    using (var entryStream = new MemoryStream(jsonBytes))
                    using (var zipEntryStream = jsonDataZipEntry.Open())
                    {
                        entryStream.CopyTo(zipEntryStream);
                    }
                }
                zip = compressedFileStream.ToArray();
            }

            return zip;
        }
        private List<Hive> _getFullHives()
        {
            return _ctx.Hives
                .Include(h => h.parts)
                    .ThenInclude(p => p.frames)
                        .ThenInclude(f => f.inspections)
                .Include(h => h.parts)
                    .ThenInclude(p => p.inspections)
                .Include(h => h.inspections)
                .ToList();
        }

        private void _appendInspectionPhotos<T>(string fileNameBase, IInspectionable<T> inspectionable, ZipArchive zipArchive)
            where T : InspectionBase
        {
            foreach (InspectionBase inspection in inspectionable.inspections.Where(i => null != i.photo && i.photo.Length > 0))
            {
                ZipArchiveEntry zipEntry = zipArchive.CreateEntry($"{fileNameBase}_{inspection.id}.jpeg", CompressionLevel.Fastest);
                using (var zipEntryStream = zipEntry.Open())
                {
                    using (var entryStream = new MemoryStream(inspection.photo))
                    {
                        entryStream.CopyTo(zipEntryStream);
                    }
                }
            }
        }

        private void _appendHivePhoto(Hive hive, ZipArchive zipArchive)
        {
            ZipArchiveEntry zipEntry = zipArchive.CreateEntry($"hive_photo_{hive.id}.jpeg", CompressionLevel.Fastest);
            using (var zipEntryStream = zipEntry.Open())
            {
                using (var entryStream = new MemoryStream(hive.photo))
                {
                    entryStream.CopyTo(zipEntryStream);
                }
            }
        }
    }
}