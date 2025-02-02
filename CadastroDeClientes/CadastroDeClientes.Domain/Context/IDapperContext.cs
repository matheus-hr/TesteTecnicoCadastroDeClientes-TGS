namespace CadastroDeClientes.Domain.Context
{
    public interface IDapperContext<T>
    {
        public Task<Guid> Insert(string sql);

        public Task<bool> Update(string sql);

        public Task<bool> Delete(string sql);
    }
}
