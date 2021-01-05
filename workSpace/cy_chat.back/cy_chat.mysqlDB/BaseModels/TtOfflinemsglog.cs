using System;
using System.Collections.Generic;

namespace cy_chat.mysqlDB
{
    public partial class TtOfflinemsglog
    {
        public int OffLineUserId { get; set; }
        public int? UserId { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
