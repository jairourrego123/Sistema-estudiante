using Domain.Entities;
using Domain.Ports.IEstudianteRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource;
using System.Data;
using Dapper;

namespace Infrastructure.Adapters.EstudianteRepository;

[Repository]
public class EstudianteQueryRepository : IEstudianteQueryRepository
{
    private readonly ISqlConnectionFactory _cf;
    public EstudianteQueryRepository(ISqlConnectionFactory cf) => _cf = cf;
    public async Task<Estudiante> ObtenerPorUserIdAsync(string userId)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerEstudiantePorUserId";

        Dictionary<Guid, Estudiante> dict = new Dictionary<Guid, Estudiante>();
        await ObtenerConsulta(userId, db, storedProcedure, dict);

        return dict.Values.FirstOrDefault()!;
    }

    public async Task<Estudiante?> ObtenerPorIdAsync(Guid id)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerEstudiantePorId";

        return await db.QueryFirstOrDefaultAsync<Estudiante>(
            storedProcedure,
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<List<Estudiante>> ObtenerTodosAsync()
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerTodosEstudiantes";

        IEnumerable<Estudiante> list = await db.QueryAsync<Estudiante>(
            storedProcedure,
            commandType: CommandType.StoredProcedure
        );
        return list.AsList();
    }

    public async Task<List<string>> ListarCompañerosAsync(Guid materiaId, string userId)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paListarCompañerosPorMateria";

        IEnumerable<string> list = await db.QueryAsync<string>(
            storedProcedure,
            new { materiaId, userId },
            commandType: CommandType.StoredProcedure
        );
        return list.AsList();
    }

    private static async Task ObtenerConsulta(string userId, IDbConnection db, string storedProcedure, Dictionary<Guid, Estudiante> dict)
    {
        IEnumerable<Estudiante> lista = await db.QueryAsync<Estudiante, Inscripcion, Materia, Estudiante>(
            storedProcedure,
            (est, ins, mat) =>
            {
                if (!dict.TryGetValue(est.Id, out var entry))
                {
                    entry = est;
                    dict.Add(entry.Id, entry);
                }
                if (ins is not null)
                {
                    ins.Materia = mat;
                    entry.Inscripciones.Add(ins);
                }
                return entry;
            },
            new { userId },
            splitOn: "Id,Id",
            commandType: CommandType.StoredProcedure
        );
    }


}
