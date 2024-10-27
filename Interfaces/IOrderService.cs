using Mysqlx.Crud;
using OrderManagementSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderInfo>> GetAllOrdersAsync();
        Task<OrderInfo> GetOrderByIdAsync(int orderId);
        Task CreateOrderAsync(OrderInfo order);
        Task UpdateOrderAsync(OrderInfo order);
        Task DeleteOrderAsync(int orderId);
        Task DeleteMultipleOrdersAsync(List<int> orderIDs);
    }
}
