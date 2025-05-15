using Application.UseCases.Profesores.Commands.CrearProfesor;
using Application.UseCases.Profesores.Commands.EditarProfesor;
using Application.UseCases.Profesores.Commands.EliminarProfesor;
using Application.UseCases.Profesores.Queries.ListarProfesores;
using Application.UseCases.Profesores.Queries.ObtenerProfesorPorIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/profesores")]
    [Authorize]
    public class ProfesoresController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfesoresController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProfesorCommand cmd)
        {
            Guid id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] EditarProfesorCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _mediator.Send(new EliminarProfesorCommand(id));
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<Profesor>>> Listar()
        {
            List<Profesor> list = await _mediator.Send(new ListarProfesoresQuery());
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Profesor>> ObtenerPorId(Guid id)
        {
            Profesor? p = await _mediator.Send(new ObtenerProfesorPorIdQuery(id));
            return p is null ? NotFound() : Ok(p);
        }
    }
}
