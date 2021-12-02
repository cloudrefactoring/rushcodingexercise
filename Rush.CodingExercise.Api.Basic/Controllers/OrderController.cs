using Microsoft.AspNetCore.Mvc;
using Rush.CodingExercise.Service.Data;
using Rush.CodingExercise.Data.Model.Enum;

using CreateOrderModel = Rush.CodingExercise.Data.Model.DTO.CreateOrder;
using UpdateOrderModel = Rush.CodingExercise.Data.Model.DTO.UpdateOrder;
using OrderModel = Rush.CodingExercise.Data.Model.Order;
using Rush.CodingExercise.Messaging;

namespace Rush.CodingExercise.Api.Basic.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderProcess _orderProcess;
    private readonly ILogger<OrderProcess> _logger;
    private readonly IServiceBus _serviceBus;

    public OrderController(IOrderProcess orderProcess, ILogger<OrderProcess> logger, IServiceBus serviceBus)
    {
        _orderProcess = orderProcess;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderModel>>> Get(string orderNumber)
    {
        var order = await _orderProcess.GetByOrderNumber(orderNumber);
        if (!order.Any<OrderModel>())
        {
            return BadRequest("Order Not Found");
        }
        return Ok(order);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderModel>> Create(CreateOrderModel createOrderModel)
    {
        if (createOrderModel == null)
        {
            return BadRequest("Invalid create order request object.");
        }

        var order = await _orderProcess.Create(createOrderModel.CustomerId);
        await _serviceBus.SendMessage(order);

        _logger.LogInformation("Order Created",order);
        return CreatedAtAction("Create",order);
    }

    [HttpPatch]
    public async Task<ActionResult<OrderModel>> Update(UpdateOrderModel createOrderModel)
    {
        if (createOrderModel == null)
        {
            var errorMessage = "Invalid update order request object.";
            _logger.LogError(errorMessage, createOrderModel);

            return BadRequest(errorMessage);
        }

        var order = await _orderProcess.Update(createOrderModel.OrderNumber,createOrderModel.Total, createOrderModel.Status);
        await _serviceBus.SendMessage(order);

        _logger.LogInformation("Order Updated", order);
        return Ok(order);
    }
}
