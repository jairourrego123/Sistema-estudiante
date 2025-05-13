using Application.UseCases.Inscripciones.Command.DesinscribirMaterias;
using Application.UseCases.Inscripciones.Command.InscribirMaterias;
using Application.UseCases.Inscripciones.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/inscripciones")]
    //[Authorize]
    public class InscripcionesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InscripcionesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Inscribir([FromBody] InscribirMateriasCommand cmd)
        {
            await _mediator.Send(cmd);
            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> Desinscribir([FromBody] DesinscribirMateriaCommand cmd)
        {
            await _mediator.Send(cmd);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<Inscripcion>>> Listar([FromQuery] string userId)
        {
            var lista = await _mediator.Send(new ListarInscripcionesQuery(userId));
            return Ok(lista);
        }

       
    }
}
