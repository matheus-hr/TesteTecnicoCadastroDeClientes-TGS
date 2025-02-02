using CadastroDeClientes.Application.ViewModels;
using MediatR;

namespace CadastroDeClientes.Application.Queries
{
    public class BuscarUsuarioPorIdQuery : IRequest<UsuarioViewModel>
    {
        public BuscarUsuarioPorIdQuery(Guid usuarioId)
        {
            Id = usuarioId;
        }

        public Guid Id { get; set; }
    }
}
