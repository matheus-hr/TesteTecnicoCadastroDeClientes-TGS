using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Presentation.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo email é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        public string Senha { get; set; }
    }
}
