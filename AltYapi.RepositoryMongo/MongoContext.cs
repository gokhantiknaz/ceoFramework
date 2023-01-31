using AltYapi.Core.MongoDbSettings;
using AltYapi.RepositoryMongo.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.RepositoryMongo
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database;
        public IClientSessionHandle Session;
        private readonly IMongoClient MongoClient;
        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;

            MongoClient=new MongoClient(_configuration["MongoSettings:Connection"]);
            Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
          

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();
              
                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        //private void ConfigureMongo()
        //{
        //    if (MongoClient != null)
        //    {
        //        return;
        //    }

        //    // Configure mongo (You can inject the config, just to simplify)
        //   // MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

        //    Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        //}

        public IMongoCollection<T> GetCollection<T>(string name)
        {
          

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}
