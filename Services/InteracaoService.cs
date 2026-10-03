using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories;

namespace DeskFlow.API.Services
{
    public class InteracaoService
    {
        private readonly InteracaoRepository _repository;
        private readonly ChamadoRepository _chamadoRepository;

        public InteracaoService(InteracaoRepository repository, ChamadoRepository chamadoRepository)
        {
            _repository = repository;
            _chamadoRepository = chamadoRepository;
        }

        public async Task AdicionarInteracaoAsync(int chamadoId, Interacao interacao)
        {
            var chamado = await _chamadoRepository.BuscarPorIdCompletoAsync(chamadoId);
            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado.");
            }

            if (chamado.Status == "Fechado")
            {
                throw new InvalidOperationException("Não é possível adicionar uma interação a um chamado que já está fechado.");
            }

            if (string.IsNullOrWhiteSpace(interacao.Autor))
            {
                throw new ArgumentException("O autor é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(interacao.Mensagem))
            {
                throw new ArgumentException("A mensagem é obrigatória.");
            }

            interacao.ChamadoId = chamadoId;
            interacao.DataRegistro = DateTime.Now;

            await _repository.AdicionarAsync(interacao);
        }
    }
}
