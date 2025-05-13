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
        const string sql = @"
                      SELECT p.*, m.Id, m.Nombre, m.Creditos, m.ProfesorId
                      FROM Profesores p
                      LEFT JOIN Materias m ON m.ProfesorId = p.Id
                      WHERE p.Id = @id;";

        Dictionary<Guid, Profesor> dict = new Dictionary<Guid, Profesor>();
        await ObtenerConsulta(id, db, sql, dict);
        return dict.Values.FirstOrDefault();
    }

    private static async Task ObtenerConsulta(Guid id, IDbConnection db, string sql, Dictionary<Guid, Profesor> dict)
    {
        IEnumerable<Profesor> lista = await db.QueryAsync<Profesor, Materia, Profesor>(
            sql,
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
            splitOn: "Id"
        );
    }

    public async Task<List<Profesor>> ObtenerTodosAsync()
    {
        using IDbConnection db = _cf.CreateConnection();
        const string sql = "SELECT * FROM Profesores;";
        IEnumerable<Profesor> list = await db.QueryAsync<Profesor>(sql);
        return list.AsList();
    }
}