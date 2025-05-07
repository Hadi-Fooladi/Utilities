using Microsoft.EntityFrameworkCore;

namespace HaFT.Utilities.Razor.Test.DB;

public class Database : DbContext
{
	public DbSet<Person> Persons { get; set; } = null!;

	public Database() { }
	public Database(DbContextOptions<Database> options) : base(options) { }

	public static Database Instance => s_lazyInstance.Value;

	private static readonly Lazy<Database> s_lazyInstance = new(() =>
	{
		var options = new DbContextOptionsBuilder<Database>()
			.UseInMemoryDatabase("DB")
			.Options;

		var db = new Database(options);

		db.Database.EnsureDeleted();
		db.Database.EnsureCreated();

		db.AddRange(p("John", "Doe"), p("Jane", "Doe"));

		db.AddRange(
			Enumerable.Range(1, 100)
				.Select(_ => p(guid(), guid())));

		db.SaveChanges();
		return db;

		static string guid() => Guid.NewGuid().ToString();
		static Person p(string fn, string ln) => new() { FirstName = fn, LastName = ln };
	});
}
