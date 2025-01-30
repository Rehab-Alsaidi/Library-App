using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Book_Recommendation_System.Models
{
    [Table("Book")]
    public class Book
    {
        public int UserID { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }

        [Key]
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }
        public string Publisher { get; set; }
        [Range(0, 10)]
        public int Rating { get; set; }
        public ICollection<ApplicationUser> UsersWhoMarkedAsFavorite { get; set; } = new List<ApplicationUser>();

        // Define a non-mapped property for recommended book titles
        //[NotMapped]
        //public List<string> RecommendedBookTitles { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Book other)
            {
                return this.ISBN == other.ISBN;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }




    }
}
