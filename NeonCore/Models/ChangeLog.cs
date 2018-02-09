using System;
using System.Collections.Generic;

namespace NeonCore.Models
{
    public partial class ChangeLog
    {
        public int ChangeLogId { get; set; }
        public string ClassNm { get; set; }
        public string PropertyNm { get; set; }
        public string OriginalValue { get; set; }
        public string CurrentValue { get; set; }
        public string UserNm { get; set; }
        public DateTime? ChangeDtm { get; set; }
    }
}
