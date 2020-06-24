using TodoApi.ServiceModel.Types;
using ServiceStack;

namespace TodoApi.ServiceModel
{
  [Route("/api/todos/complete/{Id}/value/{CompletePercentage}", "PUT")]
  public class SetCompletenessPercentageTodo : IReturn<Todo>
  {
    public int Id { get; set; }
    public int CompletePercentage { get; set; }
  }
}
