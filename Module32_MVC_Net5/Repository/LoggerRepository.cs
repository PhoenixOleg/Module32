using Microsoft.EntityFrameworkCore;
using Module32_MVC_Net5.Models.Contexts;
using Module32_MVC_Net5.Models.Db;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Module32_MVC_Net5.Repository
{
    public class LoggerRepository : ILoggerRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

        // Метод-конструктор для инициализации
        public LoggerRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddRequest(Request request)
        {
            request.Id = Guid.NewGuid();

            // Добавление записи о запросе в лог
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequests()
        {
            // Получение всех записей лога в порядке возрастания даты
            return await _context.Requests.OrderBy(r => r.Date).ToArrayAsync();
        }
    }
}
