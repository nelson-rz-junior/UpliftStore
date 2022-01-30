using System.Threading.Tasks;

namespace UpliftStore.DataAccess.Data.Initializer
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}
