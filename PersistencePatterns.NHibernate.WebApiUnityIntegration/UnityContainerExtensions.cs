namespace PersistencePatterns.NHibernate.WebApiUnityIntegration
{
    using System.Reflection;

    using Microsoft.Practices.Unity;

    using PersistencePatterns.Contracts;

    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterPersistencePatterns(this IUnityContainer container, string connectionString, Assembly mappingsAssembly)
        {
            return container
                .RegisterType<ISessionProvider, OracleSessionProvider>(new ContainerControlledLifetimeManager(), new InjectionConstructor(connectionString, mappingsAssembly))
                .RegisterType<IUnitOfWork, IUnitOfWork>(new HierarchicalLifetimeManager());
        }
    }
}