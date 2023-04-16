using System.ComponentModel.DataAnnotations;

namespace EmprestimoDVD.DTO
{
    public class GeneroDTO
    {
        [Required]
        public string Descricao { get; set; } = null!;
    }
}
