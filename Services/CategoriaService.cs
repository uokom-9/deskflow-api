using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories;

namespace DeskFlow.API.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _repository;
        public CategoriaService(CategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            await _repository.AdicionarAsync(categoria);
        }

        public async Task<List<Categoria>> ListarTodasAsync()
        {
            return await _repository.BuscarTodasAsync();
        }

        public async Task<Categoria?> BuscarPorIdAsync(int id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task AtualizarAsync(int id, Categoria categoriaAtualizada)
        {
            var categoriaExistente = await _repository.BuscarPorIdAsync(id);
            if (categoriaExistente == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            if (string.IsNullOrWhiteSpace(categoriaAtualizada.Nome))
            {
                throw new ArgumentException("O nome da categoria não pode ser vazio.");
            }

            categoriaExistente.Nome = categoriaAtualizada.Nome;
            await _repository.AtualizarAsync(categoriaExistente);
        }

        public async Task DeletarAsync(int id)
        {
            var categoria = await _repository.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            var possuiChamados = await _repository.PossuiChamadosAssociadosAsync(id);
            if (possuiChamados)
            {
                throw new InvalidOperationException("Não é possível deletar uma categoria que possui chamados associados.");
            }

            await _repository.DeletarAsync(categoria);
        }
    }
}