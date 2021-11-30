using System.IO;
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
using Newtonsoft.Json;
using Rush.CodingExercise.Service.Data;

using UpdateOrderModel = Rush.CodingExercise.Data.Model.DTO.UpdateOrder;
using OrderModel = Rush.CodingExercise.Data.Model.Order;
using Rush.CodingExercise.Data.Model.Enum;
using System;

namespace Rush.CodingExercise.Api.Function
{
    public class UpdateOrderFunction
    {
        private readonly ILogger<UpdateOrderFunction> _logger;
        private readonly IOrderProcess _orderProcess;

        public UpdateOrderFunction(ILogger<UpdateOrderFunction> log, IOrderProcess orderProcess)
        {
            _logger = log;
            _orderProcess = orderProcess;
        }

        [FunctionName("UpdateOrder")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Creating order from Azure Function.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateOrderModel = JsonConvert.DeserializeObject<UpdateOrderModel>(requestBody);

            if (updateOrderModel == null)
            {
                var errorMessage = "Invalid update order request object.";
                _logger.LogError(errorMessage, updateOrderModel);

                return new BadRequestObjectResult(errorMessage);
            }

            if (updateOrderModel.OrderNumber == null)
            {
                var errorMessage = "Order number is required.";
                _logger.LogError(errorMessage, updateOrderModel);

                return new BadRequestObjectResult(errorMessage);
            }

            if (updateOrderModel.Status == null)
            {
                var errorMessage = "Status is required";
                _logger.LogError(errorMessage, updateOrderModel);

                return new BadRequestObjectResult(errorMessage);
            }

            if (!Enum.IsDefined(typeof(OrderStatus), updateOrderModel.Status))
            {
                var errorMessage = "Invalid Status Value. Acceptable values are: Pending,Ordered,Billed,Shipped,Delivered";
                _logger.LogError(errorMessage, updateOrderModel);

                return new BadRequestObjectResult(errorMessage);
            }

            var order = await _orderProcess.Update(updateOrderModel.OrderNumber, updateOrderModel.Total, updateOrderModel.Status);
            //await _serviceBus.SendMessage(order);

            _logger.LogInformation("Order Updated from Azure Function", order);
            return new OkObjectResult(order);

        }
    }
}

