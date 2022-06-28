using ZuvviiAPI.Models;
using ZuvviiAPI.Data;

namespace ZuvviiAPI.Repository.Impl
{
    public class IUserRepositoryImpl : IUserRepository
    {
        private readonly DataContext _dataContext;
        public IUserRepositoryImpl(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User GetByEmail(string Email)
        {
            var user = new User();
            user = _dataContext.Users.FirstOrDefault(x => x.Email == Email);
            return user;
        }

        public User GetById(string Id)
        {
            var user = new User();
            Guid id = Guid.Parse(Id);
            user = _dataContext.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public User GetByUserName(string UserName)
        {
            var user = new User();
            user = _dataContext.Users.FirstOrDefault(x => x.UserName == UserName);
            return user;
        }

        public User Login(string email, string password)
        {
            return _dataContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public bool Save(User user)
        {
            var userExists = _dataContext.Users.Any(x => x.Email == user.Email);
            if (userExists)
            {
                return false;
            }

            var userNameExists = _dataContext.Users.Any(x => x.UserName == user.UserName);
            if (userNameExists)
            {
                return false;
            }

            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
            return true;
        }
    }
}
