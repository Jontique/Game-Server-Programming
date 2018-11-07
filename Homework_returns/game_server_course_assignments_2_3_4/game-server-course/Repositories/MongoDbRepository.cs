using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using game_server_course.Players;

namespace game_server_course.Repositories
{
	public class MongoDbRepository : IRepository
	{
	  	//private MongoClient _mongoClient;
    	//private MongoServer _mongoServer;
    	private IMongoDatabase _database;
    	private IMongoCollection<Player> _players;
        private IMongoCollection<Item> _items;

    	private IMongoCollection<BsonDocument> _bsonDocumentCollection;

    	public MongoDbRepository()
    	{
    		var _mongoClient = new MongoClient("mongodb://localhost:27017");
    		_database = _mongoClient.GetDatabase("game_server_course");
    		_players = _database.GetCollection<Player>("players");
    		_items = _database.GetCollection<Item>("items");
            _bsonDocumentCollection = _database.GetCollection<BsonDocument>("players");
    	}

    	public async Task<Player> Create(Player player)
    	{
    		await _players.InsertOneAsync(player);
    		return player;
    	}

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Id", id);
            var player = await _players.Find(filter).FirstAsync();
            player.Score = modifiedPlayer.Score;
            await _players.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player> Delete(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Id", id);
            await _players.DeleteOneAsync(filter);
            return null;
        }

    	public async Task<Player[]> GetAll()
    	{
    		List<Player> players = await _players.Find(new BsonDocument()).ToListAsync();
    		return players.ToArray(); 
    	}

        public async Task<Player> Get(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Id", id);
            return await _players.Find(filter).FirstAsync();
        }

    	public async Task<Player[]> GetMinscore(int minscore)
    	{
    		FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Score", minscore);
    		List<Player> players = await _players.Find(filter).ToListAsync();
    		return players.ToArray();
    	}

    	//public async Task<Player[]> GetPlayersWithItemCount(int count)
        //{
        //    FilterDefinition<Player> filter = Builders<Player>.Filter.Size("Items", count);
        //    List<Player> players = await _players.Find(filter).ToListAsync();
        //    return players.ToArray();
        //}

        //public async Task<Player[]> GetUsersWithTag(int tag)
        //{
        //    FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Tags", tag);
        //    List<Player> players = await _players.Find(filter).ToListAsync();
        //    return players.ToArray();
        //}

        public async Task<Player> GetPlayerWithName(string name)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Name", name);
            return await _players.Find(filter).FirstOrDefaultAsync();
        }

        //public Task<Player> GetPlayer(Guid id)
        //{
        //    FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Id", id);
        //    return _players.Find(filter).FirstAsync();
        //}

        public async Task<Player[]> GetBetweenLevelsAsync(int minLevel, int maxLevel)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Level", 18) & Builders<Player>.Filter.Lte("Level", 30);
            List<Player> players = await _players.Find(filter).ToListAsync();
            return players.ToArray();
        }


    //    public Task<Player> IncreasePlayerScoreAndRemoveItem(Guid playerId, Guid itemId, int score)
    //    {
    //        var pull = Builders<Player>.Update.PullFilter(p => p.Items, i => i.Id == itemId);
    //        var inc = Builders<Player>.Update.Inc(p => p.Score, score);
    //        var update = Builders<Player>.Update.Combine(pull, inc);
    //        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
//
//    //        return _players.FindOneAndUpdateAsync(filter, update);
    //    }

        public async Task<Player> UpdatePlayer(Player player)
        {
            var filter = Builders<Player>.Filter.Eq("Id", player.Id);
            var p = await _players.ReplaceOneAsync(filter, player);
            return null;
        }

        public async Task<Player[]> GetAllSortedByScoreDescending()
        {
            SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending(p => p.Score);
            List<Player> players = await _players.Find(new BsonDocument()).Sort(sortDef).ToListAsync();
            return players.ToArray();
        }

        //public async Task<Player> IncrementPlayerScore(string id, int increment)
        //{
        //    var filter = Builders<Player>.Filter.Eq("Id", id);
        //    var incrementScoreUpdate = Builders<Player>.Update.Inc(p => p.Score, increment);
        //    var options = new FindOneAndUpdateOptions<Player>()
        //    {
        //        ReturnDocument = ReturnDocument.After
        //    };
        //    Player player = await _players.FindOneAndUpdateAsync(filter, incrementScoreUpdate, options);
        //    return player;
        //}

        
        public async Task<Item> CreateItem(Item item)
        {
            await _items.InsertOneAsync(item);
            return null;
        }

        public async Task<Item> DeleteItem(Guid itemId)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", itemId);
            await _items.DeleteOneAsync(filter);
            return null;
        }

       	public async Task<Item[]> GetAllItems()
      	{
       	    FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("OwnerId", new Guid("0"));
            List<Item> items = await _items.Find(filter).ToListAsync();
            return items.ToArray(); 
       	}

        public async Task<Item> GetItem(Guid itemId)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", itemId);
            var item = await _items.DeleteOneAsync(filter);
            return null;
        }

        public async Task<Item> UpdateItem(Guid itemId, NewItem newItem)
        {
            var filter = Builders<Item>.Filter.Eq("Id", itemId);
            var item = new Item();

            await _items.ReplaceOneAsync(filter, item);
            return null;
        }

        public async Task<Item> ModifyItem(Guid id, ModifiedItem modifiedItem)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", id);
            var i = await _items.Find(filter).FirstAsync();
            await _items.ReplaceOneAsync(filter, i);
            return i;
        }

        public async Task<Item[]> GetItemsOnPlayer(Guid playerId)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("OwnerId", playerId);
            List<Item> items = await _items.Find(filter).ToListAsync();
            //p.Items = items;
            return items.ToArray();
        }
	}
}