using System.ComponentModel.DataAnnotations.Schema;

namespace Istoc_Oana_Lab2.Models
{
    [Table("Publisher")]
    public class Publisher
    {
        public int ID { get; set; }
        public string PublisherName { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
