using CrudApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CrudApi.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=MORALES91\\SQLEXPRESS;Database=CrudApiDb;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }

