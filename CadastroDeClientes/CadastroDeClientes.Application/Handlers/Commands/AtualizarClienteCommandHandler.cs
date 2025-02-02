using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;

        public AtualizarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.BuscarClientePorId(request.ClienteId);

            if (cliente == null)
                return;

            byte[] logotipo = [];

            if(request.Logotipo == null || request.Logotipo.Length == 0)
                logotipo = cliente.Logotipo;

            cliente.AtualizarDados(request.Nome, request.Email, logotipo);

            await _clienteRepository.AtualizarCliente(cliente);

            return;
        }
    }
}
