namespace DATINGAPP.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _context;

        #region Ctor
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        #endregion

        #region Utility
        public void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;<computedHash.Length;i++)
                {
                    if(computedHash[i]) != passwordSalt[i] return false;
                }
            }
        }
        #endregion

        #region Methods
        public async Task<User> Register(User user,string password)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<USER> Login(string username,string password)
        {
            var user await _context.User.FirstOrDefaultAsync(x=>x.Username == username);
            if(user == null)
                return null;

            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
                return null;

            return user;
        }
        public async Task<bool> UserExists(string username)
        {
            if(await _context.user.AnyAsync(x=>x.Username == username))
                return true;

            return false;
        }
        #endregion
    }
}