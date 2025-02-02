using CadastroDeClientes.Application.ViewModels;
using MediatR;

namespace CadastroDeClientes.Application.Queries
{
    public class BuscarLogradouroPorClienteQuery : IRequest<LogradouroViewModel>
    {
        public BuscarLogradouroPorClienteQuery(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public Guid ClienteId { get; set; }
    }
}
