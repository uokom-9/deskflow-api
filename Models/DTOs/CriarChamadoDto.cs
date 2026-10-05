namespace DeskFlow.API.Models.DTOs
{
    public class CriarChamadoDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Prioridade { get; set; } = string.Empty;
        public string SolicitanteNome { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
    }
}