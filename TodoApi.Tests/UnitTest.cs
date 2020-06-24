using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using TodoApi.ServiceInterface;
using TodoApi.ServiceModel;
using TodoApi.ServiceModel.Types;

namespace TodoApi.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<TodoServices>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public void Can_call_MyServices()
        {
            var service = appHost.Container.Resolve<TodoServices>();

            var response = (Todo)service.Get(new GetTodo { Id = 2 });

            Assert.That(response.Id, Is.EqualTo(2));
        }
    }
}
