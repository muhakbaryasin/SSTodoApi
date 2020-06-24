using System;
using TodoApi.ServiceModel.Types;
using ServiceStack;

namespace TodoApi.ServiceModel
{
  [Route("/api/todos", "POST")]
  [Route("/api/todos/{Id}", "PUT")]
  public class StoreTodo : IReturn<Todo>
  {
    public int Id { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CompletePercentage { get; set; }
  }
}
