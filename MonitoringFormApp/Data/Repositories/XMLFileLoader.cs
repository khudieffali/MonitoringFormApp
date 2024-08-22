using MonitoringFormApp.Infrastructure.Entities;
using MonitoringFormApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonitoringFormApp.Data.Repositories
{
    public class XMLFileLoader : IFileLoader
    {

        public string FileExtension => ".xml";

        public List<FileData> Load(string path)
        {
            var dataList = new List<FileData>();
            var doc = XDocument.Load(path);
            var values = doc.Descendants("value");
            foreach (var value in values)
            {
               dataList.Add(new FileData
                {
                    Date = DateTime.Parse(value.Attribute("date").Value),
                    Open = decimal.Parse(value.Attribute("open").Value),
                    High = decimal.Parse(value.Attribute("high").Value),
                    Low = decimal.Parse(value.Attribute("low").Value),
                    Close = decimal.Parse(value.Attribute("close").Value),
                    Volume = int.Parse(value.Attribute("volume").Value)
                });
            }
            return dataList;
        }
    }
}
