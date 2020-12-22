using Microsoft.EntityFrameworkCore;

namespace cy_chat.mysqlDB.Repositories
{

    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}
