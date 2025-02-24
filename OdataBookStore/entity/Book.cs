using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OdataBookStore.entity
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        public string? Title { get; set; }

        public int? Type { get; set; }

        public int? PubId { get; set; }

        public double? Price { get; set; }

        public string? Advance { get; set; }

        public double? Royalty { get; set; }

        public double? YtlSales { get; set; }

        public string? Note { get; set; }

        public DateTime? PublishedDate { get; set; }

        [JsonIgnore]
        public virtual Publisher? Publisher { get; set; }
  
    }
}
