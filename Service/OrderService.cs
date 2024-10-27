using Mysqlx.Crud;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderInfo>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<OrderInfo> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task CreateOrderAsync(OrderInfo order)
        {
            await _orderRepository.CreateOrderAsync(order);
        }

        public async Task UpdateOrderAsync(OrderInfo order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }
        public async Task DeleteMultipleOrdersAsync(List<int> orderIDs)
        {
            foreach (var orderId in orderIDs)
            {
                
                await _orderRepository.DeleteOrderAsync(orderId);
                
            }
        }
    }
}
