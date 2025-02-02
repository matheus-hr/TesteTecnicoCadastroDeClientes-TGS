using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CadastrarUsuarioCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == Guid.Empty)
                return BadRequest();

            return Created("Get", result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] Guid usuarioId)
        {
            if (usuarioId == Guid.Empty)
                return BadRequest(ModelState);

            var command = new BuscarUsuarioPorIdQuery(usuarioId);

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Created("Get", result);
        }
    }
}
