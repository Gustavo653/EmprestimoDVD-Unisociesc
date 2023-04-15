using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.DTO
{
    public class PessoaDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
    }
}
