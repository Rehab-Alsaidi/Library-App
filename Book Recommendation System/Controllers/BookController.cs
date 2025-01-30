using Microsoft.AspNetCore.Mvc;
using Book_Recommendation_System.Data;
using Book_Recommendation_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Book_Recommendation_System;
using System.Diagnostics;

public class BookController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public BookController(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    [HttpGet]
    public async Task<IActionResult> Index(int? page, string searchString)
    {
        int pageSize = 10;

        var books = _dbContext.Book.AsNoTracking();

        if (!string.IsNullOrEmpty(searchString))
        {
            books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
        }

        var paginatedBooks = await PaginatedList<Book>.CreateAsync(books, page ?? 1, pageSize);

        return View(paginatedBooks);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string searchString, int? page)
    {
        var books = _dbContext.Book.AsNoTracking();

        if (!string.IsNullOrEmpty(searchString))
        {
            books = books.Where(book => book.Title.Contains(searchString) || book.Author.Contains(searchString));
        }

        int pageSize = 10;
        var paginatedBooks = await PaginatedList<Book>.CreateAsync(books, page ?? 1, pageSize);

        ViewData["searchString"] = searchString;

        return View("Index", paginatedBooks);
    }

    [HttpGet]
    public IActionResult Error()
    {
        var errorViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
        return View(errorViewModel);
    }

    // Update the Details method in BookController
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var book = await _dbContext.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                // Check if the user exists
                if (user != null)
                {
                    // Explicitly load the user's favorite books
                    await _dbContext.Entry(user).Collection(u => u.FavoriteBooks).LoadAsync();

                    var recommendedBooks = GenerateRecommendedBooks(user);

                    ViewData["RecommendedBooks"] = recommendedBooks;
                }
            }

            return View(book);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Details: {ex.Message}");
            return RedirectToAction("Error");
        }
    }




    private List<Book> GenerateRecommendedBooks(ApplicationUser user)
    {
        try
        {
            if (user == null || _dbContext == null || _dbContext.Book == null)
            {
                return new List<Book>();
            }

            // Explicitly load the user's favorite books
            _dbContext.Entry(user).Collection(u => u.FavoriteBooks).Load();

            var recommendedBooks = _dbContext.Book
                .Where(book => !user.FavoriteBooks.Contains(book))
                .OrderBy(r => Guid.NewGuid())
                .Take(5)
                .ToList();

            return recommendedBooks;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in GenerateRecommendedBooks: {ex.Message}");
            return new List<Book>();
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToFavorites(int id)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            var book = await _dbContext.Book.FindAsync(id);

            if (book != null)
            {
                // Check if the book is not already in the favorites
                if (!user.FavoriteBooks.Any(b => b.ISBN == book.ISBN))
                {
                    user.FavoriteBooks.Add(book);
                    await _userManager.UpdateAsync(user);
                }

                // Refresh user's favorite books
                await _dbContext.Entry(user).Collection(u => u.FavoriteBooks).LoadAsync();
                var favoriteBooks = user.FavoriteBooks;

                ViewData["FavoriteBooks"] = favoriteBooks;

                return RedirectToAction("UserDashboard");
            }
            else
            {
                Console.WriteLine("Book is null!");
            }
        }
        else
        {
            Console.WriteLine("User is null!");
        }

        return RedirectToAction("Error");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RemoveFromFavorites(int id)
    {
        try
        {
            var book = await _dbContext.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            // Explicitly load the user's favorite books
            await _dbContext.Entry(user).Collection(u => u.FavoriteBooks).LoadAsync();

            // Remove the book from the user's favorites
            var removed = user.FavoriteBooks.Remove(book);

            if (removed)
            {
                // Save changes to the database
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Details", new { id });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in RemoveFromFavorites: {ex.Message}");
            return RedirectToAction("Error");
        }
    }


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> UserDashboard()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Error");
            }

            // Explicitly load the user's favorite books
            await _dbContext.Entry(user).Collection(u => u.FavoriteBooks).LoadAsync();

            var favoriteBooks = user.FavoriteBooks;
            var recommendedBooks = GenerateRecommendedBooks(user);

            ViewData["FavoriteBooks"] = favoriteBooks;
            ViewData["RecommendedBooks"] = recommendedBooks;

            return View();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in UserDashboard: {ex}");
            return RedirectToAction("Error");
        }
    }
}
