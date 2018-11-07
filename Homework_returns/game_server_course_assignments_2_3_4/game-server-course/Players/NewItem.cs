using System;

using System.ComponentModel.DataAnnotations;
using game_server_course.Validation;

namespace game_server_course.Players
{
	public class NewItem
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		//[DateValidationAttribute]
    	//[DataType(DataType.Date)]
		public DateTime CreationDate { get; set; }
		[Range(1,99)]
		public int ItemLevel { get; set; }
		public int Price { get; set; }
		[EnumDataType(typeof(ItemTypes))]
		public ItemTypes ItemType { get; set; }

		public Guid OwnerId { get; set; }
	}
}