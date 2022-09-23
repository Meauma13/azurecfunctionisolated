using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace My.Function
{
    public class HttpExample
    {
        private readonly ILogger _logger;

        public HttpExample(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpExample>();
        }

        // [Function("HttpExample")]
        // public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        // {
        //     _logger.LogInformation("C# HTTP trigger function processed a request.");

        //     var response = req.CreateResponse(HttpStatusCode.OK);
        //     response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        //     response.WriteString("Welcome to Azure Functions!");

        //     return response;
        // }

        [Function("HttpExample")]
        public static MultiResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpExample");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var message = "Welcome to Azure Functions!";

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(message);

            // Return a response to both HTTP trigger and Azure Cosmos DB output binding.
            return new MultiResponse()
            {
                Document = new MyDocument
                {
                    id = System.Guid.NewGuid().ToString(),
                    message = message
                },
                HttpResponse = response
            };
        }
    }

    public class MultiResponse
    {
        [CosmosDBOutput("my-database", "my-container",
            ConnectionStringSetting = "CosmosDBCoreSQLAPIConnectionString", CreateIfNotExists = true)]
        public MyDocument Document { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }

    public class MyDocument {
        public string id { get; set; }
        public string message { get; set; }
    }
}
