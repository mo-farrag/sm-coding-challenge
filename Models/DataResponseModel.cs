using System.Collections.Generic;
using System.Runtime.Serialization;

namespace sm_coding_challenge.Models
{
    [DataContract]
    public class DataResponseModel
    {
        [DataMember(Name = "week")]
        public string Week { get; set; }

        [DataMember(Name = "sportName")]
        public string SportName { get; set; }

        [DataMember(Name = "competitionName")]
        public string CompetitionName { get; set; }

        [DataMember(Name = "seasonID")]
        public string SeasonId { get; set; }

        [DataMember(Name = "rushing")]
        public List<RushingModel> Rushing { get; set; }

        [DataMember(Name = "passing")]
        public List<PassingModel> Passing { get; set; }

        [DataMember(Name = "receiving")]
        public List<ReceivingModel> Receiving { get; set; }

        [DataMember(Name = "kicking")]
        public List<KickingModel> Kicking { get; set; }

        public DataResponseModel()
        {

        }
    }
}

