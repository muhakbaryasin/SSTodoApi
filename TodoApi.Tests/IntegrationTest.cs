using Funq;
using ServiceStack;
using NUnit.Framework;
using TodoApi.ServiceInterface;
using TodoApi.ServiceModel;

namespace TodoApi.Tests
{
    public class IntegrationTest
    {
        const string BaseUri = "http://localhost:5000/";
        private readonly ServiceStackHost appHost;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(TodoServices).Assembly) { }

            public override void Configure(Container container)
            {
            }
        }

        public IntegrationTest()
        {
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [Test]
        public void Can_call_Hello_Service()
        {
            var client = CreateClient();

            var response = client.Get(new GetTodo { Id = 2 });

            Assert.That(response.Id, Is.EqualTo(2));
        }
    }
}