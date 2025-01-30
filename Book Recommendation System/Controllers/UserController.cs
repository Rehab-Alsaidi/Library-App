using Book_Recommendation_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Recommendation_System.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IActionResult Index()
        {
            return View();
        }

       

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user.FavoriteBooks);
        }
    }
}
