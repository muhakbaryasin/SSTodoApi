using TodoApi.ServiceModel.Types;
using ServiceStack;

namespace TodoApi.ServiceModel
{
  [Route("/api/todos/complete/{Id}", "PUT")]
  public class SetDoneTodo : IReturn<Todo>
  {
    public int Id { get; set; }
  }
}
