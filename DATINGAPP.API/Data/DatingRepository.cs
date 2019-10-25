using System.Threading.Tasks;
using System
namespace DATINGAPP.API.Data
{
    public class DatingRepository:IDatingRepository
    {
        private DataContext _context;
        public DatingRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T: Class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T:Class
        {
            _context.Delete(entity);
        }
        public Task<User> GetUser(int id)
        {
            return _context.Users.Include(p=>p.Photos).FirstOrDefault(x=>x.Id==id);
        }

    }
}