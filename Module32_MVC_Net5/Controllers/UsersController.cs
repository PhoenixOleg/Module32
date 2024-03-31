using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module32_MVC_Net5.Models;
using Module32_MVC_Net5.Models.Db;
using Module32_MVC_Net5.Repository;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Module32_MVC_Net5.Controllers
{
    public class UsersController : Controller
    {
        // ссылка на репозиторий
        private readonly IBlogRepository _repo;

        public UsersController(IBlogRepository repo)
        {  
            _repo = repo; 
        }

        public async Task<IActionResult> Authors()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.AddUser(newUser);
            return View(newUser);
        }
    }
}
