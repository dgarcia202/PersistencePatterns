namespace PersistencePatterns.Contracts
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}