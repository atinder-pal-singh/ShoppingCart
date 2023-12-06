using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ShoppingCart.Entities;

namespace ShoppingCart.DbContexts
{
    public class ShoppingDbContext: DbContext
    {
        public DbSet<Product> products { get; set; } = null!;
        public DbSet<Image> images { get; set; } = null!;

        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options): base(options) 
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
