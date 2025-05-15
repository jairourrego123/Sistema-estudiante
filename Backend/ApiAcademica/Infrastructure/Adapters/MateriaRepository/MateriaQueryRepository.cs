using Domain.Ports.IMateriaRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource;
using System.Data;
using Dapper;

namespace Infrastructure.Adapters.MateriaRepository;

[Repository]
public class MateriaQueryRepository : IMateriaQueryRepository
{
    private readonly ISqlConnectionFactory _cf;

    public MateriaQueryRepository(ISqlConnectionFactory cf)
        => _cf = cf;

    public async Task<Materia?> ObtenerPorIdAsync(Guid id)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerMateriaPorId";

        return await db.QueryFirstOrDefaultAsync<Materia>(
            storedProcedure,
            new { id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<List<Materia>> ObtenerTodosAsync()
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerTodasLasMaterias";

        IEnumerable<Materia> materias = await ObtenerMaterias(db, storedProcedure, CommandType.StoredProcedure);

        return materias.AsList();
    }

    public async Task<List<Materia>> ObtenerPorIdsAsync(IEnumerable<Guid> ids)
    {
        if (!ids.Any()) return new List<Materia>();

        using IDbConnection db = _cf.CreateConnection();

        DataTable tvp = new DataTable();
        tvp.Columns.Add("Id", typeof(Guid));
        foreach (var id in ids)
            tvp.Rows.Add(id);

        const string storedProcedure = "paObtenerMateriasPorIds";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Ids", tvp.AsTableValuedParameter("GuidList"));

        IEnumerable<Materia> list = await db.QueryAsync<Materia>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return list.AsList();
    }

    private static async Task<IEnumerable<Materia>> ObtenerMaterias(IDbConnection db, string sqlOrSpName, CommandType type = CommandType.Text)
    {
        return await db.QueryAsync<Materia, Profesor, Materia>(
            sqlOrSpName,
            map: (materia, profesor) =>
            {
                materia.AsignarProfesor(profesor);
                return materia;
            },
            splitOn: "Id",
            commandType: type
        );
    }


}