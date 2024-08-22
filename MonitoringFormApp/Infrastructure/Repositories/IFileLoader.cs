using MonitoringFormApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringFormApp.Infrastructure.Repositories
{
    public interface IFileLoader
    {
        string FileExtension { get; }
        List<FileData> Load(string path);
    }
}
