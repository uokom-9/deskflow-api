using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeskFlow.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;  // Injeção de dependência para gerenciar usuários
        private readonly IConfiguration _configuration;  // Injeção de dependência para acessar as configurações do appsettings.json

        public AuthController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto modelo)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(modelo.Email);

            if (usuarioExistente != null)
            {
                return BadRequest(new
                {
                    message = "Este e-mail já está cadastrado."
                });
            }

            var usuario = new IdentityUser
            {
                UserName = modelo.Email,
                Email = modelo.Email
            };

            var resultado = await _userManager.CreateAsync(
                usuario,
                modelo.Password);

            if (!resultado.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Não foi possível criar o usuário.",
                    errors = resultado.Errors.Select(c => c.Description)
                });
            }

            return StatusCode(201, new
            {
                message = "Usuário criado com sucesso."
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto modelo)
        {
            var usuario = await _userManager.FindByEmailAsync(modelo.Email);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    message = "Credenciais inválidas!"
                });
            }

            var senhaValida = await _userManager.CheckPasswordAsync(
                usuario,
                modelo.Password);

            if (!senhaValida)
            {
                return Unauthorized(new
                {
                    message = "Credenciais inválidas!"
                });
            }

            var token = GerarToken(usuario);

            return Ok(new
            {
                accessToken = token
            });
        }

        private string GerarToken(IdentityUser usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var chave = Encoding.UTF8.GetBytes(
                _configuration["JwtSettings:SecretKey"]!);

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id),

                new Claim(
                    ClaimTypes.Email,
                    usuario.Email!)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(2),

                Issuer = _configuration["JwtSettings:Issuer"],

                Audience = _configuration["JwtSettings:Audience"],

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    public record RegisterDto(string Email, string Password);

    public record LoginDto(string Email, string Password);
}