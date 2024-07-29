using System;
using NUnit.Framework;

namespace iCare.Di.Tests {
    [TestFixture]
    public class ServiceContainerTests {
        [SetUp]
        public void Setup() {
            _container = new ServiceContainer();
        }

        ServiceContainer _container;

        [Test]
        public void RegisterAndResolve_SingleObject() {
            var service = new SampleService();
            _container.Register(service, typeof(SampleService).GetServiceKey());

            var resolved = _container.Resolve<SampleService>(typeof(SampleService).GetServiceKey());

            Assert.NotNull(resolved);
            Assert.IsInstanceOf<SampleService>(resolved);
        }

        [Test]
        public void RegisterAndResolve_Func() {
            Func<SampleService> serviceFactory = () => new SampleService();
            var key = typeof(SampleService).GetServiceKey();
            _container.Register(serviceFactory, key);

            var resolved = _container.Resolve<SampleService>(key);

            Assert.NotNull(resolved);
            Assert.IsInstanceOf<SampleService>(resolved);
        }

        [Test]
        public void Resolve_ThrowsIfKeyNotRegistered() {
            Assert.Throws<InvalidOperationException>(() => _container.Resolve<SampleService>(1000));
        }

        [Test]
        public void RegisterAndResolve_WithStringKey() {
            var service = new SampleService();
            var stringKey = "sampleServiceKey";
            _container.Register(service, stringKey.GetServiceKey());

            var resolved = _container.Resolve<SampleService>(stringKey.GetServiceKey());

            Assert.NotNull(resolved);
            Assert.IsInstanceOf<SampleService>(resolved);
        }

        [Test]
        public void Clear_RemovesAllServices() {
            var service = new SampleService();
            _container.Register(service, typeof(SampleService).GetServiceKey());

            _container.Clear();

            Assert.Throws<InvalidOperationException>(() => _container.Resolve<SampleService>(typeof(SampleService).GetServiceKey()));
        }

        sealed class SampleService { }
    }
}