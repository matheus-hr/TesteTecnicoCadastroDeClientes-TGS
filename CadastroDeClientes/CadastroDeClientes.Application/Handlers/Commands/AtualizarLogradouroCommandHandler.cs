using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class AtualizarLogradouroCommandHandler : IRequestHandler<AtualizarLogradouroCommand>
    {
        private readonly ILogradouroRepository _logradouroRepository;

        public AtualizarLogradouroCommandHandler(ILogradouroRepository logradouroRepository)
        {
            _logradouroRepository = logradouroRepository;
        }

        public async Task Handle(AtualizarLogradouroCommand request, CancellationToken cancellationToken)
        {
            var logradouro = new Logradouro(request.Endereco, request.Cep, request.Bairro, request.Numero, request.Complemento, request.Cidade);
            logradouro.SetClienteId(request.ClienteId);

            await _logradouroRepository.AtualizarLogradouro(logradouro);
        }
    }
}
