using Ceo.Core.Models.ModelsMongo;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Ceo.Core.Repositories.RepositoriesMongo
{
    public interface IGenericRepositoryMongo<T> where T : IDocument
    {
        IMongoQueryable<T> AsQueryable();

        IEnumerable<T> FilterBy(
            Expression<Func<T, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression);

        T FindOne(Expression<Func<T, bool>> filterExpression);

        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);

        T FindById(string id);

        Task<T> FindByIdAsync(string id);

        void InsertOne(T document);

        void InsertOneAsync(T document);

        void InsertMany(IEnumerable<T> documents);

        Task InsertManyAsync(IEnumerable<T> documents);

        void ReplaceOne(T document);

        Task ReplaceOneAsync(T document);

        void DeleteOne(Expression<Func<T, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        void DeleteMany(Expression<Func<T, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression);
    }
}
