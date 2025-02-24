using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdataBookStore.entity
{
    public class Publisher
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PubId { get; set; }

        public string? PubName { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
