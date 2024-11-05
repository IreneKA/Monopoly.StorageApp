using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StorageApp
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStorageServices(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionAction = null)
        {
            services.AddDbContext<StorageDbContext>(optionAction);
            services.AddTransient<IPalletRepository, PalletRepository>();
            services.AddTransient<IPalletService, PalletService>();
        }
    }
}
