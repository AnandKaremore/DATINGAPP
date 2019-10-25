using System.Collections.Generic;
using System.Linq;
using DATINGAPP.API.Models;
using Newtonsoft.Json;
namespace DATINGAPP.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            var userFile = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var Users = JsonConvert.DeserializeObject<List<User>>(userFile);
            foreach (var user in Users)
            {
                byte[] passwordHash,passwordSalt;
                CreatePasswordHash("password",out passwordHash,out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();
                context.User.Add(user);
            }
            context.SaveChanges();
        }

        private static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}