using System;
using System.Collections.Generic;

namespace cy_chat.mysqlDB
{
    public partial class TmSysuser
    {
        public int SysUserId { get; set; }
        public string SysUserName { get; set; }
        public string SysPassword { get; set; }
        public int? Status { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
