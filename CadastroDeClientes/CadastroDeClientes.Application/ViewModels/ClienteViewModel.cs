using CadastroDeClientes.Domain.Entities;

namespace CadastroDeClientes.Application.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel(Cliente cliente)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
            Email = cliente.Email;
            Logotipo = cliente.Logotipo;
            Logradouro = new LogradouroViewModel(cliente.ObterLogradouroAtivo());
        }

        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] Logotipo { get; set; } = []; 
        public LogradouroViewModel Logradouro { get; private set; } 
    }
}
