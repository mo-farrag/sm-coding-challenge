using Microsoft.Extensions.Hosting;
using sm_coding_challenge.Services.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace sm_coding_challenge.Services.HostedServices
{
    /// <summary>
    /// this will work as a hosted service that will start to work every 1 week 
    /// to fetch a new version of the players data, as the data will update every 1 week.
    /// </summary>
    public class DataHostedService : IHostedService, IDisposable
    {
        IDataStoreService _dataStoreService;
        private Timer _timer;

        public DataHostedService(IDataStoreService dataStoreService)
        {
            this._dataStoreService = dataStoreService;
        }
        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var interval = 7 * 24 * 60 * 60 * 1000;
            _timer = new Timer(CallDataFetch, null, interval, interval); // run every 1 week

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        void CallDataFetch(object state)
        {
            _dataStoreService.FetchData();
        }
    }
}
