using CadastroDeClientes.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Application.Commands
{
    public class CadastrarClienteCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "Campo nome é obrigatorio")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo email é obrigatorio")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo logotipo é obrigatorio")]
        public byte[] Logotipo { get; set; } = [];
        public CadastrarClienteLogradouroCommand Logradouro { get; set; }
    }

    public class CadastrarClienteLogradouroCommand
    {
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

        public Logradouro ToLogradouroEntity()
        {
            return new Logradouro(Endereco, Cep, Bairro, Numero, Complemento, Cidade);
        }
    }
}
