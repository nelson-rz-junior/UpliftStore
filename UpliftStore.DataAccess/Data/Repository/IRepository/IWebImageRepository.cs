using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository.IRepository
{
    public interface IWebImageRepository : IRepository<WebImage>
    {
        void Update(WebImage webImage);
    }
}
