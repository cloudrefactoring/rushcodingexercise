using Rush.CodingExercise.Data.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateOrderModel = Rush.CodingExercise.Data.Model.DTO.UpdateOrder;

namespace Rush.CodingExercise.Service.Order;

internal static class OrderRequestValidation
{

    static OrderRequestValidation()
    {

    }

    //internal static (bool,int,string) ValidateUpdateOrder(UpdateOrderModel updateOrderModel)
    //{

    //    //if (updateOrderModel == null)
    //    //{
    //    //    var errorMessage = "Invalid update order request object.";
    //    //    _logger.LogError(errorMessage, updateOrderModel);

    //    //    return new BadRequestObjectResult(errorMessage);
    //    //}

    //    //if (updateOrderModel.OrderNumber == null)
    //    //{
    //    //    var errorMessage = "Order number is required.";
    //    //    _logger.LogError(errorMessage, updateOrderModel);

    //    //    return new BadRequestObjectResult(errorMessage);
    //    //}

    //    //if (updateOrderModel.Status == null)
    //    //{
    //    //    var errorMessage = "Status is required";
    //    //    _logger.LogError(errorMessage, updateOrderModel);

    //    //    return new BadRequestObjectResult(errorMessage);
    //    //}

    //    //if (!Enum.IsDefined(typeof(OrderStatus), updateOrderModel.Status))
    //    //{
    //    //    var errorMessage = "Invalid Status Value. Acceptable values are: Pending,Ordered,Billed,Shipped,Delivered";
    //    //    _logger.LogError(errorMessage, updateOrderModel);

    //    //    return new BadRequestObjectResult(errorMessage);
    //    //}

    //    //return "";
    //}
}


