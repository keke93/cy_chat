using System;
using System.Collections.Generic;

namespace cy_chat.mysqlDB
{
    public partial class TmUser
    {
        public int IUserId { get; set; }
        public string SUserAccount { get; set; }
        public int? IStatus { get; set; }
        public int? ICreateBy { get; set; }
        public DateTime? DtCreateTime { get; set; }
        public int? IUpdateBy { get; set; }
        public DateTime? DtUpdateTime { get; set; }
    }
}
