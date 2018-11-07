using System;
using Microsoft.AspNetCore.Mvc;
using game_server_course.Validation;
using game_server_course.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace game_server_course.Players
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItemsController : ControllerBase
	{
		private readonly ILogger<ItemsController> _logger;
		private readonly ItemsProcessor _itemsProcessor;

		public ItemsController(ILogger<ItemsController> logger, ItemsProcessor itemsProcessor)
		{
			_logger = logger;
			_itemsProcessor = itemsProcessor;
		}

		[HttpGet]
		[Route("players/{playerId}/items")]
		public async Task<Item[]> GetItemsOnPlayer(Guid playerId)
		{
			return await _itemsProcessor.GetItemsOnPlayer(playerId);
		}

		[HttpDelete]
		[Route("players/{playerId}/items")]
		public async Task<Item[]> DeleteAllItemsOnPlayer(Guid playerId)
		{
			return await _itemsProcessor.DeleteAllItemsOnPlayer(playerId);
		}

		[HttpDelete]
		[Route("players/items/{itemId}")]
		public async Task<Item> DeleteItem(string itemId)
		{
			return await _itemsProcessor.DeleteItem(new Guid(itemId));
		}

		[HttpPost]
		[Route("players/items/{itemId}")]
		public async Task<Item> UpdateItem(string itemId, NewItem item)
		{
			return await _itemsProcessor.UpdateItem(new Guid(itemId), item);
		}

		[HttpPost]
		[Route("players/items/create")]
		public async Task<Item> CreateItem(NewItem newItem)
		{
			//Item item = new Item();
			//item.Id = Guid.NewGuid();
			//tem.Name = newItem.Name;
			//item.Price = newItem.Price;
			//item.ItemType = newItem.ItemType;
			//item.CreationDate = DateTime.Today; // get time
			return await _itemsProcessor.CreateItem(newItem);
		}

		[HttpGet]
		[Route("players/items")]
		public async Task<Item[]> GetAllItems()
		{
			return await _itemsProcessor.GetAllItems();
		}

		//[HttpGet]
		//[Route("{")]
	}
}