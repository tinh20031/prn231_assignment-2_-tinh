using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdataBookStore.entity
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        public string? RoleDesc { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
