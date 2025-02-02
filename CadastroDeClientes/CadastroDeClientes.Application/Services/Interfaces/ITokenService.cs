using CadastroDeClientes.Application.Commands;

namespace CadastroDeClientes.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(LoginCommand usuario);
    }
}
