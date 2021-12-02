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
using Rush.CodingExercise.Data.Model.Enum;
using System;
using Rush.CodingExercise.Messaging;

namespace Rush.CodingExercise.Api.Function
{
    public class UpdateOrderFunction
    {
        private readonly ILogger<UpdateOrderFunction> _logger;
        private readonly IOrderProcess _orderProcess;
        private readonly IServiceBus _serviceBus;

        public UpdateOrderFunction(ILogger<UpdateOrderFunction> log, IOrderProcess orderProcess, IServiceBus serviceBus)
        {
            _logger = log;
            _orderProcess = orderProcess;
            _serviceBus = serviceBus;
        }

        [FunctionName("UpdateOrder")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateOrderModel), Description = "Update Order Payload", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Updating order from Azure Function.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateOrderModel = JsonConvert.DeserializeObject<UpdateOrderModel>(requestBody);

            var order = await _orderProcess.Update(updateOrderModel.OrderNumber, updateOrderModel.Total, updateOrderModel.Status);
            await _serviceBus.SendMessage(order);

            _logger.LogInformation("Order Updated from Azure Function", order);
            return new OkObjectResult(order);

        }
    }
}

