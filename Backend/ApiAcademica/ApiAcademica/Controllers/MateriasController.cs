using Application.Dtos;
using Application.UseCases.Materias.Commands;
using Application.UseCases.Materias.Commands.CrearMaterias;
using Application.UseCases.Materias.Commands.EditarMaterias;
using Application.UseCases.Materias.Commands.EliminarMateria;
using Application.UseCases.Materias.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/materias")]
    [Authorize]
    public class MateriasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MateriasController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearMateriaCommand cmd)
        {
            await _mediator.Send(cmd);
            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Editar(Guid id,[FromBody] EditarMateriaCommand cmd)
        {
           
            await _mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _mediator.Send(new EliminarMateriaCommand(id));
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<MateriaConProfesorDto>>> Listar()
        {
            List<MateriaConProfesorDto> lista = await _mediator.Send(new ListarMateriasQuery());
            return Ok(lista);
        }

        
    }
}
