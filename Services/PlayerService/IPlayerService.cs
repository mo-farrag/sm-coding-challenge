using sm_coding_challenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sm_coding_challenge.Services.PlayerService
{
    //thsi is the old IDataProvider
    public interface IPlayerService
    {
        Task<PlayerModel> GetPlayerById(string id);
        Task<List<PlayerModel>> GetLatestPlayerById(string[] ids);
        Task<bool> RefreshData();
    }
}
