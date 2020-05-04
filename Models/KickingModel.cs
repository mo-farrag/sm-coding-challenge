using System.Runtime.Serialization;

namespace sm_coding_challenge.Models
{
    public class KickingModel
    {
        [DataMember(Name = "player_id")]
        public string player_id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "fld_goals_made")]
        public string fld_goals_made { get; set; }

        [DataMember(Name = "fld_goals_att")]
        public string fld_goals_att { get; set; }

        [DataMember(Name = "extra_pt_made")]
        public string extra_pt_made { get; set; }

        [DataMember(Name = "extra_pt_att")]
        public string extra_pt_att { get; set; }
    }
}

