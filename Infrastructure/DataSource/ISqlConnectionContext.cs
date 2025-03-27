
using System.Data;

namespace Infrastructure.DataSource
{
    public interface ISqlConnectionContext
    {
        IDbConnection CreateConnection();
    }
}
