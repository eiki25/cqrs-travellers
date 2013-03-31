using System;

namespace Travellers.Core.Entities
{
	public class Place
	{
		// Needed by EF
		protected Place()
		{
		}

		public Place(Guid id, string name, string description, int points)
		{
			Id = id;
			Name = name;
			Description = description;
			Points = points;
		}

		public Guid Id { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public int Points { get; protected set; }

		public void ChangeName(string name)
		{
			Name = name;
		}

		public void ChangeDescription(string description)
		{
			Description = description;
		}

		public void ChangePoints(int points)
		{
			Points = points;
		}
	}
}