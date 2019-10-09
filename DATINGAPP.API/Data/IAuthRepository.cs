namespace DATINGAPP.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user,string password);
         Task<USER> Login(string username,string password);
         Task<bool> UserExists(string username);
    }
}