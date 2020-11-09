using System.IO;

namespace MyHiveService.Services.Download
{
    public interface IDownloadService
    {
        byte[] downloadZip();
    }
}