using System;
using System.Collections.Generic;

namespace NeonCore.Models
{
    public partial class TaUser
    {
        public int UserIdx { get; set; }
        public string UserId { get; set; }
        public string Pwd { get; set; }
        public string UserNm { get; set; }
        public string Tel { get; set; }
        public string Hp { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public DateTime RegId { get; set; }
        public string RegIp { get; set; }
    }
}
