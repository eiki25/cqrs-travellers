using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Travellers.Core.Entities;

namespace Travellers.Infrastructure.Repositories
{
	public class DbContextTravellerRepository : DbContextRepository<Traveller>
	{
		public DbContextTravellerRepository(DbContext context) : base(context) { }

		public override IEnumerable<Traveller> Search(string query)
		{
			return string.IsNullOrWhiteSpace(query)
					   ? base.All()
					   : base.Context.Set<Traveller>().Where(x => x.Firstname.Contains(query) || x.Lastname.Contains(query));
		}
	}
}