using NHibernate;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetTest.Data
{
    public class NHibernateMapperSession : IMapperSession
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHibernateMapperSession(ISession session)
        {
            _session = session;
        }

        public IQueryable<AppUser> AppUsers => _session.Query<AppUser>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Delete(AppUser entity)
        {
            await _session.DeleteAsync(entity);
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public async Task Save(AppUser entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }
    }
}
