using ServiceStack;

namespace TodoApi.ServiceModel
{
  [Route("/api/todos/{Id}", "DELETE")]
  public class DeleteTodo : IReturnVoid
  {
    public int Id { get; set; }
  }
}
