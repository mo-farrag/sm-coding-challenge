using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using sm_coding_challenge.Models;

namespace sm_coding_challenge.Services.DataStore
{
    /// <summary>
    /// this class is for fetching and storing data in memory object. 
    /// it follow the singleton pattern to have only one instance of it shared in the application memory to save time of calling the api on every request.
    /// this class is acting as a Repository class 
    /// </summary>
    public class DataStoreService : IDataStoreService
    {
        public static TimeSpan Timeout = TimeSpan.FromSeconds(30);

        DataResponseModel _playersData;
        public DateTime LastFetchAt { get; set; }

        public DataStoreService()
        {
            FetchData();
        }

        /// <summary>
        /// call api to get fresh version of the players data, and save it in in-memory object.
        /// </summary>
        public bool FetchData()
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var client = new HttpClient(handler))
            {
                client.Timeout = Timeout;
                try
                {
                    var response = client.GetAsync("https://gist.githubusercontent.com/RichardD012/a81e0d1730555bc0d8856d1be980c803/raw/3fe73fafadf7e5b699f056e55396282ff45a124b/basic.json").Result;
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    this._playersData = JsonConvert.DeserializeObject<DataResponseModel>(stringData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                }
                catch (HttpRequestException ex)
                {
                    return false;
                }
                LastFetchAt = DateTime.UtcNow;
            }
            return true;
        }

        /// <summary>
        /// get list of players data that were fetched earlier
        /// </summary>
        /// <returns></returns>
        public DataResponseModel GetPlayersData()
        {
            return _playersData;
        }
    }
}
