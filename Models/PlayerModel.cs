using System.Collections.Generic;
using System.Runtime.Serialization;

namespace sm_coding_challenge.Models
{
    [DataContract]
    public class PlayerModel
    {
        [DataMember(Name = "rushing")]
        public List<RushingModel> Rushing { get; set; }

        [DataMember(Name = "passing")]
        public List<PassingModel> Passing { get; set; }

        [DataMember(Name = "receiving")]
        public List<ReceivingModel> Receiving { get; set; }

        [DataMember(Name = "kicking")]
        public List<KickingModel> Kicking { get; set; }
    }
}

