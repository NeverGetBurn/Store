using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Store.DB
{
    public static class DbContextBuilder
    {
        public static void InitContext(IServiceCollection services, string connection){

            services.AddDbContext<Context>(options =>
                options.UseSqlite(connection)
            );
        }
    }
}