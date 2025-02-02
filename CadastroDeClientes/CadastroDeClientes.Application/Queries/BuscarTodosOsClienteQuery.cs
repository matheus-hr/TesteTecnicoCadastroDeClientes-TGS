using CadastroDeClientes.Application.ViewModels;
using MediatR;

namespace CadastroDeClientes.Application.Queries
{
    public class BuscarTodosOsClienteQuery : IRequest<IEnumerable<ClienteViewModel>>
    {
        public BuscarTodosOsClienteQuery(string? nome, string? email)
        {
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
    }
}
