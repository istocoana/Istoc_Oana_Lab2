namespace Istoc_Oana_Lab2.Models
{
    public class AuthorBook
    {
        public int AuthorBookId { get; set; }

        public int AuthorID { get; set; }
        public int BookID { get; set; }

        public Author Author { get; set; }
        public Book Book { get; set; }

    }
}
