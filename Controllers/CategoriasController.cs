using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.DTOs;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DeskFlow.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _service;
        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarCategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nome = dto.Nome,
            };

            await _service.CriarAsync(categoria);
            return Created($"/api/categorias/{categoria.Id}", ConverterParaDto(categoria));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _service.ListarTodasAsync();
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _service.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound(new { mensagem = "Categoria não encontrada." });
            }
            return Ok(categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CriarCategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nome = dto.Nome
            };

            await _service.AtualizarAsync(id, categoria);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletarAsync(id);
            return NoContent();
        }

        private static CategoriaDto ConverterParaDto(Categoria categoria)
        {
            return new CategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }
    }
}