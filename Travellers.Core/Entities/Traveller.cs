using System;
using System.Collections.Generic;

namespace Travellers.Core.Entities
{
	public class Traveller
	{
		public Traveller()
		{
			Visits = new HashSet<Visit>();
		}

		public Guid Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Country { get; set; }
		public int TotalPoints { get; set; }
		public int NumberOfVisits { get; set; }
		public bool IsReallyCool { get; set; }

		public virtual ICollection<Visit> Visits { get; set; }
	}
}