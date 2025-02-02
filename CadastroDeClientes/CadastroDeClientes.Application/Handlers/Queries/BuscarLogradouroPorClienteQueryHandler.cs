using CadastroDeClientes.Application.Queries;
using CadastroDeClientes.Application.ViewModels;
using CadastroDeClientes.Domain.Repositories;
using MediatR;

namespace CadastroDeClientes.Application.Handlers.Queries
{
    public class BuscarLogradouroPorClienteQueryHandler : IRequestHandler<BuscarLogradouroPorClienteQuery, LogradouroViewModel>
    {
        private readonly ILogradouroRepository _logradouroRepository;

        public BuscarLogradouroPorClienteQueryHandler(ILogradouroRepository logradouroRepository)
        {
            _logradouroRepository = logradouroRepository;
        }

        public async Task<LogradouroViewModel> Handle(BuscarLogradouroPorClienteQuery request, CancellationToken cancellationToken)
        {
            var logradouro = await _logradouroRepository.BuscarLogradouro(request.ClienteId);

            if (logradouro == null)
                return null;

            return new LogradouroViewModel(logradouro);
        }
    }
}
