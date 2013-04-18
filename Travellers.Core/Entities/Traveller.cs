using System;
using System.Collections.Generic;
using System.Linq;

namespace Travellers.Core.Entities
{
	public class Traveller
	{
		// Needed by EF
		protected Traveller()
		{
			Visits = new HashSet<Visit>();
		}

		public Traveller(Guid id, string firstname, string lastname, string country) : this()
		{
			Id = id;
			Firstname = firstname;
			Lastname = lastname;
			Country = country;
		}

		public Guid Id { get; protected set; }
		public string Firstname { get; protected set; }
		public string Lastname { get; protected set; }
		public string Country { get; protected set; }
		public int TotalPoints { get; protected set; }
		public int NumberOfVisits { get; protected set; }
		public bool IsReallyCool { get; protected set; }

		protected virtual ICollection<Visit> Visits { get; set; }

		public void ChangeName(string firstname, string lastname)
		{
			Firstname = firstname;
			Lastname = lastname;
		}

		public void ChangeCountry(string country)
		{
			Country = country;
		}

		public void VisitPlace(Guid placeId, int points, int? rating)
		{
			Visits.Add(new Visit(Id, placeId, NumberOfVisits + 1, rating));

			NumberOfVisits++;
			TotalPoints += points;
			IsReallyCool = (TotalPoints > 1000 || NumberOfVisits > 15);
		}
	}
}