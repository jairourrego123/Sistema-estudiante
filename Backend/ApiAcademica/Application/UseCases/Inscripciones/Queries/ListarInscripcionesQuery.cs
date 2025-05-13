using Application.Dtos;
using Domain.Entities;
using MediatR;


namespace Application.UseCases.Inscripciones.Queries;


/// <summary>
/// Devuelve todas las inscripciones de un estudiante (con detalle de materia).
/// </summary>
public record ListarInscripcionesQuery(string UserId) : IRequest<List<InscripcionDetalleDto>>;