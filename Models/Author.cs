﻿namespace Istoc_Oana_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }

    }

}
