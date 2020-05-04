using System.Runtime.Serialization;

namespace sm_coding_challenge.Models
{
    public class ReceivingModel
    {
        [DataMember(Name = "player_id")]
        public string player_id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "yds")]
        public string YDS { get; set; }

        [DataMember(Name = "tds")]
        public string TDS { get; set; }

        [DataMember(Name = "rec")]
        public string REC { get; set; }
    }
}

