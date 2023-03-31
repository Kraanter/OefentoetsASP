using Microsoft.EntityFrameworkCore;
using Oefentoets1MVC.Data.Entities;

namespace Oefentoets1MVC.Data;

public class CijferRegistratieDBContext: DbContext 
{
	public DbSet<Vak> Vakken { get; set; }
	
	public DbSet<Poging> Pogingen { get; set; }

	public CijferRegistratieDBContext(DbContextOptions<CijferRegistratieDBContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Vak>(options =>
		{
			options.HasData(new Vak() { Id = 1, Naam = "Server", EC = 4 });
			options.HasData(new Vak() { Id = 2, Naam = "C#", EC = 4 });
			options.HasData(new Vak() { Id = 3, Naam = "Databases", EC = 3 });
			options.HasData(new Vak() { Id = 4, Naam = "UML", EC = 3 });
			options.HasData(new Vak() { Id = 5, Naam = "KBS", EC = 9 });
		});

		modelBuilder.Entity<Poging>()
			.HasOne(p => p.Vak)
			.WithMany(v => v.Pogingen)
			.HasForeignKey(p => p.VakNaam)
			.HasPrincipalKey(v => v.Naam);

		modelBuilder.Entity<Vak>()
			.HasMany(v => v.Pogingen)
			.WithOne(v => v.Vak);
	}
}