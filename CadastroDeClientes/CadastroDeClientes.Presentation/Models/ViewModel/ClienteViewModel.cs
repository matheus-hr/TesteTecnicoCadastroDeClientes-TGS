namespace CadastroDeClientes.Presentation.Models.ViewModel
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] Logotipo { get; set; } = [];
        public LogradouroViewModel Logradouro { get; set; }
    }
}
