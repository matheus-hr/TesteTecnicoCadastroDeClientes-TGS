using MediatR;

namespace CadastroDeClientes.Application.Commands
{
    public class DeletarClienteCommand : IRequest
    {
        public DeletarClienteCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public Guid ClienteId { get;  set; }
    }
}
