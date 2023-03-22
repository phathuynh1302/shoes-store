using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;

namespace PRN211_ShoesStore.Service.vH
{
    public class UserService2 : vH.Interface.IUserService
    {
        private readonly IUserRepository UserRepository2;
        public UserService2(IUserRepository userRepository)
        {
            this.UserRepository2 = userRepository;
        }
        public void AddUser(User user) => this.UserRepository2.Add(user);
        public User GetUserByUserId(int id) => this.UserRepository2.GetFirstOrDefault(item => item.id==id);
        public User GetUserByUsername(string username) => this.UserRepository2.GetFirstOrDefault(item => item.username.Equals(username));
        public void UpdateUser(User user) => this.UserRepository2.Update(user);
        public void RemoveUser(User user) => this.UserRepository2.Remove(user);


        public User GetUserByGoogleId(string googleId)
        {
            throw new System.NotImplementedException();
        }
    }
}
