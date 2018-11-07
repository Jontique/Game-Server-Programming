using System;
using Microsoft.AspNetCore.Mvc;
using game_server_course.Validation;
using game_server_course.Repositories;
using game_server_course.Players;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace game_server_course.Repositories
{
	public class InMemoryRepository : IRepository
	{
		private List<Player> players;
		private List<Item> items;

		public InMemoryRepository()
		{
			players = new List<Player>();
			items = new List<Item>();
		}

		private Player GetPlayerById(Guid id)
		{
			foreach(Player p in players)
			{
				if(p.Id == id)
				{
					return p;
				}
			}
			return null;
		}

		public async Task<Player> Get(Guid id)
		{
			Player player = GetPlayerById(id);
			//Task<Player> p = new Task<Player>(player);
			return player;
		}	

	    public async Task<Player[]> GetAll()
	    {
	    	return players.ToArray();
	    }

	    public async Task<Player> Create(Player player)
	    {
	    	players.Add(player);
	    	return null;
	    }

	    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
	    {
	    	for(int i=0; i < players.Count; ++i)
	    	{
	    		if(players[i].Id == id)
	    		{
	   				players[i].Score = player.Score;
	   			}
	   		}
	   		return null;
	    }

	    public async Task<Player> Delete(Guid id)
	    {
	    	int index = -1;
	    	for(int i=0; i < players.Count; ++i)
	   		{
	   			if(players[i].Id == id)
	   			{
	   				index = i;
	    			}
    		}
       		if(index>=0)
	    	{
	  			players.RemoveAt(index);
	   		}
	   		return null;
	    }

	    public async Task<Item> CreateItem(Item item)
	    {
	    	items.Add(item);
	    	return item;
	    }

	    public async Task<Item> UpdateItem(Guid id, NewItem newItem)
	    {
	   		int index = -1;
	   		for(int i = 0; i < items.Count; ++i)
	   		{
	   			if(items[i].Id == id)
	   			{
	   				index = i;
	   			}
	   		}
	   		if(index>=0)
	   		{
	   			items[index].Name = newItem.Name;
	   			//if(newItem.Name) items[i].Name = newItem.Name;
	   			items[index].ItemLevel = newItem.ItemLevel;
	   			items[index].Price = newItem.Price;
	   			items[index].OwnerId = newItem.OwnerId;
	   			items[index].CreationDate = newItem.CreationDate; 
	   			return items[index];
	   		}
	    	else return null;
	    }

	    public async Task<Item> ModifyItem(Guid id, ModifiedItem modifiedItem)
	    {
	    	int index = -1;
	    	for(int i = 0; i < items.Count; ++i)
	    	{
	    		if(items[i].Id == id)
	    		{
	    			index = i;
	    		}
	    	}
	    	if(index>=0)
	    	{
	    		items[index].OwnerId = modifiedItem.OwnerId;
	    		return items[index];
	    	}
	    	else return null;
	    }

	    public async Task<Item[]> GetItemsOnPlayer(Guid id)
	    {
	    	List<Item> itemList = new List<Item>();
	    	
	    	int index = -1;
	    	for(int i = 0; i < players.Count; ++i)
	    	{
	    		if(players[i].Id == id)
	    		{
	    			index = i;
	    		}
	    	}
	    	if(index>=0)
	    	{
		    	for(int j = 0; j < items.Count; ++j)
		    	{
		    		if(items[j].OwnerId == players[index].Id)
		    		{
		    			itemList.Add(items[j]);
		    		}
	    		}
	    	}
			return itemList.ToArray();
	    }

	    public async Task<Item> DeleteItem(Guid id)
	    {
    		int index = -1;
    		for(int i = 0; i < items.Count; ++i)
    		{
    			if(items[i].Id == id)
    			{
    				index = i;
    			}
    		}
    		if(index >= 0)
    		{
    			var returnValue = items[index];
    			items.RemoveAt(index);
    			return returnValue;
    		}
    		else return null;
	    }

	    public async Task<Item> GetItem(Guid id)
	    {
			foreach(Item i in items)
			{
				if(i.Id == id)
				{
					return i;
				}
			}
			return null;
		}

		public async Task<Item[]> GetAllItems()
		{
			if(items.Count < 1) return null;
			return items.ToArray();
		}
	}
}