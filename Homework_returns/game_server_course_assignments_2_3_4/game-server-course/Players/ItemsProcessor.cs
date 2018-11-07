using System;
using System.Threading.Tasks;
using game_server_course.Repositories;
using game_server_course.Players;

namespace game_server_course.Players
{

        public enum ItemTypes
        {
            Sword,
            Mace,
            Lightsaber
        }
    public class ItemsProcessor
    {

        private IRepository _repository;
        
        public ItemsProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Item> CreateItem(NewItem newItem)
        {
            var player = await _repository.Get(newItem.OwnerId);
            if(player.Level < 3 && newItem.ItemType == ItemTypes.Sword)
            {
                throw new TooLowLevelException();
            }
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                OwnerId = newItem.OwnerId,
                Name = newItem.Name,
                Price = newItem.Price,
                ItemType = newItem.ItemType,
                CreationDate = DateTime.Today
            };
            await _repository.CreateItem(item);
            return item;
        }

        public async Task<Item> GetItem(Guid itemId)
        {
            return await _repository.GetItem(itemId);
        }

        public async Task<Item[]> GetAllItems()
        {
            return await _repository.GetAllItems();
        }

        public async Task<Item[]> GetItemsOnPlayer(Guid id)
        {
            return await _repository.GetItemsOnPlayer(id);
        }

        public async Task<Item> UpdateItem(Guid id, NewItem newItem)
        {
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                OwnerId = newItem.OwnerId,
                Name = newItem.Name,
                Price = newItem.Price,
                ItemType = newItem.ItemType
            };
            await _repository.UpdateItem(id, newItem);
            return item;
        }

        public async Task<Item> ModifyItem(Guid id, ModifiedItem modifiedItem)
        {
            var item = await _repository.GetItem(id);
            //var item = new Item()
            //{
            //    Id = id,
            //    OwnerId = newItem.OwnerId,
            //    Name = newItem.Name,
            //    Price = newItem.Price,
            //    ItemType = newItem.ItemType
            //};
            item.OwnerId = modifiedItem.OwnerId;
            await _repository.ModifyItem(id, modifiedItem);
            return item;
        }
        public async Task<Item> DeleteItem(Guid itemId)
        {
            var item = await _repository.DeleteItem(itemId);
            return item;
        }

        public async Task<Item[]> DeleteAllItemsOnPlayer(Guid id)
        {
            return null; // not implemented
        }

    }
}
