using Rush.CodingExercise.Data.Model.Enum;
using Rush.CodingExercise.Data;

using OrderModel = Rush.CodingExercise.Data.Model.Order;

namespace Rush.CodingExercise.Service.Data;

public class OrderProcess : IOrderProcess
{
    private readonly ISQLDataAdapter _sqlDataConnector;
    private readonly IOrderNumber _orderNumber;

    public OrderProcess(ISQLDataAdapter sqlDataConnector, IOrderNumber orderNumber)
    {
        _sqlDataConnector = sqlDataConnector;
        _orderNumber = orderNumber;
    }

    public async Task<IEnumerable<OrderModel>> GetByCustomer(int customerId, DateTime dateFrom, DateTime dateTo)
    {
        var queryParameters = new
        {
            customerId,
            dateFrom,
            dateTo
        };

        var data = await _sqlDataConnector.Query<OrderModel, dynamic>("dbo.spOrder_GetByCustomer", queryParameters);
        return data;
    }

    public async Task<IEnumerable<OrderModel>> GetByCustomer(int customerId)
    {
        var queryParameters = new
        {
            customerId
        };

        var data = await _sqlDataConnector.Query<OrderModel, dynamic>("dbo.spOrder_GetByCustomerAll", queryParameters);
        return data;
    }
    public async Task<IEnumerable<OrderModel>> GetByCustomer(int customerId, OrderStatus orderStatus)
    {
        var orderStatusName = Enum.GetName(typeof(OrderStatus), orderStatus);
        var queryParameters = new
        {
            customerId,
            orderStatusName
        };

        var data = await _sqlDataConnector.Query<OrderModel, dynamic>("dbo.spOrder_GetByCustomerAndOrderStatus", queryParameters);
        return data;
    }

    public async Task<IEnumerable<OrderModel>> GetByOrderNumber(string orderNumber)
    {
        var queryParameters = new
        {
            orderNumber
        };

        var data = await _sqlDataConnector.Query<OrderModel, dynamic>("dbo.spOrder_GetByOrderNumber", queryParameters);
        return data;
    }

    public async Task<OrderModel> Create(int customerId)
    {

        var status = Enum.GetName(typeof(OrderStatus), OrderStatus.Pending);
        var orderNumber = _orderNumber.GetNext();

        var insertParameters = new
        {
            customerId,
            orderNumber,
            status
        };

        var data = await _sqlDataConnector.Create<OrderModel, dynamic>("dbo.spOrder_Create", insertParameters);
        return data;
    }

    public async Task<OrderModel> Update(string orderNumber, decimal total, string status)
    {
        var queryParameters = new
        {
            orderNumber,
            total,
            status
        };

        var data = await _sqlDataConnector.Update<OrderModel, dynamic>("dbo.spOrder_Update", queryParameters);
        return data;
    }

    public async Task<IEnumerable<OrderModel>> UpdateStatus(int orderId, OrderStatus orderStatus)
    {
        var orderStatusName = Enum.GetName(typeof(OrderStatus), OrderStatus.Ordered);

        var queryParameters = new
        {
            orderId,
            orderStatusName
        };

        var data = await _sqlDataConnector.Query<OrderModel, dynamic>("dbo.spOrder_UpdateStatus", queryParameters);
        return data;
    }

    public async Task<IEnumerable<OrderModel>> UpdateTotal(int orderId, OrderStatus orderStatus)
    {
        var orderStatusName = Enum.GetName(typeof(OrderStatus), OrderStatus.Ordered);

        var queryParameters = new
        {
            orderId,
            orderStatusName
        };

        var data = await _sqlDataConnector.Query<OrderModel, dynamic>("dbo.spOrder_UpdateStatus", queryParameters);
        return data;
    }
}