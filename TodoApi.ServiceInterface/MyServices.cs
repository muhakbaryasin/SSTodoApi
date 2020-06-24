using System;
using ServiceStack;
using TodoApi.ServiceModel;

namespace TodoApi.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }

        /*
        public object Any(Todo request)
        {
          return new Todo { Id = request.Id };
        }
        */
  }
}
