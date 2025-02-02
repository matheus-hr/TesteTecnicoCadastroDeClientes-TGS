using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{clienteId:guid}")]
        [Authorize(Roles = "funcionario,gerente,admin")]
        public async Task<IActionResult> Get(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                return BadRequest("Informe um id de cliente");

            var command = new BuscarClientePorIdQuery(clienteId);

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "funcionario,gerente,admin")]
        public async Task<IActionResult> GetAll([FromQuery] string? nome, [FromQuery] string? email)
        {
            var command = new BuscarTodosOsClienteQuery(nome, email);

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "gerente,admin")]
        public async Task<IActionResult> Post(CadastrarClienteCommand command)
        {
            var result = await _mediator.Send(command);
            return Created("Get", result);
        }

        [HttpPut("{clienteId:guid}")]
        [Authorize(Roles = "gerente,admin")]
        public async Task<IActionResult> Put([FromBody] AtualizarClienteCommand command, Guid clienteId)
        {
            if(clienteId == Guid.Empty)
                return BadRequest("Informe um id de cliente");

            command.AdicionarClienteId(clienteId);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{clienteId:guid}")]
        [Authorize(Roles = "gerente,admin")]
        public async Task<IActionResult> Delete(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                return BadRequest("Informe um id de cliente");

            var command = new DeletarClienteCommand(clienteId);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
