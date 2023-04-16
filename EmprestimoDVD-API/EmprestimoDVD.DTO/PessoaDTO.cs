using System.ComponentModel.DataAnnotations;

namespace EmprestimoDVD.DTO
{
    public class PessoaDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
    }
}
