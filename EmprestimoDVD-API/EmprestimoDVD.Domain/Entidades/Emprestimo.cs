using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.Domain.Entidades
{
    public class Emprestimo : BaseEntity
    {
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolver { get; set; }
        public Amigo Amigo { get; set; }
        public DVD DVD { get; set; }
    }
}
