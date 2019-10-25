using System.Collections.Generic;
using System.Threading.Tasks;
using DATINGAPP.API.Models;
namespace DATINGAPP.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: Class;

         void Delete<T>(T entity) where T: Class;

         Task<bool> SaveAll();

         Task<User> GetUser(int id);

         Task<IEnumerable<User>> GetUsers();
    }
}