namespace CadastroDeClientes.Domain.DomainObjects
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
        }

        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }
    }
}
