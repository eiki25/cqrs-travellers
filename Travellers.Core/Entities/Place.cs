using System;

namespace Travellers.Core.Entities
{
	public class Place
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Points { get; set; }
	}
}