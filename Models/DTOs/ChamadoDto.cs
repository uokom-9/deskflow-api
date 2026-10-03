namespace DeskFlow.API.Models.DTOs
{
    public class ChamadoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Prioridade { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string SolicitanteNome { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string? Solucao { get; set; }
        public int CategoriaId { get; set; }

        public CategoriaDto? Categoria { get; set; }
        public List<InteracaoDto> Interacoes { get; set; } = new();
    }
}