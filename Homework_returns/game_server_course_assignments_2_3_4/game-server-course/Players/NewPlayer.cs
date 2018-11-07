using System;
using Microsoft.AspNetCore.Mvc;

namespace game_server_course.Players
{
	public class NewPlayer
	{
	    public Guid Id { get; set; }
	    public string Name { get; set; }
	    public int Score { get; set; }
	    public int Level { get; set; }
	    public bool IsBanned { get; set; }
	    public DateTime CreationTime { get; set; }
	}
}