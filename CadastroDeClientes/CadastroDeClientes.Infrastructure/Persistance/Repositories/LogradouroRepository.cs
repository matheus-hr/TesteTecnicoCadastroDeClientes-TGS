using CadastroDeClientes.Domain.Context;
using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Repositories;
using CadastroDeClientes.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Infrastructure.Persistance.Repositories
{
    public class LogradouroRepository : ILogradouroRepository
    {
        private readonly CadastroDeClientesDbContext _context;
        private readonly IDapperContext<Logradouro> _dapperContext;

        public LogradouroRepository(CadastroDeClientesDbContext context, IDapperContext<Logradouro> dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public async Task<Logradouro> BuscarLogradouro(Guid clienteId)
        {
            return await _context.Logradouros.FirstOrDefaultAsync(x => x.ClienteId == clienteId && x.Ativo);
        }

        public async Task<Guid> CriarLogradouro(Logradouro logradouro)
        {
            string sqlQuery = $@"EXEC CadastrarLogradouro
                        @Id = '{logradouro.Id}',
                        @Endereco = '{logradouro.Endereco}',
                        @Cep = '{logradouro.Cep}',
                        @Bairro = '{logradouro.Bairro}',
                        @Numero = '{logradouro.Numero}',
                        @Complemento = '{logradouro.Complemento}',
                        @Cidade = '{logradouro.Cidade}',
                        @ClienteId = '{logradouro.ClienteId}'";

            return await _dapperContext.Insert(sqlQuery);
        }

        public async Task<bool> AtualizarLogradouro(Logradouro logradouro)
        {
            string sqlQuery = @$"EXEC AtualizarLogradouro
                @ClienteId = '{logradouro.ClienteId}',
                @Endereco = {(string.IsNullOrWhiteSpace(logradouro.Endereco) ? "NULL" : "'" + logradouro.Endereco + "'")},
                @Cep = {(string.IsNullOrWhiteSpace(logradouro.Cep) ? "NULL" : "'" + logradouro.Cep + "'")},
                @Bairro = {(string.IsNullOrWhiteSpace(logradouro.Bairro) ? "NULL" : "'" + logradouro.Bairro + "'")},
                @Numero = {(string.IsNullOrWhiteSpace(logradouro.Numero) ? "NULL" : "'" + logradouro.Numero + "'")},
                @Complemento = {(string.IsNullOrWhiteSpace(logradouro.Complemento) ? "NULL" : "'" + logradouro.Complemento + "'")},
                @Cidade = {(string.IsNullOrWhiteSpace(logradouro.Cidade) ? "NULL" : "'" + logradouro.Cidade + "'")};";

            return await _dapperContext.Update(sqlQuery);
        }

        public async Task<bool> DeletarLogradouro(Guid clienteId)
        {
            string sqlQuery = @$"EXEC DesativarLogradouro @ClienteId = '{clienteId}';";

            return await _dapperContext.Delete(sqlQuery);
        }
    }
}
