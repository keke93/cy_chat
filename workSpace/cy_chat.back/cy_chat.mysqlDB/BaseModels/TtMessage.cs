using System;
using System.Collections.Generic;

namespace cy_chat.mysqlDB
{
    public partial class TtMessage
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public int? SendBy { get; set; }
        public int? ReceivedBy { get; set; }
        public int? SendStatus { get; set; }
        public int? SendType { get; set; }
        public int? MessageTypeId { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
