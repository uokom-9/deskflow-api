using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories;

namespace DeskFlow.API.Services
{
    public class ChamadoService
    {
        private readonly ChamadoRepository _repository;
        private readonly CategoriaRepository _categoriaRepository;

        public ChamadoService(ChamadoRepository repository, CategoriaRepository categoriaRepository)
        {
            _repository = repository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task CriarAsync(Chamado chamado)
        {
            var categoriaExiste = await _categoriaRepository.BuscarPorIdAsync(chamado.CategoriaId);
            if (categoriaExiste == null)
            {
                throw new KeyNotFoundException("A categoria informada para o chamado não existe.");
            }

            chamado.Status = "Aberto";
            chamado.DataAbertura = DateTime.Now;
            chamado.DataFechamento = null;
            chamado.Solucao = null;

            await _repository.AdicionarAsync(chamado);
        }

        public async Task<Chamado> BuscarPorIdAsync(int id)
        {
            var chamado = await _repository.BuscarPorIdCompletoAsync(id);
            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado.");
            }
            return chamado;
        }

        public async Task<List<Chamado>> ListarComFiltrosAsync(string? status, string? prioridade, int? categoriaId)
        {
            return await _repository.BuscarComFiltrosAsync(status, prioridade, categoriaId);
        }

        public async Task IniciarAtendimentoAsync(int id)
        {
            var chamado = await _repository.BuscarPorIdCompletoAsync(id);
            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado.");
            }

            if (chamado.Status != "Aberto")
            {
                throw new InvalidOperationException("Só é possível iniciar chamados que estão com o status 'Aberto'.");
            }

            chamado.Status = "EmAndamento";
            await _repository.AtualizarAsync(chamado);
        }

        public async Task EncerrarChamadoAsync(int id, string solucao)
        {
            var chamado = await _repository.BuscarPorIdCompletoAsync(id);
            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado.");
            }

            if (string.IsNullOrWhiteSpace(solucao))
            {
                throw new ArgumentException("É obrigatório informar o texto de solução para encerrar o chamado.");
            }

            if (chamado.Status == "Fechado")
            {
                throw new InvalidOperationException("Este chamado já se encontra encerrado.");
            }

            chamado.Status = "Fechado";
            chamado.Solucao = solucao;
            chamado.DataFechamento = DateTime.Now;

            await _repository.AtualizarAsync(chamado);
        }
    }
}
