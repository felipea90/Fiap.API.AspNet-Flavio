using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace fmkp.core.Data.Pagination.Asbtractions
{
    public interface IPagedDataFactory
    {
        Task<IPagedData<T>> CreateAsync<T>(
            IQueryable<T> query, int page, int perPage,
            CancellationToken cancellationToken);
    }
}
