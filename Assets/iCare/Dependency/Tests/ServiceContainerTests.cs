using System;
using NUnit.Framework;

namespace iCare.Tests
{
    [TestFixture]
    public class ServiceContainerTests
    {
        [SetUp]
        public void SetUp()
        {
            _serviceContainer = new ServiceContainer();
        }

        ServiceContainer _serviceContainer;

        [Test]
        public void RegisterService_ShouldRegisterServiceSuccessfully()
        {
            _serviceContainer.Register(new object());

            Assert.DoesNotThrow(() => _serviceContainer.Resolve<object>());
        }

        [Test]
        public void RegisterService_ThrowsException_WhenServiceTypeIsAlreadyRegistered()
        {
            _serviceContainer.Register(new object());

            Assert.Throws<InvalidOperationException>(() => _serviceContainer.Register(new object()));
        }

        [Test]
        public void ResolveService_ReturnsRegisteredService()
        {
            var expectedService = new object();
            _serviceContainer.Register(expectedService);

            var resolvedService = _serviceContainer.Resolve<object>();

            Assert.AreEqual(expectedService, resolvedService);
        }

        [Test]
        public void ResolveService_ThrowsException_WhenServiceIsNotRegistered()
        {
            Assert.Throws<InvalidOperationException>(() => _serviceContainer.Resolve<object>());
        }

        [Test]
        public void RegisterServiceWithId_ShouldRegisterServiceSuccessfully()
        {
            _serviceContainer.Register(new object(), typeof(object), "TestID");

            Assert.DoesNotThrow(() => _serviceContainer.Resolve<object>("TestID"));
        }

        [Test]
        public void RegisterServiceWithId_ThrowsException_WhenServiceWithSameIdIsAlreadyRegistered()
        {
            _serviceContainer.Register(new object(), typeof(object), "TestID");

            Assert.Throws<InvalidOperationException>(() =>
                _serviceContainer.Register(new object(), typeof(object), "TestID"));
        }

        [Test]
        public void ResolveServiceById_ReturnsRegisteredService()
        {
            var expectedService = new object();
            _serviceContainer.Register(expectedService, typeof(object), "TestID");

            var resolvedService = _serviceContainer.Resolve<object>("TestID");

            Assert.AreEqual(expectedService, resolvedService);
        }

        [Test]
        public void ResolveServiceById_ThrowsException_WhenServiceWithIdIsNotRegistered()
        {
            Assert.Throws<InvalidOperationException>(() => _serviceContainer.Resolve<object>("TestID"));
        }

        [Test]
        public void RegisterFactory_ShouldRegisterFactorySuccessfully()
        {
            Func<object> factory = () => new object();
            _serviceContainer.RegisterFactory(factory);

            Assert.DoesNotThrow(() => _serviceContainer.Resolve<object>());
        }

        [Test]
        public void RegisterFactory_ThrowsException_WhenFactoryForTypeIsAlreadyRegistered()
        {
            Func<object> factory = () => new object();
            _serviceContainer.RegisterFactory(factory);

            Assert.Throws<InvalidOperationException>(() => _serviceContainer.RegisterFactory(factory));
        }

        [Test]
        public void ResolveFactory_ReturnsServiceFromFactory()
        {
            var expectedService = new object();
            _serviceContainer.RegisterFactory(Factory);

            var resolvedService = _serviceContainer.Resolve<object>();

            Assert.AreEqual(expectedService, resolvedService);
            return;

            object Factory()
            {
                return expectedService;
            }
        }

        [Test]
        public void ResolveFactory_ReturnsServiceFromFactory_WithGivenType()
        {
            var expectedService = new object();
            _serviceContainer.RegisterFactory(Factory, typeof(string));

            var resolvedService = _serviceContainer.Resolve(typeof(string));

            Assert.AreEqual(expectedService, resolvedService);
            return;

            object Factory()
            {
                return expectedService;
            }
        }


        [Test]
        public void Clear_ShouldRemoveAllRegisteredServicesAndFactories()
        {
            _serviceContainer.Register(new object());
            _serviceContainer.Register(new object(), typeof(object), "TestID");
            _serviceContainer.Clear();

            Assert.Throws<InvalidOperationException>(() => _serviceContainer.Resolve<object>());
            Assert.Throws<InvalidOperationException>(() => _serviceContainer.Resolve<object>("TestID"));
        }
    }
}