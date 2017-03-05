namespace PersistencePatterns.NHibernate
{
    using System;
    using System.Linq.Expressions;

    using PersistencePatterns.Contracts;

    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity : class, new()
    {
        public Type ElementType => typeof(TEntity);

        public Expression Expression => 
    }
}