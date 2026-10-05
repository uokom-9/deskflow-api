using DeskFlow.API.Models.DTOs;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers
{
    [ApiController]
    [Route("api/chamados")]
    public class ChamadosController : ControllerBase
    {
        private readonly ChamadoService _service;

        public ChamadosController(ChamadoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarChamadoDto dto)
        {
            var chamado = new Chamado
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Prioridade = dto.Prioridade,
                SolicitanteNome = dto.SolicitanteNome,
                CategoriaId = dto.CategoriaId
            };

            await _service.CriarAsync(chamado);

            return Created(
                $"/api/chamados/{chamado.Id}",
                ConverterParaDto(chamado)
            );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chamado = await _service.BuscarPorIdAsync(id);

            return Ok(ConverterParaDto(chamado));
        }

        [HttpGet]
        public async Task<IActionResult> GetWithFilters(
            [FromQuery] string? status,
            [FromQuery] string? prioridade,
            [FromQuery] int? categoriaId)
        {
            var chamados = await _service.ListarComFiltrosAsync(
                status,
                prioridade,
                categoriaId
            );

            var chamadosDto = chamados
                .Select(ConverterParaDto)
                .ToList();

            return Ok(chamadosDto);
        }

        [HttpPost("{id:int}/iniciar")]
        public async Task<IActionResult> IniciarAtendimento(int id)
        {
            await _service.IniciarAtendimentoAsync(id);

            return NoContent();
        }

        [HttpPost("{id:int}/encerrar")]
        public async Task<IActionResult> EncerrarChamado(
            int id,
            [FromBody] string solucao)
        {
            await _service.EncerrarChamadoAsync(id, solucao);

            return NoContent();
        }

        private static ChamadoDto ConverterParaDto(Chamado chamado)
        {
            return new ChamadoDto
            {
                Id = chamado.Id,
                Titulo = chamado.Titulo,
                Descricao = chamado.Descricao,
                Prioridade = chamado.Prioridade,
                Status = chamado.Status,
                SolicitanteNome = chamado.SolicitanteNome,
                DataAbertura = chamado.DataAbertura,
                DataFechamento = chamado.DataFechamento,
                Solucao = chamado.Solucao,
                CategoriaId = chamado.CategoriaId,

                Categoria = chamado.Categoria == null
                    ? null
                    : new CategoriaDto
                    {
                        Id = chamado.Categoria.Id,
                        Nome = chamado.Categoria.Nome
                    },

                Interacoes = chamado.Interacoes
                    .Select(interacao => new InteracaoDto
                    {
                        Id = interacao.Id,
                        ChamadoId = interacao.ChamadoId,
                        Autor = interacao.Autor,
                        Mensagem = interacao.Mensagem,
                        DataRegistro = interacao.DataRegistro
                    })
                    .ToList()
            };
        }
    }
}