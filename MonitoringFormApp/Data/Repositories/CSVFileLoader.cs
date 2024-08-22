using MonitoringFormApp.Infrastructure.Entities;
using MonitoringFormApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringFormApp.Data.Repositories
{
    public class CSVFileLoader : IFileLoader
    {
        public string FileExtension => ".csv";

        public List<FileData> Load(string path)
        {
            var dataList = new List<FileData>();
            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length == 6)
                    {
                        dataList.Add(new FileData
                        {
                            Date = DateTime.ParseExact(values[0], "yyyy-M-d", CultureInfo.InvariantCulture),
                            Open = decimal.Parse(values[1]),
                            High = decimal.Parse(values[2]),
                            Low = decimal.Parse(values[3]),
                            Close = decimal.Parse(values[4]),
                            Volume = long.Parse(values[5])
                        });
                    }
                }
            }
                return dataList;

        }
    }
}
