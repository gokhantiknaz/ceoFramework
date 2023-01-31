using AltYapi.Core.Models;
using AltYapi.Core.MongoDbSettings;
using AltYapi.RepositoryMongo.Repositories;
using MongoDB.Driver;


namespace AltYapi.RepositoryMongo
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase Database;
        public  IClientSessionHandle Session;
        private readonly IMongoClient MongoClient;
        private static  List<Func<Task>> _commands;
        public MongoContext( IDatabaseSettings databaseSettings)
        {
             MongoClient = new MongoClient(databaseSettings.ConnectionString);
            //Clientımızı oluşturduk client üzerinden veritabanını aldım. "databaseSettings.DatabaseName" hangi database'e bağlanmak istediğimizi belirttik.
             Database = MongoClient.GetDatabase(databaseSettings.DatabaseName);
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
