
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.UseCases.Estudiantes.Commands.CrearEstudiante;
using Application.UseCases.Estudiantes.Commands.EditarEstudiante;
using Application.UseCases.Estudiantes.Commands.EliminarEstudiante;
using Application.UseCases.Estudiantes.Queries.ListarEstudiante;
using Application.UseCases.Estudiantes.Queries.ObtenerCompañeros;
using Application.UseCases.Estudiantes.Queries.ObtenerEstudiantePorId;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/estudiantes")]
    //[Authorize]
    public class EstudiantesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EstudiantesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearEstudianteCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] EditarEstudianteCommand cmd)
        {
            await _mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _mediator.Send(new EliminarEstudianteCommand(id));
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<Estudiante>>> Listar()
        {
            var list = await _mediator.Send(new ListarEstudiantesQuery());
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Estudiante>> ObtenerPorId(Guid id)
        {
            var e = await _mediator.Send(new ObtenerEstudiantePorIdQuery(id));
            return e is null ? NotFound() : Ok(e);
        }

        [HttpGet("companeros")]
        public async Task<ActionResult<List<string>>> Companeros(
            [FromQuery] string userId)
        {
            var list = await _mediator.Send(new ListarCompanerosQuery(userId));
            return Ok(list);
        }
    }
}
