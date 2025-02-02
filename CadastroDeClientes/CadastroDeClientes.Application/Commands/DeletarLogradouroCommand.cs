using MediatR;

namespace CadastroDeClientes.Application.Commands
{
    public class DeletarLogradouroCommand : IRequest
    {
        public DeletarLogradouroCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public Guid ClienteId { get; set; }
    }
}
