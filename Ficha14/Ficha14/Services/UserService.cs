using Ficha14.Models;

namespace Ficha14.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext context;

        public UserService(UserContext context)
        {
            this.context = context;

        }
        public User Create(User newUser)
        {
                context.Users.Add(newUser);
                context.SaveChanges();
                return newUser;
        }

        public User FindByName(string username)
        {
           var user = context.Users
                .SingleOrDefault(u => u.UserName == username);
            return user;
        }

        public User? Get(string username, string password)
        {
            var user = context.Users
                .SingleOrDefault(u => u.UserName == username && u.Password == password);
            return user;
        }


    }
}
