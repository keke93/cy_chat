using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cy_chat.mysqlDB.Paging
{

    public class DataPage<TEntity>
    {
        public IEnumerable<TEntity> Data { get; set; }
        public long TotalEntityCount { get; set; }

        public int PageNumber { get; set; }
        public int PageLength { get; set; }

        public int TotalPageCount => Convert.ToInt32(Math.Ceiling((decimal)TotalEntityCount / PageLength));
    }
}
