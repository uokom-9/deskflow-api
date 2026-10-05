namespace DeskFlow.API.Models.Entities
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public string Prioridade { get; set; } = string.Empty; // Baixa, Media ou Alta
        public string Status { get; set; } = string.Empty; // Aberto, EmAndamento ou Fechado
        
        public string SolicitanteNome { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; } // O ? permite que comece vazio
        public string? Solucao { get; set; }

        // Relacionamento N:1
        public int CategoriaId { get; set; } // Chave estrangeira
        public Categoria? Categoria { get; set; } = null!; // null! é para não deixar nulo, mas vazio

        // Relacionamento 1:N
        public ICollection<Interacao> Interacoes { get; set; } = new List<Interacao>();
    }
}