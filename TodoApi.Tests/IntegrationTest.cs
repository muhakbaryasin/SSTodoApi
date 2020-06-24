using Funq;
using System;
using ServiceStack;
using NUnit.Framework;
using TodoApi.ServiceInterface;
using TodoApi.ServiceModel;
using TodoApi.ServiceModel.Types;
using System.Collections.Generic;

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

    public Todo AddEntry( DateTime date )
    {
      var client = CreateClient();
      return client.Post(new StoreTodo { ExpiryDate = date, Title = "Entry test", Description = "Entry test description", CompletePercentage = 0 });
    }

    public void DeleteEntry(int id)
    {
      var client = CreateClient();
      client.Delete( new DeleteTodo { Id = id } );
    }

    public List<Todo> GetTodos()
    {
      var client = CreateClient();
      return client.Get(new GetTodos() );
    }

    public Todo GetTodo(int id)
    {
      var client = CreateClient();
      return client.Get(new GetTodo { Id = id });
    }

    [Test]
    public void Test_Add_Entry()
    {
      var client = CreateClient();
      var todoItem = new StoreTodo { ExpiryDate = DateTime.Now, Title = "Entry test", Description = "Entry test description", CompletePercentage = 0 };
      var response = client.Post( todoItem );

      Assert.That(response.Title, Is.EqualTo(todoItem.Title ));

      DeleteEntry(response.Id);
    }

    [Test]
    public void Test_GetTodos()
    {
      var entry1 = AddEntry(DateTime.Now);
      var entry2 = AddEntry(DateTime.Now);

      var client = CreateClient();
      var response = client.Get( new GetTodos() );

      Assert.That(response.Count, Is.EqualTo(2));

      DeleteEntry(entry1.Id);
      DeleteEntry(entry2.Id);
    }

    [Test]
    public void Test_Update()
    {
      var entry = AddEntry(DateTime.Now);
      var newEntry = new StoreTodo { Id = entry.Id, CompletePercentage = entry.CompletePercentage, ExpiryDate = entry.ExpiryDate, Description = "Entry update test description", Title = "Entry update test" };

      var client = CreateClient();
      var response = client.Put( newEntry );

      Assert.That(response.Id, Is.EqualTo(entry.Id));
      Assert.That(response.Title, Is.EqualTo(newEntry.Title));
      Assert.That(response.Description, Is.EqualTo(newEntry.Description));

      DeleteEntry(entry.Id);
    }

    [Test]
    public void Test_Delete()
    {
      var entry = AddEntry(DateTime.Now);
      
      var client = CreateClient();
      client.Delete(new DeleteTodo { Id = entry.Id });

      Assert.That(GetTodos().Count, Is.EqualTo(0));

      DeleteEntry(entry.Id);
    }

    public void Test_GetByDateToday()
    {
      var entry1 = AddEntry(DateTime.Now);
      var entry2 = AddEntry(DateTime.Now);
      var entry3 = AddEntry(DateTime.Now);

      var client = CreateClient();
      client.Get(new GetByDateTodo { DateRangeType = DateRangeEnum.TODAY });

      Assert.That(GetTodos().Count, Is.EqualTo(3));

      DeleteEntry(entry1.Id);
      DeleteEntry(entry2.Id);
      DeleteEntry(entry3.Id);
    }

    public void Test_GetByDateTomorrow()
    {
      var entry1 = AddEntry(DateTime.Now.AddDays(1));
      var entry2 = AddEntry(DateTime.Now.AddDays(1));
      var entry3 = AddEntry(DateTime.Now.AddDays(1));

      var client = CreateClient();
      client.Get(new GetByDateTodo { DateRangeType = DateRangeEnum.TOMORROW });

      Assert.That(GetTodos().Count, Is.EqualTo(3));

      DeleteEntry(entry1.Id);
      DeleteEntry(entry2.Id);
      DeleteEntry(entry3.Id);
    }

    public void Test_GetByDateThisWeek()
    {
      var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
      var saturday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday);

      var entry1 = AddEntry(sunday);
      var entry2 = AddEntry(sunday.AddDays(2));
      var entry3 = AddEntry(saturday);

      var client = CreateClient();
      client.Get(new GetByDateTodo { DateRangeType = DateRangeEnum.THISWEEK });

      Assert.That(GetTodos().Count, Is.EqualTo(3));

      DeleteEntry(entry1.Id);
      DeleteEntry(entry2.Id);
      DeleteEntry(entry3.Id);
    }

    [Test]
    public void Test_UpdateCompletePercentage()
    {
      var entry = AddEntry(DateTime.Now);
      
      var client = CreateClient();
      var response = client.Put( new SetCompletenessPercentageTodo { Id = entry.Id, CompletePercentage = 50 } );

      Assert.That(response.CompletePercentage, Is.EqualTo(50));

      DeleteEntry(entry.Id);
    }

    [Test]
    public void Test_UpdateTodoDone()
    {
      var entry = AddEntry(DateTime.Now);

      var client = CreateClient();
      var response = client.Put(new SetDoneTodo { Id = entry.Id });

      // done entry would has completenessPercentage 100
      Assert.That(response.CompletePercentage, Is.EqualTo(100));

      DeleteEntry(entry.Id);
    }
  }
}