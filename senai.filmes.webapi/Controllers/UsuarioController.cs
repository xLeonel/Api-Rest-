using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.filmes.webapi.Domains;
using senai.filmes.webapi.Interfaces;
using senai.filmes.webapi.Repositories;

namespace senai.Filmes.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Post(UsuarioDomain loginJson)
        {
            UsuarioDomain usuarioSelecionado = _usuarioRepository.BuscarPorEmailSenha(loginJson.Email, loginJson.Senha);

            if (usuarioSelecionado == null)
            {
                return NotFound("E-mail ou senha inválido");
            }

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, usuarioSelecionado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioSelecionado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioSelecionado.Permissao),
                new Claim("Claim Personalizada", "teste")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Filmes.WebApi",
                audience: "Filmes.WebApi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            // payload
            System.Console.WriteLine("================================");
            foreach (var item in claims)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine("================================");

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }
    }
}