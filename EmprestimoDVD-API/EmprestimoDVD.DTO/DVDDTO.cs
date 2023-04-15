using EmprestimoDVD.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.DTO
{
    public class DVDDTO
    {
        [Required]
        public string Titulo { get; set; } = null!;
        [Required]
        public string Sinopse { get; set; } = null!;
        [Required]
        [Range(0, 18)]
        public int IdadeMinima { get; set; }
        [Required]
        public int IdArtistaPrincipal { get; set; }
        [Required]
        public int IdDiretor { get; set; }
        [Required]
        public int IdGenero { get; set; }
    }
}
