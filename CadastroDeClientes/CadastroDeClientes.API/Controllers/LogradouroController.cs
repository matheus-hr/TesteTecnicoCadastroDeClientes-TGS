using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogradouroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogradouroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{clienteId:Guid}")]
        [Authorize(Roles = "gerente,admin")]
        public async Task<IActionResult> Post([FromBody] CadastrarLogradouroCommand command, Guid clienteId)
        {
            if(clienteId == Guid.Empty) 
                return BadRequest("O campo clienteId é obrigatorio");

            command.SetClienteId(clienteId);

            Guid id = await _mediator.Send(command);

            if (id == Guid.Empty)
                return BadRequest();

            return Created("Get", id);
        }

        [HttpPut("{clienteId:Guid}")]
        [Authorize(Roles = "gerente,admin")]
        public async Task<IActionResult> Put([FromBody] AtualizarLogradouroCommand command, Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                return BadRequest("O campo clienteId é obrigatorio");

            command.SetClienteId(clienteId);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{clienteId:Guid}")]
        [Authorize(Roles = "gerente,admin")]
        public async Task<IActionResult> Delete(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                return BadRequest("O campo clienteId é obrigatorio");

            var command = new DeletarLogradouroCommand(clienteId);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{clienteId:Guid}")]
        [Authorize(Roles = "funcionario,gerente,admin")]
        public async Task<IActionResult> Get(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                return BadRequest("O campo clienteId é obrigatorio");

            var command = new BuscarLogradouroPorClienteQuery(clienteId);
            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
