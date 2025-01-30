// BookRecommendedService.cs
using System.Collections.Generic;
using System.Linq;
using Book_Recommendation_System.Data;
using Book_Recommendation_System.Models;
using Book_Recommendation_System.Services;

/*public class BookRecommendedService : IBookRecommendedService
{
    private readonly AppDbContext _dbContext;

    public BookRecommendedService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<string> GetRecommendedBooks(int userId)
    {
        // Implement your recommendation logic here.
        // Fetch random 5 recommended book titles from the database based on userId or any other criteria.
        var recommendedBookTitles = _dbContext.Book
            .Where(rb => rb.UserID == userId)
            .OrderBy(r => Guid.NewGuid()) // Shuffle the records
            .Select(rb => rb.Title)
            .Take(5)
            .ToList();

        return recommendedBookTitles;
    }
}
*/