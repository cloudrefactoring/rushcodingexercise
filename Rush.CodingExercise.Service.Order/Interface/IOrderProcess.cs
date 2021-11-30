using Rush.CodingExercise.Data.Model.Enum;
using OrderModel = Rush.CodingExercise.Data.Model.Order;

namespace Rush.CodingExercise.Service.Data;

public interface IOrderProcess
{
    Task<OrderModel> Create(int customerId);
    Task<IEnumerable<OrderModel>> GetByCustomer(int customerId);
    Task<IEnumerable<OrderModel>> GetByCustomer(int customerId, DateTime dateFrom, DateTime dateTo);
    Task<IEnumerable<OrderModel>> GetByCustomer(int customerId, OrderStatus orderStatus);
    Task<IEnumerable<OrderModel>> GetByOrderNumber(string orderNumber);
    Task<OrderModel> Update(string orderNumber, decimal orderTotal, string orderStatusName);
    Task<IEnumerable<OrderModel>> UpdateStatus(int orderId, OrderStatus orderStatus);
    Task<IEnumerable<OrderModel>> UpdateTotal(int orderId, OrderStatus orderStatus);
}