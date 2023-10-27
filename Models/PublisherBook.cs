namespace Istoc_Oana_Lab2.Models
{
    public class PublisherBook
    {
        public int PublisherBookId { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public Publisher Publisher { get; set; }
        public Book Book { get; set; }
    }
}
