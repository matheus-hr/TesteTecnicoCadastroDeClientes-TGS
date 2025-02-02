using CadastroDeClientes.Domain.Context;
using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Repositories;
using CadastroDeClientes.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Infrastructure.Persistance.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CadastroDeClientesDbContext _context;
        private readonly IDapperContext<Usuario> _dapperContext;

        public UsuarioRepository(CadastroDeClientesDbContext context, IDapperContext<Usuario> dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public async Task<Usuario?> BuscarUsuarioPorId(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id && x.Ativo);
        }

        public async Task<Usuario?> BuscarUsuarioPorEmailESenha(string email, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Senha == senha && x.Ativo);
        }

        public async Task<Guid> CadastrarUsuario(Usuario usuario)
        {
            string sqlQuery = $@"EXEC CadastarUsuario
                                    @Id = '{Guid.NewGuid()}',
                                    @Nome = '{usuario.Nome}',
                                    @Email = '{usuario.Email}',
                                    @Senha = '{usuario.Senha}',
                                    @Role = '{usuario.Role.ToLower()}';";

            return await _dapperContext.Insert(sqlQuery);
        }
    }
}
