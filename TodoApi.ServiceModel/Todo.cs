﻿using System;
using System.Collections.Generic;
using ServiceStack;

namespace TodoApi.ServiceModel
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

  [Route("/api/todos/{Id}", "DELETE")]
  public class DeleteTodo : IReturnVoid
  {
    public int Id { get; set; }
  }

  [Route("/api/todos/{Id}", "GET")]
  public class GetTodo : IReturn<Todo>
  {
    public int Id { get; set; }
  }

  [Route("/api/todos", "GET")]
  public class GetTodos : IReturn<List<Todo>>
  {
  }

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

  [Route("/api/todos/complete/{Id}/value/{CompletePercentage}", "PUT")]
  public class SetCompletenessPercentageTodo : IReturn<Todo>
  {
    public int Id { get; set; }
    public int CompletePercentage { get; set; }
  }

  [Route("/api/todos/complete/{Id}", "PUT")]
  public class SetDoneTodo : IReturn<Todo>
  {
    public int Id { get; set; }
  }

  [Route("/api/todos/getbydate/{DateRangeType}", "GET")]
  public class GetByDateTodo : IReturn<List<Todo>>
  {
    public DateRangeEnum DateRangeType { get; set; }
  }

  public enum DateRangeEnum
  {
    TODAY,
    TOMORROW,
    THISWEEK
  }
}
