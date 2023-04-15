using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.DTO
{
    public class AmigoDTO : PessoaDTO
    {
        [Required]
        public long NumTelefone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Endereco { get; set; } = null!;
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
