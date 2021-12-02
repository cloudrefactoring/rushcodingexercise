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

using CreateOrderModel = Rush.CodingExercise.Data.Model.DTO.CreateOrder;


namespace Rush.CodingExercise.Api.Function
{
    public class CreateOrderFunction
    {
        private readonly ILogger<CreateOrderFunction> _logger;
        private readonly IOrderProcess _orderProcess;
        private readonly IServiceBus _serviceBus;

        public CreateOrderFunction(ILogger<CreateOrderFunction> log, IOrderProcess orderProcess, IServiceBus serviceBus)
        {
            _logger = log;
            _orderProcess = orderProcess;
            _serviceBus = serviceBus;
        }

        [FunctionName("CreateOrder")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateOrderModel), Description = "Create Order Payload", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Creating order from Azure Function.");

            if (req == null)
            {
                _logger.LogInformation("Error creating order from Azure Function. Invalid request object");
                return new BadRequestObjectResult("Invalid create order request object.");
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createOrderModel = JsonConvert.DeserializeObject<CreateOrderModel>(requestBody);

            var order = await _orderProcess.Create(createOrderModel.CustomerId);
            await _serviceBus.SendMessage(order);

            _logger.LogInformation("Order Created from Azure Function", order);
            return new OkObjectResult(order);
        }
    }
}