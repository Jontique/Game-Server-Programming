using System;
using Microsoft.AspNetCore.Mvc;
using game_server_course.Validation;
using game_server_course.Players;
using game_server_course.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations;

namespace game_server_course.Players
{
	public class Player
	{
	    public Guid Id { get; set; }
	    public string Name { get; set; }
	    public int Score { get; set; }
	    public int Level { get; set; }
	    public bool IsBanned { get; set; }
	    public DateTime CreationTime { get; set; }
	    public List<Item> Items { get; set; }
	}
}
