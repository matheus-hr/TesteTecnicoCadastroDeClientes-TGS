using CadastroDeClientes.Domain.Context;
using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Repositories;
using CadastroDeClientes.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Infrastructure.Persistance.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CadastroDeClientesDbContext _dbContext;
        private readonly IDapperContext<Cliente> _dapperContext;

        public ClienteRepository(CadastroDeClientesDbContext context, IDapperContext<Cliente> dapperContext)
        {
            _dbContext = context;
            _dapperContext = dapperContext;
        }

        public async Task<Cliente?> BuscarClientePorId(Guid id)
        {
            return await _dbContext.Clientes
                .Include(x => x.Logradouros.Where(l => l.Ativo)) 
                .Where(x => x.Id == id && x.Ativo)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosOsClientes(string nome, string email)
        {
            return await _dbContext.Clientes
                .Include(x => x.Logradouros.Where(l => l.Ativo)) 
                .Where(x => (string.IsNullOrWhiteSpace(nome) || x.Nome == nome) &&
                            (string.IsNullOrWhiteSpace(email) || x.Nome == email) && x.Ativo)
                .ToListAsync();
        }

        public async Task<Guid> CadastrarCliente(Cliente cliente)
        {
            var logradouroAtivo = cliente.ObterLogradouroAtivo();

            string logotipoHex = BitConverter.ToString(cliente.Logotipo).Replace("-", "");

            string sqlQuery = $@"EXEC CadastrarCliente
                                    @ClienteId = '{cliente.Id}',
                                    @Nome = '{cliente.Nome}',
                                    @Email = '{cliente.Email}',
                                    @Logotipo = 0x{logotipoHex},
                                    @LogradouroId = '{logradouroAtivo.Id}',
                                    @Endereco = '{logradouroAtivo.Endereco}',
                                    @Cep = '{logradouroAtivo.Cep}',
                                    @Bairro = '{logradouroAtivo.Bairro}',
                                    @Numero = '{logradouroAtivo.Numero}',
                                    @Complemento = '{logradouroAtivo.Complemento}',
                                    @Cidade = '{logradouroAtivo.Cidade}';";

            Guid clienteId = await _dapperContext.Insert(sqlQuery);

            return clienteId;
        }

        public async Task<bool> AtualizarCliente(Cliente cliente)
        {
            var logradouroAtivo = cliente.ObterLogradouroAtivo();

            string logotipoHex = BitConverter.ToString(cliente.Logotipo).Replace("-", "");

            string sqlQuery = @$"EXEC AtualizarCliente
                    @ClienteId = '{cliente.Id}', 
                    @Nome = '{cliente.Nome}', 
                    @Email = '{cliente.Email}', 
                    @Logotipo = 0x{logotipoHex}, 
                    @Endereco = '{logradouroAtivo.Endereco}', 
                    @Cep = '{logradouroAtivo.Cep}', 
                    @Bairro = '{logradouroAtivo.Bairro}', 
                    @Numero = '{logradouroAtivo.Numero}', 
                    @Complemento = '{logradouroAtivo.Complemento}',
                    @Cidade = '{logradouroAtivo.Cidade}';";


            return await _dapperContext.Update(sqlQuery);
        }

        public async Task<bool> DeletarCliente(Guid clienteId)
        {
            string sqlQuery = $"EXEC DesativarCliente @ClienteId = '{clienteId}'";

            return await _dapperContext.Delete(sqlQuery);
        }
    }
}
