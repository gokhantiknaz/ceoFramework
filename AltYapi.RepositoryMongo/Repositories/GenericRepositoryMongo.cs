﻿using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.MongoDbSettings;
using AltYapi.Core.Repositories.RepositoriesMongo;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;


namespace AltYapi.RepositoryMongo.Repositories
{
    public class GenericRepositoryMongo<T> : IGenericRepositoryMongo<T>
      where T : IDocument
    {

        protected readonly IMongoCollection<T> _collection;
        private readonly MongoDbSettings settings;

        public GenericRepositoryMongo(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            var db = client.GetDatabase(this.settings.DatabaseName);
            this._collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        //private readonly IMongoCollection<T> _collection;

        //public GenericRepositoryMongo(IMongoDbSettings settings)
        //{
        //    var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        //    _collection = database.GetCollection<T>(GenericRepositoryMongo<T>.GetCollectionName(typeof(T)));
        //}

        //private protected static string? GetCollectionName(Type documentType)
        //{
        //    return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
        //            typeof(BsonCollectionAttribute),
        //            true)
        //        .FirstOrDefault())?.CollectionName;
        //}

        public virtual IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<T> FilterBy(
            Expression<Func<T, bool>> filterExpression)
        {
         
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual T FindOne(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

       

        public virtual Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual T FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }


      

        public virtual Task<T> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }


        public virtual void InsertOne(T document)
        {
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(T document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(IEnumerable<T> documents)
        {
            _collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync(IEnumerable<T> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(T document)
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