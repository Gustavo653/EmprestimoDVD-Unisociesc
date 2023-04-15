using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.DTO
{
    public class GeneroDTO
    {
        [Required]
        public string Descricao { get; set; } = null!;
    }
}
