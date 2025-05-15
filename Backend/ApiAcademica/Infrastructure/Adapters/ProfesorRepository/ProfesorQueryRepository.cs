using Dapper;
using Domain.Ports.IProfesorRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource;
using System.Data;


namespace Infrastructure.Adapters.ProfesorRepository;

[Repository]
public class ProfesorQueryRepository : IProfesorQueryRepository
{
    private readonly ISqlConnectionFactory _cf;
    public ProfesorQueryRepository(ISqlConnectionFactory cf) => _cf = cf;

    public async Task<Profesor?> ObtenerPorIdAsync(Guid id)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerProfesorPorId";

        Dictionary<Guid, Profesor> dict = new Dictionary<Guid, Profesor>();
        await ObtenerConsulta(id, db, storedProcedure, dict);

        return dict.Values.FirstOrDefault();
    }

    private static async Task ObtenerConsulta(Guid id, IDbConnection db, string storedProcedure, Dictionary<Guid, Profesor> dict)
    {
        IEnumerable<Profesor> lista = await db.QueryAsync<Profesor, Materia, Profesor>(
            storedProcedure,
            (prof, mat) =>
            {
                if (!dict.TryGetValue(prof.Id, out var entry))
                {
                    entry = prof;
                    dict.Add(entry.Id, entry);
                }
                if (mat is not null)
                {
                    entry.Materias.Add(mat);
                }
                return entry;
            },
            new { id },
            splitOn: "Id",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<List<Profesor>> ObtenerTodosAsync()
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerTodosLosProfesores";

        IEnumerable<Profesor> list = await db.QueryAsync<Profesor>(
            storedProcedure,
            commandType: CommandType.StoredProcedure
        );

        return list.AsList();
    }
}