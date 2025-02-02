using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class CadastrarClienteCommandHandler : IRequestHandler<CadastrarClienteCommand, Guid>
    {
        private readonly IClienteRepository _clienteRepository;

        public CadastrarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Guid> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(request.Nome, request.Email, request.Logotipo, request.Logradouro.ToLogradouroEntity());

            var clienteId = await _clienteRepository.CadastrarCliente(cliente);
            return clienteId;
        }
    }
}
