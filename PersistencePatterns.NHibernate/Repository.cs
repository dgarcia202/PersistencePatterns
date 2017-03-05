namespace PersistencePatterns.NHibernate
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using global::NHibernate.Linq;

    using PersistencePatterns.Contracts;

    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity : class, new()
    {
        private readonly UnitOfWork unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork as UnitOfWork;

            if (this.unitOfWork == null)
            {
                throw new ArgumentException("Repository received an invalid implementation of IUnitOfWork.");
            }
        }

        public Type ElementType => this.unitOfWork.Session.Query<TEntity>().ElementType;

        public Expression Expression => this.unitOfWork.Session.Query<TEntity>().Expression;

        public IQueryProvider Provider => this.unitOfWork.Session.Query<TEntity>().Provider;

        public void Add(TEntity entity)
        {
            this.unitOfWork.Session.Save(entity);
        }

        public void Update(TEntity entity)
        {
            this.unitOfWork.Session.Merge(entity);
        }

        public TEntity Get(TKey id)
        {
            return this.unitOfWork.Session.Get<TEntity>(id);
        }

        public void Remove(TEntity entity)
        {
            this.unitOfWork.Session.Delete(entity);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.unitOfWork.Session.Query<TEntity>().GetEnumerator();
        }
    }
}