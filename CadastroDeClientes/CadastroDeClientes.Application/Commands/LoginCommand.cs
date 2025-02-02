using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Application.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public LoginCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        [Required(ErrorMessage = "O campo email é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        public string Senha { get; set; }
    }
}
