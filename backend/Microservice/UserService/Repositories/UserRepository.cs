using TextFileDataStore;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ITextFileStorage<User> _storage;

        public UserRepository(ITextFileStorage<User> storage)
        {
            _storage = storage;
        }

        public IEnumerable<User> GetAll() => _storage.GetAll();

        public User GetById(int id) => _storage.GetById(id);

        public User Add(User user)
        {
            _storage.Add(user);
            return user;
        }

        public User Update(User user)
        {
            _storage.Update(user);
            return user;
        }

        public void Remove(int id)
        {
            _storage.Remove(id);
        }
    }
}
