using CadastroDeClientes.Domain.Entities;

namespace CadastroDeClientes.Application.ViewModels
{
    public class LogradouroViewModel
    {
        public LogradouroViewModel(Logradouro logradouro)
        {
            if(logradouro != null)
            {
                Endereco = logradouro.Endereco;
                Cep = logradouro.Cep;
                Bairro = logradouro.Bairro;
                Numero = logradouro.Numero;
                Complemento = logradouro.Complemento;
                Cidade = logradouro.Cidade;
            }
        }

        public string Endereco { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
    }
}
