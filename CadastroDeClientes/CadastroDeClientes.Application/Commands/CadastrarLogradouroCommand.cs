using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Application.Commands
{
    public class CadastrarLogradouroCommand : IRequest<Guid>
    {
        public Guid ClienteId { get; private set; }

        [Required(ErrorMessage = "Campo endereco é obrigatorio")]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo cep é obrigatorio")]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo bairro é obrigatorio")]
        public string Bairro { get; set; } = string.Empty;

        public string? Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo cidade é obrigatorio")]
        public string Cidade { get; set; } = string.Empty;

        public void SetClienteId(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
