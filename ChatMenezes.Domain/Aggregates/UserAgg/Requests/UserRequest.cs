using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ChatMenezes.Domain.Aggregates.UserAgg.Requests
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? Name { get; set; }

        [StringLength(18)]
        [Required(ErrorMessage = "CNPJ é obrigatório!")]
        public string? CNPJ { get; set; }

        [Display(Name = "Data de Aniversário")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data de nascimento é obrigatória!")]
        public DateTime? BirthDate { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Número da da CNH é obrigatório!")]
        public string? CNHNumber { get; set; }

        [Required(ErrorMessage = "Tipo da CNH é obrigatório!")]
        public int? CNHTypeId { get; set; }

        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        public IFormFile? Archive { get; set; }
    }
}
