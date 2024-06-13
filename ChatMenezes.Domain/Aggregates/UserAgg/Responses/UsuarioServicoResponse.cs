namespace ChatMenezes.Domain.Aggregates.UserAgg.Responses
{
    public class UsuarioServicoResponse
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public Guid? Identificador { get; set; }
        public string? Whatsapp { get; set; }
        public string? Facebook { get; set; }
        public string? Linkedin { get; set; }
        public string? Instagram { get; set; }
        public string? Tiktok { get; set; }
        public string? Youtube { get; set; }
        public string? UrlImagem { get; set; }
        public string? ImagemBase64 { get; set; }
    }
}
