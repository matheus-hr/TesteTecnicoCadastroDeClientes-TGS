using CadastroDeClientes.Domain.Entities;

namespace CadastroDeClientes.Domain.Repositories
{
    public interface IClienteRepository
    {
        public Task<Cliente?> BuscarClientePorId(Guid id);

        public Task<IEnumerable<Cliente>> BuscarTodosOsClientes(string nome, string email);

        public Task<bool> AtualizarCliente(Cliente cliente);

        public Task<Guid> CadastrarCliente(Cliente cliente);

        public Task<bool> DeletarCliente(Guid clienteId);
    }
}
