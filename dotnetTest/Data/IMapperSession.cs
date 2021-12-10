using NHibernate.Persister.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetTest.Data
{
    public interface IMapperSession
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(AppUser entity);
        Task Delete(AppUser entity);

        IQueryable<AppUser> AppUsers { get; }
    }
}
