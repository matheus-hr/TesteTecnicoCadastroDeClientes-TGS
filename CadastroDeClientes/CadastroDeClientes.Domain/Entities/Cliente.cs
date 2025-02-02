using CadastroDeClientes.Domain.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeClientes.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public Cliente()
        {

        }

        public Cliente(string nome, string email, byte[] logotipo, Logradouro logradouro)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Logotipo = logotipo;
            Logradouros.Add(logradouro);
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public byte[] Logotipo { get; private set; }
        public List<Logradouro> Logradouros { get; private set; } = new List<Logradouro>();

        public void AtualizarDados(string nome, string email, byte[] logotipo)
        {
            Nome = nome;
            Email = email;
            Logotipo = logotipo;
        }

        public Logradouro ObterLogradouroAtivo()
        {
            return Logradouros.FirstOrDefault(l => l.Ativo);
        }
    }
}
