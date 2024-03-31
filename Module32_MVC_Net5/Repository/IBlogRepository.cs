using Module32_MVC_Net5.Models.Db;
using System.Threading.Tasks;

namespace Module32_MVC_Net5.Repository
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}
