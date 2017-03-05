namespace PersistencePatterns.NHibernate
{
    using System;
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using global::NHibernate;
    using global::NHibernate.Driver;

    public class OracleSessionProvider : ISessionProvider
    {
        private ISessionFactory internalSessionFactory;

        private readonly string connectionString;

        private readonly Assembly mappingsAssembly;

        public OracleSessionProvider(string connectionString, Assembly mappingsAssembly)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");    
            }

            if (mappingsAssembly == null)
            {
                throw new ArgumentNullException("mappingsAssembly");
            }

            this.connectionString = connectionString;
            this.mappingsAssembly = mappingsAssembly;
        }

        public ISession OpenSession()
        {
            if (this.internalSessionFactory == null)
            {
                this.internalSessionFactory = this.CreateSessionFactory();
            }

            return this.internalSessionFactory.OpenSession();
        }

        private ISessionFactory CreateSessionFactory()
        {
            var databaseConfig =
                OracleDataClientConfiguration.Oracle10.Driver<OracleManagedDataClientDriver>()
                    .ConnectionString(c => c.Is(this.connectionString))
                    .AdoNetBatchSize(25);

            return Fluently.Configure()
                    .Database(databaseConfig)
                    .Mappings(m => m.FluentMappings.AddFromAssembly(this.mappingsAssembly))
                    .BuildSessionFactory();
        }
    }
}