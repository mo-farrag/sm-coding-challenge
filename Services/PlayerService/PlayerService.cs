using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using sm_coding_challenge.Models;
using sm_coding_challenge.Services.DataStore;

namespace sm_coding_challenge.Services.PlayerService
{
    //this is the old DataProviderImpl
    public class PlayerService : IPlayerService
    {
        public static TimeSpan Timeout = TimeSpan.FromSeconds(30);
        IDataStoreService _dataStoreService;

        public PlayerService(IDataStoreService dataStateService)
        {
            this._dataStoreService = dataStateService;
        }

        /// <summary>
        /// Get player data from the in-memory loaded data, 
        /// this function is just to filter and return the data related to the player id passed.
        /// if there is no data for that player(rushing, kicking, passing or receiving) will return null to not  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PlayerModel> GetPlayerById(string id)
        {
            var playerResult = new PlayerModel();
            List<KickingModel> kicking = new List<KickingModel>();
            List<RushingModel> rushing = new List<RushingModel>();
            List<ReceivingModel> receiving = new List<ReceivingModel>();
            List<PassingModel> passing = new List<PassingModel>();

            await Task.Run(() =>
            {
                kicking = _dataStoreService.GetPlayersData().Kicking.Where(x => x.player_id.Equals(id)).ToList();
                rushing = _dataStoreService.GetPlayersData().Rushing.Where(x => x.player_id.Equals(id)).ToList();
                receiving = _dataStoreService.GetPlayersData().Receiving.Where(x => x.player_id.Equals(id)).ToList();
                passing = _dataStoreService.GetPlayersData().Passing.Where(x => x.player_id.Equals(id)).ToList();
            });

            playerResult.Kicking = kicking.Any() ? kicking : null;
            playerResult.Rushing = rushing.Any() ? rushing : null;
            playerResult.Receiving = receiving.Any() ? receiving : null;
            playerResult.Passing = passing.Any() ? passing : null;

            if (playerResult.Kicking == null && playerResult.Rushing == null && playerResult.Receiving == null && playerResult.Passing == null)
                return null;
            return playerResult;

        }

        /// <summary>
        /// fetch the remote end point and get the players' data for the passed ids list
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<PlayerModel>> GetLatestPlayerById(string[] ids)
        {
            //force fetch data first 
            _dataStoreService.FetchData();

            //get player's data based on the ids parameter
            var result = new List<PlayerModel>();
            foreach (var id in ids)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var player = await Task.Run(() => GetPlayerById(id));
                    if (player != null)
                        result.Add(player);
                }
            }

            return result;
        }

        public async Task<bool> RefreshData()
        {
            return await Task.Run(() => _dataStoreService.FetchData());
        }
    }
}
