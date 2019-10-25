using System.Threading.Tasks;
using DATINGAPP.API.Models;
using System.Collections.Generic;
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
            var users = _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.Id==id);
            return users;
        }
        public Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p=> p.Photos).ToListAsync();
            return users;
        }
        public Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}