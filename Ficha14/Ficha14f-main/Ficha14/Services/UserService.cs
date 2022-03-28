using Ficha14.Models;
using Microsoft.EntityFrameworkCore;

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

        public User? Get(string username, string password) /*, string avatar*/
        {
            var user = context.Users
                .SingleOrDefault(u => u.UserName == username && u.Password == password); /* && u.Avatar == avatar*/

            return user;
        }

        public void ImageUpdate(string avatar, User nUser)
        {
            var idToUpdate = context.Users.Find(1); //context.Users.Find(nUser.ID); ID nao dá

            if (idToUpdate is null)
            {
                throw new NullReferenceException("User does not exist");
            }

            else
            {
                idToUpdate.Avatar = avatar;
                context.SaveChanges();
            }


        }
    }
}
