using DeskFlow.API.Models.Entities;
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
            [FromRoute(Name = "chamadoId")] int chamadoId,
            [FromBody] Interacao interacao)
        {
            await _service.AdicionarInteracaoAsync(chamadoId, interacao);

            return NoContent();
        }
    }
}