using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.Domain.Entidades
{
    public class FaixaEtaria : BaseEntity
    {
        public int De { get; set; }
        public int Ate { get; set; }
    }
}
