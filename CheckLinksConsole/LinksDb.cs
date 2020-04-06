using Serilog;
using Serilog.Core;
using Microsoft.EntityFrameworkCore;

namespace CheckLinksConsole
{
    public class LinksDb : DbContext
    {
		public DbSet<LinkCheckResult> Links { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// MSSQL:
			var connection = @"Server=192.168.1.87;Database=Links;User Id=sa; Password=whatever12!";
			optionsBuilder.UseSqlServer(connection);

			// MySQL (Pomelo)
			//var connection = "server=192.168.1.87;userid=root;pwd=password;database=Links;sslmode=none;";
			//optionsBuilder.UseMySql(connection);

			// PostgreSQL (Npgsql)
			//var connection = "Host=192.168.1.87;Database=Links;Username=postgres;Password=password";
			//optionsBuilder.UseNpgsql(connection);

			// SQLite:
			// var databaseLocation = Path.Combine(Directory.GetCurrentDirectory(), "links.db");
			// optionsBuilder.UseSqlite($"Filename={databaseLocation}");
		}
	}
}
