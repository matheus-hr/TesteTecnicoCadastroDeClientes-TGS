using CadastroDeClientes.Domain.Entities;

namespace CadastroDeClientes.Domain.Repositories
{
    public interface ILogradouroRepository
    {
        Task<Guid> CriarLogradouro(Logradouro logradouro);

        Task<Logradouro> BuscarLogradouro(Guid clienteId);

        Task<bool> AtualizarLogradouro(Logradouro logradouro);
        Task<bool> DeletarLogradouro(Guid clienteId);
    }
}
