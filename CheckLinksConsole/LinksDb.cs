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
		var connection = @"Server=192.168.1.87;Database=Links;User Id=sa; Password=whatever12!";
		optionsBuilder.UseSqlServer(connection);
	}

    }
}
