using CadastroDeClientes.Application.Queries;
using CadastroDeClientes.Application.ViewModels;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Queries
{
    public class BuscarUsuarioQueryHandler : IRequestHandler<BuscarUsuarioPorIdQuery, UsuarioViewModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public BuscarUsuarioQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioViewModel?> Handle(BuscarUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioPorId(request.Id);

            if (usuario == null)
                return null;

            return new UsuarioViewModel(usuario);
        }
    }
}
