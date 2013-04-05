using System;
using Travellers.Core.Events;

namespace Travellers.Core.Entities
{
	public class Traveller : AggregateRoot
	{
		private string _firstname;
		private string _lastname;
		private string _country;
		private int _totalPoints;
		private int _numberOfVisits;
		private bool _isReallyCool;

		public Traveller(Guid id, string firstname, string lastname, string country) : base(id)
		{
			AddEvent(new TravellerCreated(id, firstname, lastname, country));
		}

		public Traveller(IEventStream events) : base(events) { }

		public void ChangeName(string firstname, string lastname)
		{
			if (firstname != _firstname || lastname != _lastname)
			{
				AddEvent(new TravellerNameChanged(Id, firstname, lastname));
			}
		}

		public void ChangeCountry(string country)
		{
			if (country != _country)
			{
				AddEvent(new TravellerCountryChanged(Id, country));
			}
		}

		public void VisitPlace(Guid placeId, int points, int? rating)
		{
			AddEvent(new TravellerVisitedPlace(Id, placeId, points, rating));

			if (!_isReallyCool)
			{
				if(_totalPoints > 1000 || _numberOfVisits > 15)
				{
					AddEvent(new TravellerGotReallyCool(Id));
				}
			}
		}

		protected override void Apply(IEvent evt)
		{
			// There are obviously much nicer ways to solve this, but this is easier to explain.

			if(evt is TravellerCreated)
			{
				_firstname = ((TravellerCreated) evt).Firstname;
				_lastname = ((TravellerCreated) evt).Lastname;
				_country = ((TravellerCreated) evt).Country;
			}

			else if(evt is TravellerNameChanged)
			{
				_firstname = ((TravellerNameChanged)evt).Firstname;
				_lastname = ((TravellerNameChanged)evt).Lastname;
			}

			else if(evt is TravellerCountryChanged)
			{
				_country = ((TravellerCountryChanged)evt).Country;
			}

			else if (evt is TravellerVisitedPlace)
			{
				_numberOfVisits++;
				_totalPoints += ((TravellerVisitedPlace)evt).Points;
			}

			else if (evt is TravellerGotReallyCool)
			{
				_isReallyCool = true;
			}
		}
	}
}