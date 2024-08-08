using NUnit.Framework;
using UnityEngine;

namespace iCare.Di.Tests {
    public sealed class ServiceLocatorTests {
        MonoContainer _container;
        TestMonoBehaviour _testMonoBehaviour;
        GameObject _testObject;

        [SetUp]
        public void SetUp() {
            _testObject = new GameObject();
            _container = _testObject.AddComponent<MonoContainer>();

            _testMonoBehaviour = _testObject.AddComponent<TestMonoBehaviour>();
        }

        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(_testObject);
            ServiceLocator.ClearAll();
        }

        [Test]
        public void Global_InitializesGlobalContainer() {
            var container = ServiceLocator.Global;

            Assert.IsNotNull(container);
        }


        [Test]
        public void ForSceneOf_CreatesNewContainerForScene() {
            var container = ServiceLocator.ForSceneOf(_testMonoBehaviour);

            Assert.IsNotNull(container);
            Assert.IsTrue(ServiceLocator.ForSceneOf(_testMonoBehaviour) == container);
        }

        [Test]
        public void For_ReturnsMonoContainerIfAvailable() {
            var container = ServiceLocator.For(_container);
            Assert.AreEqual(_container.Value, container);
        }

        [Test]
        public void For_ReturnsSceneContainerIfMonoContainerNotFound() {
            Object.DestroyImmediate(_container);
            var container = ServiceLocator.For(_testMonoBehaviour);

            Assert.IsNotNull(container);
            Assert.IsTrue(ServiceLocator.ForSceneOf(_testMonoBehaviour) == container);
        }

        sealed class TestMonoBehaviour : MonoBehaviour { }
    }
}