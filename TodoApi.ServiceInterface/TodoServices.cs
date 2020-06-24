using TodoApi.ServiceModel;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;
using System;
using TodoApi.ServiceModel.Types;

namespace TodoApi.ServiceInterface
{
  public class TodoServices : Service, IGet<GetTodos>, IGet<GetTodo>, IPost<StoreTodo>, IPut<StoreTodo>, IDeleteVoid<DeleteTodo>
  {
    private readonly TodoRepository _todoRepository;
    /*
    public TodoServices(TodoRepository todoRepository)
    {
      _todoRepository = todoRepository;
    }
    */

    public TodoServices()
    {
      var connectionString = "Data Source=localhost,1433;Initial Catalog=TestTS;User ID=sa;Password=123abcMetnah;";
      var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServer2017Dialect.Provider);
      
      _todoRepository = new TodoRepository(dbFactory);
    }

    private TodoRepository Repository
    {
      get
      {
        return this._todoRepository;
      }
    }

    public void Delete(DeleteTodo request)
    {
      Repository.Delete(request);
    }

    public object Get(GetTodo request)
    {
      var todo = Repository.Read(request);

      if (todo == null)
        return HttpError.NotFound("The requested todo instance cannot be found");

      return todo;
    }

    public object Get(GetTodos request)
    {
      return Repository.Read();
    }

    public object Post(StoreTodo request)
    {
      return Repository.Store(request);
    }

    public object Put(StoreTodo request)
    {
      return Repository.Store(request);
    }

    public object Put(SetCompletenessPercentageTodo request)
    {
      var getTodo = new GetTodo { Id = request.Id };
      var entry = Repository.Read( getTodo );
      entry.CompletePercentage = request.CompletePercentage;

      return Repository.Store( entry.ConvertTo<StoreTodo>() );
    }

    public object Put(SetDoneTodo request)
    {
      var getTodo = new GetTodo { Id = request.Id };
      var entry = Repository.Read(getTodo);
      entry.CompletePercentage = 100;

      return Repository.Store(entry.ConvertTo<StoreTodo>());
    }

    public object Get(GetByDateTodo request)
    {
      if (request.DateRangeType == DateRangeEnum.TODAY)
        return Repository.Read().Where(a => a.ExpiryDate.Date == DateTime.Now.Date );

      else if (request.DateRangeType == DateRangeEnum.TOMORROW)
        return Repository.Read().Where(a => a.ExpiryDate.Date == DateTime.Now.AddDays(1).Date );

      else if (request.DateRangeType == DateRangeEnum.THISWEEK)
      {
        var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        var saturday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday);
        
        return Repository.Read().Where(a => a.ExpiryDate.Date >= sunday.Date && a.ExpiryDate.Date <= saturday.Date);
      }

      return HttpError.NotFound( string.Format("DateRangeType {0} is not registered. Use TODAY/TOMORROW/THISWEEK instead", request.DateRangeType.ToString()) );
    }
  }
}
