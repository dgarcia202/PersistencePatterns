namespace PersistencePatterns.NHibernate
{
    using System;

    using global::NHibernate;

    using PersistencePatterns.Contracts;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession internalSession;

        private bool isDirty;

        public UnitOfWork(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null)
            {
                throw new ArgumentNullException("sessionProvider");
            }

            this.internalSession = sessionProvider.OpenSession();
            this.internalSession.FlushMode = FlushMode.Commit;
        }

        internal ISession Session
        {
            get
            {
                if (this.isDirty)
                {
                    throw new InvalidOperationException("This unit of work is unusable due to previous errors.");
                }

                return this.internalSession;
            }
        }

        public void Dispose()
        {
            if (this.internalSession != null)
            {
                if (this.internalSession.IsOpen)
                {
                    this.internalSession.Close();
                }

                this.internalSession.Dispose();
            }
        }

        public void Commit()
        {
            var tx = this.internalSession.BeginTransaction();

            try
            {
                // triggers ISession flush
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                this.isDirty = true;
                throw;
            }
            finally
            {
                tx.Dispose();
            }
        }
    }
}