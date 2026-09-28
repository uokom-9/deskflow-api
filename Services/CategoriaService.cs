using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories;

namespace DeskFlow.API.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _repository;

        // O CategoriaRepository é injetado automaticamente
        public CategoriaService(CategoriaRepository repository)
        {
            _repository = repository;
        }

        // Regra para cadastrar categoria
        public async Task CadastrarAsync(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            await _repository.AdicionarAsync(categoria);
        }

        // Regra para listar todas as categorias
        public async Task<List<Categoria>> ListarTodasAsync()
        {
            return await _repository.BuscarTodasAsync();
        }

        // Regra para buscar categoria por Id
        public async Task<Categoria?> BuscarPorIdAsync(int id) // ? serve para permitir que retorne nulo caso não encontre a categoria
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        // Regra para atualizar categoria
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

        // Regra de deletar categoria
        public async Task DeletarAsync(int id)
        {
            var categoria = await _repository.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            // Verifica se há chamados vinculados
            var possuiChamados = await _repository.PossuiChamadosAssociadosAsync(id);
            if (possuiChamados)
            {
                throw new InvalidOperationException("Não é possível deletar uma categoria que possui chamados associados.");
            }

            await _repository.DeletarAsync(categoria);
        }
    }
}