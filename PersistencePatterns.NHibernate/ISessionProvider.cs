namespace PersistencePatterns.NHibernate
{
    using global::NHibernate;

    public interface ISessionProvider
    {
        ISession OpenSession();
    }
}