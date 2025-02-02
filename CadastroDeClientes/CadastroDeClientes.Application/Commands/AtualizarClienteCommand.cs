using MediatR;

namespace CadastroDeClientes.Application.Commands
{
    public class AtualizarClienteCommand : IRequest
    {
        public Guid ClienteId { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public byte[]? Logotipo { get; set; }

        public void AdicionarClienteId(Guid clienteId) 
        {
            ClienteId = clienteId;
        }
    }
}
