using Ficha14.Models;

namespace Ficha14.Services
{
    public interface IUserService
    {
        public abstract User? Get(string username, string password/*, string avatar  ImageUploaded avatar*/);
        public abstract User Create(User newUser);
        public abstract User FindByName(string username);
        public abstract void ImageUpdate(string avatar, User nUser);
    }
}
