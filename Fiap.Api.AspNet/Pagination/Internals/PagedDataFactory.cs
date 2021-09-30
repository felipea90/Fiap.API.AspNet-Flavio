using fmkp.core.Data.Pagination.Asbtractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace fmkp.core.Data.Pagination.Internals
{
    internal class PagedDataFactory
        : IPagedDataFactory
    {
        public async Task<IPagedData<T>> CreateAsync<T>(
            IQueryable<T> query, int page, int perPage,
            CancellationToken cancellationToken)
        {
            int? count = null;
            int? totalPages = null;
            int? currentPage = null;
            int skip = (page - 1) * perPage;

            if (page != 0)
            {
                count = await query.CountAsync(cancellationToken);
                var resultDivRem = Math.DivRem(count.Value, perPage, out int resultRemainder);

                if (perPage >= count)
                {
                    totalPages = 1;
                }
                else
                {
                    totalPages = resultDivRem + (resultRemainder > 0 ? 1 : 0);
                }

                currentPage = page;

                query = query
                    .Skip(skip)
                    .Take(perPage);
            }

            var data = await query
                .ToListAsync(cancellationToken);

            if (page.Equals(0))
            {
                totalPages = 1;
                currentPage = 1;
                count = data.Count;
            }

            return new PagedData<T>(
                data,
                currentPage.Value,
                totalPages.Value,
                count.Value);
        }
    }
}
