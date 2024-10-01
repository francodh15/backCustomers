using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using BackMedicos.Models;

namespace BackMedicos.DataBase
{
    public class ApplicationDbContext : DbContext
    {
      public DbSet<Clientes> Clientes { get; set; }
      public DbSet<Medicos> Medicos { get; set; }
      public DbSet<Usuarios> Usuarios { get; set; }
      public DbSet<Login> Login { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
			try
			{
				var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                        dbCreator.Create();
                    if (!dbCreator.HasTables())
                        dbCreator.CreateTables();
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
        }
    }
}
