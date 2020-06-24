using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using System;
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
        public void Can_call_TodoService_GetTodo()
        {
            var service = appHost.Container.Resolve<TodoServices>();
            var entry = (Todo)service.Post(new StoreTodo { Title = "Unit test", Description = "Unit test description", ExpiryDate = DateTime.Now, CompletePercentage = 0 });

            var response = (Todo)service.Get(new GetTodo { Id = entry.Id });

            Assert.That(response.Id, Is.EqualTo( entry.Id ));
        }
    }
}
