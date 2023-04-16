using System;

namespace EmprestimoDVD.Domain.Entidades
{
    public class Emprestimo : BaseEntity
    {
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolver { get; set; }
        public Amigo Amigo { get; set; }
        public DVD DVD { get; set; }
    }
}
