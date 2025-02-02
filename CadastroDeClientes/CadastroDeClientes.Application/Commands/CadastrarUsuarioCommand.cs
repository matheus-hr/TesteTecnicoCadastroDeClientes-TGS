using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Application.Commands
{
    public class CadastrarUsuarioCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "Campo nome é obrigatorio")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo email é obrigatorio")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo senha é obrigatorio")]
        public string Senha { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo role é obrigatorio")]
        public string Role { get; set; } = string.Empty;
    }
}
