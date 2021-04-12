using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void LockUser(string userId);

        void UnlockUser(string userId);
    }
}
