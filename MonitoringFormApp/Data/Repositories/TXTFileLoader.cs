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
using System.Windows.Forms;

namespace MonitoringFormApp.Data.Repositories
{
    public class TXTFileLoader : IFileLoader
    { 
         public string FileExtension => ".txt";
    
        public List<FileData> Load(string path)
        {
            var dataList = new List<FileData>();
            var lines = File.ReadAllLines(path);
            string format = "yyyy-MM-dd";
            foreach (var line in lines)
            {
                var columns = line.Split(',').ToList();

                if (DateTime.TryParseExact(columns[0], format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    dataList.Add(new FileData
                    {
                        Date = date,
                        Open = decimal.Parse(columns[1]),
                        High = decimal.Parse(columns[2]),
                        Low = decimal.Parse(columns[3]),
                        Close = decimal.Parse(columns[4]),
                        Volume = long.Parse(columns[5])
                    });
                }
                else
                {
                    Console.WriteLine($"Tarix formatı səhvdir: {columns[0]}");
                }

            }
            return dataList;
        }
    }
}
