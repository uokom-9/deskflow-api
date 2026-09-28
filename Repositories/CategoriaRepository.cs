using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories
{
    public class CategoriaRepository
    {
        private readonly AppDbContext _context;

        // O Program.cs vai injetar o AppDbContext aqui automaticamente através do construtor
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        // POST
        public async Task AdicionarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        // GET todas
        public async Task<List<Categoria>> BuscarTodasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET por Id
        public async Task<Categoria?> BuscarPorIdAsync(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        // PUT
        public async Task AtualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task DeletarAsync(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        // Verifica se já existe algum chamado que usa o ID dessa categoria
        public async Task<bool> PossuiChamadosAssociadosAsync(int categoriaId)
        {
            return await _context.Chamados.AnyAsync(c => c.CategoriaId == categoriaId);
        }
    }
}