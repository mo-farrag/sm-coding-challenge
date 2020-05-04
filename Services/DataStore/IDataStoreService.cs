using sm_coding_challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sm_coding_challenge.Services.DataStore
{
    public interface IDataStoreService
    {
        DataResponseModel GetPlayersData();
        bool FetchData();

    }
}
