using Microsoft.EntityFrameworkCore;

namespace HaFT.Utilities.Razor.Test.DB;

public class Database : DbContext
{
	public DbSet<Person> Persons { get; set; } = null!;

	public Database() { }
	public Database(DbContextOptions<Database> options) : base(options) { }
}
