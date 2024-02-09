using ConsoleApp.Entities;
using ConsoleApp.Repositories;
namespace ConsoleApp.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly CustomerRepository _customerRepository;

    public OrderService(OrderRepository orderRepository, CustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<OrderEntity> CreateOrderAsync(int customerId, decimal totalPrice, DateTime orderDate)
    {
        // Kontrollera att en CustomerEntity med customerId existerar
        var customerExists = await _customerRepository.GetAsync(x => x.Id == customerId) != null;
        if (!customerExists)
        {
            throw new InvalidOperationException("Ingen kund finns med angivet ID.");
        }

        var orderEntity = new OrderEntity
        {
            CustomerId = customerId,
            TotalPrice = totalPrice,
            OrderDate = orderDate
        };

        await _orderRepository.CreateAsync(orderEntity);
        return orderEntity;
    }

    public async Task<OrderEntity> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetAsync(x => x.Id == orderId);
    }

    public async Task<IEnumerable<OrderEntity>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAsync();
    }

    public async Task<OrderEntity> UpdateOrderAsync(int orderId, decimal totalPrice, DateTime orderDate)
    {
        var orderEntity = await _orderRepository.GetAsync(x => x.Id == orderId);
        if (orderEntity == null)
        {
            throw new KeyNotFoundException("Ordern hittades inte med ID: " + orderId);
        }

        orderEntity.TotalPrice = totalPrice;
        orderEntity.OrderDate = orderDate;

        await _orderRepository.UpdateAsync(x => x.Id == orderId, orderEntity);
        return orderEntity;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var orderEntity = await _orderRepository.GetAsync(x => x.Id == orderId);
        if (orderEntity == null)
        {
            return false;
        }

        await _orderRepository.DeleteAsync(orderEntity);
        return true;
    }
}
