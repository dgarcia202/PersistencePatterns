namespace PersistencePatterns.Contracts
{
    using System.Linq;

    public interface IRepository<in TKey, TEntity> : IQueryable<TEntity>
        where TEntity : class, new()
    {
        void Add(TEntity entity);

        TEntity Get(TKey id);

        void Remove(TEntity entity);

        void Update(TEntity entity);
    }
}