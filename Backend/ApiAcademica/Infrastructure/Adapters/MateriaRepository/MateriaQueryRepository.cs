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
        const string sql = @"SELECT * FROM Materias WHERE Id=@id;";
        return await db.QueryFirstOrDefaultAsync<Materia>(sql, new { id });
    }

    public async Task<List<Materia>> ObtenerTodosAsync()
    {
        using var db = _cf.CreateConnection();
        const string sql = @"
                        SELECT 
                            m.Id, m.Nombre, m.Creditos, m.ProfesorId,
                            p.Id, p.Nombre
                        FROM Materias m
                        JOIN Profesores p ON p.Id = m.ProfesorId;
                        ";

        IEnumerable<Materia> materias = await ObtenerMaterias(db, sql);

        return materias.AsList();
    }


    public async Task<List<Materia>> ObtenerPorIdsAsync(IEnumerable<Guid> ids)
    {
        if (!ids.Any()) return new List<Materia>();
        using IDbConnection db = _cf.CreateConnection();
        const string sql = @"SELECT * FROM Materias WHERE Id IN @ids;";
        var list = await db.QueryAsync<Materia>(sql, new { ids });
        return list.AsList();
    }

    private static async Task<IEnumerable<Materia>> ObtenerMaterias(IDbConnection db, string sql)
    {
        return await db.QueryAsync<Materia, Profesor, Materia>(
            sql,
            map: (materia, profesor) =>
            {
                materia.AsignarProfesor(profesor);
                return materia;
            },
            splitOn: "Id"
        );
    }

}