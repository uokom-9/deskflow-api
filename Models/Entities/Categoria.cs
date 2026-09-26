namespace DeskFlow.API.Models.Entities
{
    public class Categoria
    {
        public int Id { get; set; } // Chave primária
        public string Nome { get; set; } = string.Empty; // O string.Empty é para não deixar nulo, mas vazio

        // Relacionamento 1:N
        public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>(); // List é para não deixar nulo, mas vazio
    }
}