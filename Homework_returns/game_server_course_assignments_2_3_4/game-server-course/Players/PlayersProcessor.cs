using System;
using System.Threading.Tasks;
using game_server_course.Repositories;

namespace game_server_course.Players
{
    public class PlayersProcessor
    {
        private IRepository _repository;
        
        public PlayersProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Player> Create(NewPlayer newPlayer)
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = newPlayer.Name
            };
            await _repository.Create(player);
            return player;
        }

        public async Task<Player> Get(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task<Player[]> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
        {
            var player = await _repository.Get(id);
            await _repository.Modify(id, modifiedPlayer);
            return player;
        }
        
        public async Task<Player> Delete(Guid id)
        {
            var player = await _repository.Delete(id);
            return player;
        }
    }
}
