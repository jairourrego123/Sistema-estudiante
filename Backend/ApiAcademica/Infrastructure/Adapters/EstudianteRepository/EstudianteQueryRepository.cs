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

    public async Task<Estudiante?> ObtenerPorUserIdAsync(string userId)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string sql = @"
                          SELECT e.*, i.Id, i.MateriaId, i.FechaGrabacion,
                                 m.Id, m.Nombre, m.Creditos, m.ProfesorId
                          FROM Estudiantes e
                          LEFT JOIN Inscripciones i ON i.EstudianteId = e.Id
                          LEFT JOIN Materias m      ON m.Id = i.MateriaId
                          WHERE e.userId = @userId;
                        ";
        Dictionary<Guid, Estudiante> dict = new Dictionary<Guid, Estudiante>();
        await ObtenerConsulta(userId, db, sql, dict);

        return dict.Values.FirstOrDefault();
    }

    public async Task<Estudiante?> ObtenerPorIdAsync(Guid id)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string sql = @"
                            SELECT Id,UserId,Nombre,Apellido,FechaGrabacion
                            FROM Estudiantes
                            WHERE Id = @Id;
                        ";

        return await db.QueryFirstOrDefaultAsync<Estudiante>(sql, new { Id = id });
    }


    public async Task<List<Estudiante>> ObtenerTodosAsync()
    {
        using IDbConnection db = _cf.CreateConnection();
        const string sql = @"SELECT * FROM Estudiantes;";
        var list = await db.QueryAsync<Estudiante>(sql);
        return list.AsList();
    }

    public async Task<List<string>> ListarCompañerosAsync(Guid materiaId, string userId)
    {
        using IDbConnection db = _cf.CreateConnection();
        const string sql = @"
                          SELECT DISTINCT e.Nombre + ' ' + e.Apellido
                          FROM Inscripciones i
                            JOIN Estudiantes e ON e.Id = i.EstudianteId
                          WHERE i.MateriaId=@materiaId
                            AND e.UserId<>@userId;
                        ";
        var list = await db.QueryAsync<string>(sql, new { materiaId, userId });
        return list.AsList();
    }

    private static async Task ObtenerConsulta(string userId, IDbConnection db, string sql, Dictionary<Guid, Estudiante> dict)
    {
        IEnumerable<Estudiante> lista = await db.QueryAsync<Estudiante, Inscripcion, Materia, Estudiante>(
            sql,
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
            splitOn: "Id,Id"
        );
    }

}
