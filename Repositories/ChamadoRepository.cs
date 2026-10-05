using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories
{
    public class ChamadoRepository
    {
        private readonly AppDbContext _context;
        public ChamadoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        
        // Busca chamado por ID, incluindo Categoria e Interações
        public async Task<Chamado?> BuscarPorIdCompletoAsync(int id)
        {
            return await _context.Chamados
                .Include(c => c.Categoria)
                .Include(c => c.Interacoes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Busca chamados com filtros
        public async Task<List<Chamado>> BuscarComFiltrosAsync(string? status, string? prioridade, int? categoriaId)
        {
            var query = _context.Chamados
                .AsNoTracking()
                .Include(c => c.Categoria)
                .Include(c => c.Interacoes)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status == status);
            }

            if (!string.IsNullOrWhiteSpace(prioridade))
            {
                query = query.Where(c => c.Prioridade == prioridade);
            }

            if (categoriaId.HasValue)
            {
                query = query.Where(c => c.CategoriaId == categoriaId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task AtualizarAsync(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
        }
    }
}
