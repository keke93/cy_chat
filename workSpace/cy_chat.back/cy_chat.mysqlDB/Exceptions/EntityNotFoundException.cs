using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cy_chat.mysqlDB.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, int entityKey)
        {
            EntityName = entityName;
            EntityKey = entityKey;
            Message = string.Format("Entity of type '{0}' and key {1} not found in the current context.", entityName, EntityKey);
        }

        public string EntityName { get; set; }

        public int EntityKey { get; set; }

        public override string Message { get; }
    }
}
