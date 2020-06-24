using System;

namespace TodoApi.ServiceModel.Types
{
  public class Todo
  {
    [ServiceStack.DataAnnotations.AutoIncrement]
    public int Id { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CompletePercentage { get; set; }
  }
}
