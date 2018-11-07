using System;

namespace game_server_course.Players
{

	public class Item
	{
		public DateTime CreationDate { get; set; }
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
		public int ItemLevel { get; set; }
		public ItemTypes ItemType { get; set; }
		public Guid OwnerId { get; set; }
	}
}