using CadastroDeClientes.Application.Queries;
using CadastroDeClientes.Application.ViewModels;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Queries
{
    public class BuscarTodosOsClienteQueryHandler : IRequestHandler<BuscarTodosOsClienteQuery, IEnumerable<ClienteViewModel>>
    {
        private readonly IClienteRepository _clienteRepository;

        public BuscarTodosOsClienteQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteViewModel>> Handle(BuscarTodosOsClienteQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.BuscarTodosOsClientes(request.Nome, request.Email);
            return clientes.Select(x => new ClienteViewModel(x));
        }
    }
}
