using PRN211_ShoesStore.Models.Entity;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface IUserService
    {
        public void AddUser(User user);
        public User GetUserByUserId(int id);
        public User GetUserByUsername(string username);
        public User GetUserByGoogleId(string googleId);
        public void UpdateUser(User user);
        public void RemoveUser(User user);
    }
}
