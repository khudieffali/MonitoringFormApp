using MonitoringFormApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringFormApp.Business
{
    public class DataLoadedEventArgs : EventArgs
    {
        public List<FileData> Data { get; }

        public DataLoadedEventArgs(List<FileData> data)
        {
            Data = data;
        }
    }
}
