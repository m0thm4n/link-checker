using Serilog;
using Serilog.Core;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Connections.Abstractions;

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

			// MySQL (Pomelo):
			// var connection = "server=localhost;userid=root;pwd=password;database=Links;sslmode=none;";
			// optionsBuilder.UseMySql(connection);

			// PostgresSQL (Npgsql):
			// var connection = "Host=localhost;Database=Links;Username=postgres;Password=password";
			// optionsBuilder.UseNpgsql(connection);

			// SQLite:
			// var databaseLocation = Path.Combine(Directory.GetCurrentDirectory(), "links.db");
			// optionsBuilder.UseSqlite($"Filename={databaseLocation}");
		}
    }
}
