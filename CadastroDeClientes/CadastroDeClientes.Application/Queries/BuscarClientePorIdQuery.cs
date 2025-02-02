using CadastroDeClientes.Application.ViewModels;
using MediatR;

namespace CadastroDeClientes.Application.Queries
{
    public class BuscarClientePorIdQuery : IRequest<ClienteViewModel>
    {
        public BuscarClientePorIdQuery(Guid clienteId)
        {
            Id = clienteId;
        }

        public Guid Id { get; set; }
    }
}
