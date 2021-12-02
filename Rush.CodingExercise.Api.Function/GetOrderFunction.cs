using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rush.CodingExercise.Service.Data;
using OrderModel = Rush.CodingExercise.Data.Model.Order;


namespace Rush.CodingExercise.Api.Function
{
    public class GetOrderFunction
    {
        private readonly ILogger<GetOrderFunction> _logger;
        private readonly IOrderProcess _orderProcess;

        public GetOrderFunction(ILogger<GetOrderFunction> log, IOrderProcess orderProcess)
        {
            _logger = log;
            _orderProcess = orderProcess;
        }

        [FunctionName("GetOrderFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **OrderNumber** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            string orderNumber = req.Query["orderNumber"];

            var order = await _orderProcess.GetByOrderNumber(orderNumber);
            if (!order.Any<OrderModel>())
            {
                return new BadRequestObjectResult("Order Not Found");
            }
            return new OkObjectResult(order);
        }
    }
}