using CadastroDeClientes.Application.Queries;
using CadastroDeClientes.Application.ViewModels;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Queries
{
    public class BuscarClientePorIdQueryHandler : IRequestHandler<BuscarClientePorIdQuery, ClienteViewModel>
    {
        private readonly IClienteRepository _clienteRepository;

        public BuscarClientePorIdQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteViewModel> Handle(BuscarClientePorIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.BuscarClientePorId(request.Id);

            if (cliente == null)
                return null;

            return new ClienteViewModel(cliente);
        }
    }
}
