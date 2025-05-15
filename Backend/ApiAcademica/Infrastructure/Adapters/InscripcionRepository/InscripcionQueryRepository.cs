using Domain.Entities;
using Domain.Ports.IInscripcionRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource;
using System.Data;
using Dapper;
using Application.Dtos;

namespace Infrastructure.Adapters.InscripcionRepository;

[Repository]
public class InscripcionQueryRepository : IInscripcionQueryRepository
{
    private readonly ISqlConnectionFactory _cf;

    public InscripcionQueryRepository(ISqlConnectionFactory cf)
        => _cf = cf;

    public async Task<List<Inscripcion>> ObtenerPorEstudianteAsync(Guid estudianteId)
    {
        using IDbConnection db = _cf.CreateConnection();

        const string storedProcedure = "paObtenerInscripcionesPorEstudiante";

        IEnumerable<InscripcionDetalleDto> resultado = await db.QueryAsync<InscripcionDetalleDto>(
            storedProcedure,
            new { EstudianteId = estudianteId },
            commandType: CommandType.StoredProcedure
        );

        List<Inscripcion> inscripciones = MapToInscripcion(resultado);

        return inscripciones;
    }


    private static List<Inscripcion> MapToInscripcion(IEnumerable<InscripcionDetalleDto> resultado)
    {
        return resultado.Select(row => new Inscripcion
        {
            Id = row.InscripcionId,
            EstudianteId = row.EstudianteId,
            MateriaId = row.MateriaId,
            Materia = new Materia(row.MateriaNombre, new Profesor { Nombre = row.ProfesorNombre })


        }).ToList();
    }
}