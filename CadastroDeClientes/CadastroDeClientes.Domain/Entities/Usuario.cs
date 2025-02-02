using CadastroDeClientes.Domain.DomainObjects;

namespace CadastroDeClientes.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public Usuario()
        {
            
        }

        public Usuario(string nome, string email, string senha, string role)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Role { get; private set; }
    }
}
