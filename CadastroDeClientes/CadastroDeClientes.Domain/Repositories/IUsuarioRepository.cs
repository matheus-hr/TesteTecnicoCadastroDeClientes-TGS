using CadastroDeClientes.Domain.Entities;

namespace CadastroDeClientes.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> BuscarUsuarioPorId(Guid id);

        Task<Usuario?> BuscarUsuarioPorEmailESenha(string email, string senha);

        Task<Guid> CadastrarUsuario(Usuario usuario);
    }
}
