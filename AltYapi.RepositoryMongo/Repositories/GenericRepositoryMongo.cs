using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.MongoDbSettings;
using AltYapi.Core.Repositories.RepositoriesMongo;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;


namespace AltYapi.RepositoryMongo.Repositories
{
    public class GenericRepositoryMongo<T> : IGenericRepositoryMongo<T>
      where T : IDocument
    {
        public IMongoContext Context { get; set; }

        //public  IMongoContext Context;
        public IMongoCollection<T> _collection;

        public GenericRepositoryMongo(IMongoContext context)
        {
            Context = context;
            _collection = Context.GetCollection<T>(typeof(T).Name);
           
        }

        public IMongoQueryable<T> AsQueryable()
        {
            return _collection .AsQueryable();
        }

        public  IEnumerable<T> FilterBy(
            Expression<Func<T, bool>> filterExpression)
        {
         
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public  IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public  T FindOne(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

       

        public  Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public  T FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }


      

        public  Task<T> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }


        public  void InsertOne(T document)
        {
            Context.AddCommand(() => _collection.InsertOneAsync(document));
        }

        public  void InsertOneAsync(T document)
        {

           Context.AddCommand(() => _collection.InsertOneAsync(document));
         //   return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(IEnumerable<T> documents)
        {
            _collection.InsertMany(documents);
        }


        public  async Task InsertManyAsync(IEnumerable<T> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public  async Task ReplaceOneAsync(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<T, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<T, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }


    }
}
