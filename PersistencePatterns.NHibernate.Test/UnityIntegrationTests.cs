namespace PersistencePatterns.NHibernate.Test
{
    using System.Configuration;
    using System.Linq;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PersistencePatterns.Contracts;
    using PersistencePatterns.NHibernate.WebApiUnityIntegration;

    [TestClass]
    public class UnityIntegrationTests
    {
        [TestMethod]
        public void SessionProviderIsRegisteredAsSingletonTest()
        {
            // arrange
            var container = new UnityContainer();

            // act
            container.RegisterPersistencePatterns(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString(), this.GetType().Assembly);
            var registration = container.Registrations.FirstOrDefault(x => x.RegisteredType == typeof(ISessionProvider));

            // assert
            Assert.IsNotNull(registration);
            Assert.IsTrue(registration.LifetimeManager is ContainerControlledLifetimeManager);
        }

        [TestMethod]
        public void UnitOfWorkIsRegisteredWithHierarchical()
        {
            // arrange
            var container = new UnityContainer();

            // act
            container.RegisterPersistencePatterns(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString(), this.GetType().Assembly);
            var registration = container.Registrations.FirstOrDefault(x => x.RegisteredType == typeof(IUnitOfWork));

            // assert
            Assert.IsNotNull(registration);
            Assert.IsTrue(registration.LifetimeManager is HierarchicalLifetimeManager);
        }
    }
}