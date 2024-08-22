using MonitoringFormApp.Business;
using MonitoringFormApp.Data.Repositories;
using MonitoringFormApp.Infrastructure.Entities;
using MonitoringFormApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitoringFormApp
{
    public partial class MonitoringForm : Form
    {
        private FileMonitoringService _fileMonitoring;

        public MonitoringForm()
        {
            InitializeComponent();
            InitializeFileMonitor();
        }

        private void MonitoringForm_Load(object sender, EventArgs e)
        {
        }

        private void OnDataLoaded(object sender, DataLoadedEventArgs e)
        {
            if (e.Data == null || !e.Data.Any())
            {
                MessageBox.Show("No data loaded.");
                return;
            }

            Invoke(new Action(() =>
            {
                var dt = new DataTable();
                dt.Columns.Add("Date", typeof(DateTime));
                dt.Columns.Add("Open", typeof(decimal));
                dt.Columns.Add("High", typeof(decimal));
                dt.Columns.Add("Low", typeof(decimal));
                dt.Columns.Add("Close", typeof(decimal));
                dt.Columns.Add("Volume", typeof(long));

                foreach (var dataItem in e.Data)
                {
                    dt.Rows.Add(dataItem.Date, dataItem.Open, dataItem.High, dataItem.Low, dataItem.Close, dataItem.Volume);
                }

                dtgFiles.DataSource = dt;
            }));
        }

        private void InitializeFileMonitor()
        {
            var loaders = new List<IFileLoader>
            {
                new TXTFileLoader(),
                new CSVFileLoader(),
                new XMLFileLoader()
            };

            _fileMonitoring = new FileMonitoringService(@"D:\MonitoringFiles", 1000000, loaders);
            _fileMonitoring.DataLoaded += OnDataLoaded;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _fileMonitoring?.StopMonitoring();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _fileMonitoring.StartMonitoring();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _fileMonitoring.StopMonitoring();
        }
    }
}
