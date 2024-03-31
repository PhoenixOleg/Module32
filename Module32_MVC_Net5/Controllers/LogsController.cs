using Microsoft.AspNetCore.Mvc;
using Module32_MVC_Net5.Repository;
using System.Threading.Tasks;

namespace Module32_MVC_Net5.Controllers
{
    public class LogsController : Controller
    {
        // ссылка на репозиторий
        private readonly ILoggerRepository _repo;

        public LogsController(ILoggerRepository repo)
        {
            _repo = repo; 
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _repo.GetRequests();
            return View(logs);
        }
    }
}
