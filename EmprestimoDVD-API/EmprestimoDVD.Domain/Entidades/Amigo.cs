using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.Domain.Entidades
{
    public class Amigo : Pessoa
    {
        public long NumTelefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public FaixaEtaria FaixaEtaria { get; set; }
    }
}
