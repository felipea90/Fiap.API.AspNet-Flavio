using fmkp.core.Data.Pagination.Asbtractions;
using fmkp.core.Data.Pagination.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace fmkp.core.Data.Pagination
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPagination(
            this IServiceCollection services)
        {
            return services
                .AddSingleton<IPagedDataFactory, PagedDataFactory>();
        }
    }
}
