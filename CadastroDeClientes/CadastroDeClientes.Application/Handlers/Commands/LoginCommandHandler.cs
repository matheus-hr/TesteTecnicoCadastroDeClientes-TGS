using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeClientes.Application.Handlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GenerateToken(request);
            return await Task.FromResult(token);
        }
    }
}
