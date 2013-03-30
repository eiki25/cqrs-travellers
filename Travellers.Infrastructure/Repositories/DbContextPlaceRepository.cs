using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Travellers.Core.Entities;

namespace Travellers.Infrastructure.Repositories
{
	public class DbContextPlaceRepository : DbContextRepository<Place>
	{
		public DbContextPlaceRepository(DbContext context) : base(context) { }

		public override IEnumerable<Place> Search(string query)
		{
			return string.IsNullOrWhiteSpace(query)
					   ? base.All()
					   : base.Context.Set<Place>().Where(x => x.Name.Contains(query) || x.Description.Contains(query));
		}
	}
}