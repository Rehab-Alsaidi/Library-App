using Microsoft.AspNetCore.Identity;

namespace Book_Recommendation_System.Models
{
    public class ApplicationUser : IdentityUser
    {

        public List<Book> FavoriteBooks { get; set; } = new List<Book>();




    }
}
