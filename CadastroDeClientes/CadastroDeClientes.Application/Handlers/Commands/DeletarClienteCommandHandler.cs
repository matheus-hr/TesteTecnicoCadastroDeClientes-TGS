using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class DeletarClienteCommandHandler : IRequestHandler<DeletarClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;

        public DeletarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Handle(DeletarClienteCommand request, CancellationToken cancellationToken)
        {
            await _clienteRepository.DeletarCliente(request.ClienteId);
        }
    }
}
