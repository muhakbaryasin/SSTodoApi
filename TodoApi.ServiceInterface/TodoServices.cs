using TodoApi.ServiceModel;
using ServiceStack;
using ServiceStack.OrmLite;

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
  }
}
