using ZuvviiAPI.Models;

namespace ZuvviiAPI.Repository
{
    public interface IUserRepository
    {
        public bool Save(User user);
        public User GetById(string Id);
        public User GetByEmail(string Email);
        public User GetByUserName(string UserName);
        public User Login(string email, string password);
    }
}
