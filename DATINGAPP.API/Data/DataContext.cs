using DATINGAPP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DATINGAPP.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<Value> Values{get;set;}
        public DbSet<User> User{get;set;}
        public DbSet<Photo> Photo{get;set;}
    }
}