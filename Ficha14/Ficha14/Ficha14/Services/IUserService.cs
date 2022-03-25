using Ficha14.Models;

namespace Ficha14.Services
{
    public interface IUserService
    {
        public abstract User? Get(string username, string password);
        public abstract User Create(User newUser);
        public abstract User FindByName(string username);
    }
}
