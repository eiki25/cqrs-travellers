using System.Data.Entity;
using Travellers.Core.Entities;

namespace Travellers.Infrastructure
{
	public class TravellersDbContext : DbContext
	{
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Traveller>().HasKey(x => x.Id);
			modelBuilder.Entity<Place>().HasKey(x => x.Id);
			modelBuilder.Entity<Visit>().HasKey(x => new { x.TravellerId, x.VisitNumber });

			base.OnModelCreating(modelBuilder);
		}
	}
}
