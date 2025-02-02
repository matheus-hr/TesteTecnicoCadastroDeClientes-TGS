using CadastroDeClientes.Domain.DomainObjects;

namespace CadastroDeClientes.Domain.Entities
{
    public class Logradouro : BaseEntity
    {
        public Logradouro()
        {
            
        }

        public Logradouro(string endereco, string cep, string bairro, string numero, string complemento, string cidade)
        {
            Id = Guid.NewGuid();
            Endereco = endereco;
            Cep = cep;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
        }

        public Guid Id { get; private set; }
        public string Endereco { get; private set; }
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public Guid ClienteId { get; private set; }

        public void SetClienteId(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
