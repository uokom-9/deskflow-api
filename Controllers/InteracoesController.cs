using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.DTOs;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers
{
    [ApiController]
    [Route("api/chamados/{chamadoId}/interacoes")]
    public class InteracoesController : ControllerBase
    {
        private readonly InteracaoService _service;

        public InteracoesController(InteracaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
    int chamadoId,
    [FromBody] CriarInteracaoDto dto)
        {
            var interacao = new Interacao
            {
                Autor = dto.Autor,
                Mensagem = dto.Mensagem
            };

            await _service.AdicionarInteracaoAsync(chamadoId, interacao);

            return NoContent();
        }
    }
}