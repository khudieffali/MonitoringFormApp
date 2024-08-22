using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MonitoringFormApp.Infrastructure.Entities;
using MonitoringFormApp.Infrastructure.Repositories;

namespace MonitoringFormApp.Business
{
    public class FileMonitoringService
    {
        private readonly List<IFileLoader> _fileLoaders;
        private readonly string _directoryPath;
        private readonly int _interval;
        private readonly Timer _timer;
        private readonly Dictionary<string, DateTime> _fileLastModifiedDict = new Dictionary<string, DateTime>();

        public event EventHandler<DataLoadedEventArgs> DataLoaded;

        public FileMonitoringService(string directoryPath, int interval, List<IFileLoader> fileLoaders)
        {
            _fileLoaders = fileLoaders;
            _directoryPath = directoryPath;
            _interval = interval;

            _timer = new Timer(OnTimerElapsed, null, Timeout.Infinite, interval);
        }

        public void StartMonitoring()
        {
            _timer.Change(0, _interval);
        }

        public void StopMonitoring()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        protected virtual void OnDataLoaded(List<FileData> data)
        {
            DataLoaded?.Invoke(this, new DataLoadedEventArgs(data));
        }

        private void OnTimerElapsed(object state)
        {
            var files = Directory.GetFiles(_directoryPath);

            foreach (var file in files)
            {
                var lastModified = File.GetLastWriteTime(file);

                if (!_fileLastModifiedDict.ContainsKey(file) || _fileLastModifiedDict[file] < lastModified)
                {
                    _fileLastModifiedDict[file] = lastModified;

                    foreach (var loader in _fileLoaders)
                    {
                        if (file.EndsWith(loader.FileExtension, StringComparison.OrdinalIgnoreCase))
                        {
                            var data = loader.Load(file);
                            OnDataLoaded(data);
                        }
                    }
                }
            }
        }
    }
}