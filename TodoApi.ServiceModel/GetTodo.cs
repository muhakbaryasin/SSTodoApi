using TodoApi.ServiceModel.Types;
using System.Collections.Generic;
using ServiceStack;

namespace TodoApi.ServiceModel
{
  [Route("/api/todos/{Id}", "GET")]
  public class GetTodo : IReturn<Todo>
  {
    public int Id { get; set; }
  }

  [Route("/api/todos", "GET")]
  public class GetTodos : IReturn<List<Todo>>
  {
  }
}
