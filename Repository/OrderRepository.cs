using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace OrderManagementSystem.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<OrderInfo>> GetAllOrdersAsync()
        {
            var orders = new List<OrderInfo>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM Orders", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new OrderInfo
                        {
                            OrderID = reader.GetInt32("OrderID"),
                            Vendor = reader.GetString("Vendor"),
                            OrderAmount = reader.GetDecimal("OrderAmount"),
                            OrderNumber = reader.GetInt32("OrderNumber"),
                            Shop = reader.GetString("Shop"),
                            OrderDate=reader.GetDateTime("OrderDate")
                        });
                    }
                }
            }

            return orders;
        }

        public async Task<OrderInfo> GetOrderByIdAsync(int orderId)
        {
            OrderInfo order = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM Orders WHERE OrderID = @OrderID", connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            order = new OrderInfo
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Vendor = reader.GetString("Vendor"),
                                OrderAmount = reader.GetDecimal("OrderAmount"),
                                OrderNumber = reader.GetInt32("OrderNumber"),
                                Shop = reader.GetString("Shop"),
                                OrderDate = reader.GetDateTime("OrderDate")
                            };
                        }
                    }
                }
            }

            return order;
        }

        public async Task CreateOrderAsync(OrderInfo order)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("INSERT INTO Orders (Vendor, OrderAmount, OrderNumber, Shop,OrderDate) VALUES (@Vendor, @OrderAmount, @OrderNumber, @Shop,@OrderDate)", connection))
                {
                    command.Parameters.AddWithValue("@Vendor", order.Vendor);
                    command.Parameters.AddWithValue("@OrderAmount", order.OrderAmount);
                    command.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    command.Parameters.AddWithValue("@Shop", order.Shop);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateOrderAsync(OrderInfo order)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("UPDATE Orders SET Vendor = @Vendor, OrderAmount = @OrderAmount, OrderNumber = @OrderNumber, Shop = @Shop WHERE OrderID = @OrderID", connection))
                {
                    command.Parameters.AddWithValue("@OrderID", order.OrderID);
                    command.Parameters.AddWithValue("@Vendor", order.Vendor);
                    command.Parameters.AddWithValue("@OrderAmount", order.OrderAmount);
                    command.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    command.Parameters.AddWithValue("@Shop", order.Shop);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("DELETE FROM Orders WHERE OrderID = @OrderID", connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
