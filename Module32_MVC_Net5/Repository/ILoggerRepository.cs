using Module32_MVC_Net5.Models.Db;
using System.Threading.Tasks;

namespace Module32_MVC_Net5.Repository
{
    public interface ILoggerRepository
    {
        Task AddRequest(Request request);
        Task<Request[]> GetRequests();
    }
}
