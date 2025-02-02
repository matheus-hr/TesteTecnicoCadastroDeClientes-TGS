using CadastroDeClientes.Application.Commands;
using CadastroDeClientes.Application.Services.Interfaces;
using CadastroDeClientes.Domain.Repositories;
using CadastroDeClientes.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroDeClientes.Application.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public TokenService(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> GenerateToken(LoginCommand usuarioLogin)
        {
            var usuarioDatabase = await _usuarioRepository.BuscarUsuarioPorEmailESenha(usuarioLogin.Email, usuarioLogin.Senha);

            if (usuarioDatabase == null)
                return string.Empty;

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioDatabase.Nome),
                new Claim(ClaimTypes.Email, usuarioDatabase.Email),
                new Claim(ClaimTypes.Role, usuarioDatabase.Role)
            };

            var tokenOptions = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.Now.AddHours(8),
                    signingCredentials: signInCredentials,
                    claims: claims
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
    }
}
