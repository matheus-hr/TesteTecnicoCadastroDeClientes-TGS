using CadastroDeClientes.Domain.Entities;

namespace CadastroDeClientes.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel(Usuario usuario)
        {
            Nome = usuario.Nome;
            Email = usuario.Email;
            Role = usuario.Role;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
    }
}
