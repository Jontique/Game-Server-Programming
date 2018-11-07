using System;
using Microsoft.AspNetCore.Mvc;
using game_server_course.Validation;
using game_server_course.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations;

//using MongoDB.Bson;
//using MongoDB.Driver;

//using MongoDB.Driver.Builders;
//using MongoDB.Driver.GridFS;
//using MongoDB.Driver.Linq;

//using MongoDB.Bson.IO;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson.Serialization.Conventions;
//using MongoDB.Bson.Serialization.IdGenerators;
//using MongoDB.Bson.Serialization.Options;
//using MongoDB.Bson.Serialization.Serializers;
//using MongoDB.Driver.Wrappers;


namespace game_server_course.Players
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly PlayersProcessor _playerProcessor;

        public PlayersController(ILogger<PlayersController> logger, PlayersProcessor playersProcessor)
        {
            _logger = logger;
            _playerProcessor = playersProcessor;
        }

        [HttpGet]
        [Route("")]
        public Task<Player[]> GetAll()
        {
            return _playerProcessor.GetAll();
        }

        [HttpGet]
        [Route("{playerId}")]
        public Task<Player> Get(string playerId)
        {
            
            return _playerProcessor.Get(new Guid(playerId));
        }

        [HttpPost]
        [Route("")]
//        [ValidateModel]
        public Task<Player> Create(NewPlayer newPlayer)
        {
           // _logger.LogInformation("Creating player with name " + newPlayer.Name);
            return _playerProcessor.Create(newPlayer);
        }

        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> DeletePlayer(Guid playerId)
        {
            // _logger.LogInformation("Deleting player with id " + playerId.ToString());
            return _playerProcessor.Delete(playerId);
        }
    }
}
