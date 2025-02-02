using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class DeletarLogradouroCommandHandler : IRequestHandler<DeletarLogradouroCommand>
    {
        private readonly ILogradouroRepository _logradouroRepository;

        public DeletarLogradouroCommandHandler(ILogradouroRepository logradouroRepository)
        {
            _logradouroRepository = logradouroRepository;
        }

        public async Task Handle(DeletarLogradouroCommand request, CancellationToken cancellationToken)
        {
            await _logradouroRepository.DeletarLogradouro(request.ClienteId);
        }
    }
}
