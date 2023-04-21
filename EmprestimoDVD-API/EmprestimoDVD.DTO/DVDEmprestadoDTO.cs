using EmprestimoDVD.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.DTO
{
    public class DVDEmprestadoDTO
    {
        public int Id { get; set; }
        public Genero Genero { get; set; }
        public Pessoa ArtistaPrincipal { get; set; }
        public Pessoa Diretor { get; set; }
        public string Sinopse { get; set; }
        public string Titulo { get; set; }
        public int IdadeMinima { get; set; }
        public string Amigo { get; set; }
    }
}
