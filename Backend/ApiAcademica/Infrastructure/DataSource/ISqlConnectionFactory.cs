
using System.Data;


namespace Infrastructure.DataSource;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
