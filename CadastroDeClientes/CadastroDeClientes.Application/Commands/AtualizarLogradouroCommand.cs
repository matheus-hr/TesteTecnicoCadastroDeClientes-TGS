using CadastroDeClientes.Application.ViewModels;
using MediatR;

namespace CadastroDeClientes.Application.Commands
{
    public class AtualizarLogradouroCommand : IRequest
    {
        public string? Endereco { get; set; }
        public string? Cep { get; set; }
        public string? Bairro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Cidade { get; set; }
        public Guid ClienteId { get; private set; }

        public void SetClienteId(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
