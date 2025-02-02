using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, Guid>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(request.Nome, request.Email, request.Senha, request.Role);

            Guid id = await _usuarioRepository.CadastrarUsuario(usuario);
            return id;
        }
    }
}
