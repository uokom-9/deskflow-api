namespace DeskFlow.API.Models.Entities
{
    public class Interacao
    {
        public int Id { get; set; }
        public int ChamadoId { get; set; } // Chave estrangeira
        public string Autor { get; set; } = string.Empty; // Quem escreveu (técnico ou usuário)
        public string Mensagem { get; set; } = string.Empty;
        public DateTime DataRegistro { get; set; }

        // Relacionamento N:1
        public Chamado Chamado { get; set; } = null!;
    }
}