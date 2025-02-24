using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OdataBookStore.entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string? EmailAddress { get; set; }

        public string? Password { get; set; }

        public string? Source { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public int? RoleId { get; set; }

        public int? PubId { get; set; }

        public DateTime? HireDate { get; set; }
        [JsonIgnore]
        public virtual Publisher? Publisher { get; set; }
        [JsonIgnore]
        public virtual Role? Role { get; set; }
    }
}
