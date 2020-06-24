using System.Collections.Generic;
using ServiceStack;
using TodoApi.ServiceModel.Types;

namespace TodoApi.ServiceModel
{
  [Route("/api/todos/getbydate/{DateRangeType}", "GET")]
  public class GetByDateTodo : IReturn<List<Todo>>
  {
    public DateRangeEnum DateRangeType { get; set; }
  }
}
