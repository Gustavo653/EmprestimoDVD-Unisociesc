using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimoDVD.Domain
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
