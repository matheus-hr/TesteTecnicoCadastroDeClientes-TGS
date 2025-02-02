using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Domain.DomainObjects;
using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class CadastrarLogradouroCommandHandler : IRequestHandler<CadastrarLogradouroCommand, Guid>
    {
        private readonly ILogradouroRepository _logradouroRepository;

        public CadastrarLogradouroCommandHandler(ILogradouroRepository logradouroRepository)
        {
            _logradouroRepository = logradouroRepository;
        }

        public async Task<Guid> Handle(CadastrarLogradouroCommand request, CancellationToken cancellationToken)
        {
            var logradouroAtual = await _logradouroRepository.BuscarLogradouro(request.ClienteId);

            if (logradouroAtual != null)
                throw new DomainException("Já existe um logradouro para o cliente atual");

            var logradouro = new Logradouro(request.Endereco, request.Cep, request.Bairro, request.Numero, request.Complemento, request.Cidade);
            logradouro.SetClienteId(request.ClienteId);

            return await _logradouroRepository.CriarLogradouro(logradouro);
        }
    }
}
