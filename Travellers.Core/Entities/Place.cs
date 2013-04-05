using System;
using Travellers.Core.Events;

namespace Travellers.Core.Entities
{
	public class Place : AggregateRoot
	{
		private string _name;
		private string _description;
		private int _points;

		public Place(Guid id, string name, string description, int points) : base(id)
		{
			AddEvent(new PlaceCreated(id, name, description, points));
		}

		public Place(IEventStream events) : base(events) { }

		public void ChangeName(string name)
		{
			if (name != _name)
			{
				AddEvent(new PlaceNameChanged(Id, name));
			}
		}

		public void ChangeDescription(string description)
		{
			if (description != _description)
			{
				AddEvent(new PlaceDescriptionChanged(Id, description));
			}
		}

		public void ChangePoints(int points)
		{
			if (points != _points)
			{
				AddEvent(new PlacePointsChanged(Id, points));
			}
		}

		protected override void Apply(IEvent evt)
		{
			// There are obviously much nicer ways to solve this, but this is easier to explain.

			if (evt is PlaceCreated)
			{
				_name = ((PlaceCreated)evt).Name;
				_description = ((PlaceCreated)evt).Description;
				_points = ((PlaceCreated)evt).Points;
			}

			else if (evt is PlaceNameChanged)
			{
				_name = ((PlaceNameChanged)evt).Name;
			}

			else if (evt is PlaceDescriptionChanged)
			{
				_description = ((PlaceDescriptionChanged)evt).Description;
			}

			else if (evt is PlacePointsChanged)
			{
				_points = ((PlacePointsChanged)evt).Points;
			}
		}
	}
}