using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;

namespace PRN211_ShoesStore.Repository.vH
{
    public class UserRepository2 : vH.Repository<User>, vH.Interface.IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this._appDbContext = appDbContext;
        }
    }
}
