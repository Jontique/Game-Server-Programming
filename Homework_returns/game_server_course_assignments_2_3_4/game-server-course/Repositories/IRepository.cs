using System;
using Microsoft.AspNetCore.Mvc;
using game_server_course.Validation;
using game_server_course.Players;
using game_server_course.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations;

namespace game_server_course.Repositories
{
	public interface IRepository
	{
	    Task<Player> Get(Guid id);
	    Task<Player[]> GetAll();
	    Task<Player> Create(Player player);
	    Task<Player> Modify(Guid id, ModifiedPlayer player);
	    Task<Player> Delete(Guid id);
	    Task<Item> GetItem(Guid id);
	    Task<Item[]> GetAllItems();
	    Task<Item> CreateItem(Item item);
	    Task<Item> UpdateItem(Guid id, NewItem modifiedItem);
	    Task<Item> ModifyItem(Guid id, ModifiedItem modifiedItem);
	    Task<Item> DeleteItem(Guid id);
	    Task<Item[]> GetItemsOnPlayer(Guid playerId);
	}
}