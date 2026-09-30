using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly ChamadoService _service;

        public ChamadosController(ChamadoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Chamado chamado)
        {
            await _service.CriarAsync(chamado);
            return Created($"/api/chamados/{chamado.Id}", chamado);
        }

        // Obter chamado com Categoria e Interações
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chamado = await _service.BuscarPorIdAsync(id);
            return Ok(chamado);
        }

        [HttpGet]
        public async Task<IActionResult> GetWithFilters(
            [FromQuery] string? status, 
            [FromQuery] string? prioridade, 
            [FromQuery] int? categoriaId)
        {
            var chamados = await _service.ListarComFiltrosAsync(status, prioridade, categoriaId);
            return Ok(chamados);
        }

        [HttpPost("{id:int}/iniciar")]
        public async Task<IActionResult> IniciarAtendimento(int id)
        {
            await _service.IniciarAtendimentoAsync(id);
            return NoContent();
        }

        [HttpPost("{id:int}/encerrar")]
        public async Task<IActionResult> EncerrarChamado(int id, [FromBody] string solucao)
        {
            await _service.EncerrarChamadoAsync(id, solucao);
            return NoContent();
        }
    }
}
