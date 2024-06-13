namespace ChatMenezes.Domain.Aggregates.UserAgg.Dtos
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CNHNumber { get; set; }
        public int? CNHTypeId { get; set; }
        public string? Email { get; set; }
        public bool PossuiImagem { get; set; }
    }
}
