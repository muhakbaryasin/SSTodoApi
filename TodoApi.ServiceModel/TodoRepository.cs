using TodoApi.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System.Collections.Generic;

namespace TodoApi.ServiceModel
{
  public class TodoRepository : IRepository
  {
    private IDbConnectionFactory _dbConnectionFactory;

    public TodoRepository(IDbConnectionFactory dbConnectionFactory) {
      this._dbConnectionFactory = dbConnectionFactory;
      Initialize();
    }

    private IDbConnectionFactory ConnectionFactory
    {
      get
      {
        return this._dbConnectionFactory;
      }
    }

    public void Initialize()
    {
      using ( var db = this.ConnectionFactory.Open() )
      {
        db.CreateTableIfNotExists<Todo>();
      }
    }

    public void Delete( DeleteTodo request )
    {
      using( var db = ConnectionFactory.Open() )
      {
        db.DeleteById<Todo>(request.Id);
      }
    }

    public Todo Read( GetTodo request)
    {
      using (var db = ConnectionFactory.Open() )
      {
        return db.SingleById<Todo>(request.Id);
      }
    }

    public List<Todo> Read()
    {
      using (var db = ConnectionFactory.Open() )
      {
        return db.Select<Todo>();
      }
    }

    public Todo Store( StoreTodo request )
    {
      var todo = request.ConvertTo<Todo>();

      using (var db = ConnectionFactory.Open())
      {
        bool success = true;

        if (request.Id <= 0)
        {
          success = db.Save(todo);
        } else
        {
          success = db.Update(todo) == 1;

          if (success)
            todo = db.SingleById<Todo>(request.Id);
        }

        if (!success)
          return null;
      }

      return todo;
    }
  }
}
