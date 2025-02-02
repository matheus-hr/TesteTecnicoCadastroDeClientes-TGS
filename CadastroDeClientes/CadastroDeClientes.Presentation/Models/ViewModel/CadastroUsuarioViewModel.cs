using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Presentation.Models.ViewModel
{
    public class CadastroUsuarioViewModel
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
