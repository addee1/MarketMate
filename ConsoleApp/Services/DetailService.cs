using ConsoleApp.Entities;
using ConsoleApp.Repositories;
namespace ConsoleApp.Services;

public class DetailService
{
    private readonly DetailRepository _detailRepository;
    private readonly ProductRepository _productRepository;
    private readonly OrderRepository _orderRepository;

    public DetailService(DetailRepository detailRepository, ProductRepository productRepository, OrderRepository orderRepository)
    {
        _detailRepository = detailRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<DetailEntity> CreateDetailAsync(int orderId, int productId, int quantity, decimal subtotal)
    {
        // Kontrollera att både Order och Product existerar
        var orderExists = await _orderRepository.GetAsync(x => x.Id == orderId) != null;
        var productExists = await _productRepository.GetAsync(x => x.Id == productId) != null;

        if (!orderExists || !productExists)
        {
            throw new InvalidOperationException("Order eller Produkt existerar inte.");
        }

        // Kontrollera först om en liknande detalj redan finns
        var existingDetail = await _detailRepository.GetAsync(x => x.OrderId == orderId && x.ProductId == productId);

        if (existingDetail == null)
        {
            var detailEntity = new DetailEntity
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Subtotal = subtotal
            };

            await _detailRepository.CreateAsync(detailEntity);
            return detailEntity;
        }

        return existingDetail;
    }

    public async Task<DetailEntity> GetDetailByIdAsync(int detailId)
    {
        return await _detailRepository.GetAsync(x => x.Id == detailId);
    }

    public async Task<IEnumerable<DetailEntity>> GetAllDetailsAsync()
    {
        return await _detailRepository.GetAsync();
    }

    public async Task<DetailEntity> UpdateDetailAsync(int detailId, int quantity, decimal subtotal)
    {
        var detailEntity = await _detailRepository.GetAsync(x => x.Id == detailId);
        if (detailEntity == null)
        {
            throw new KeyNotFoundException("Detalj hittas inte med ID: " + detailId);
        }

        detailEntity.Quantity = quantity;
        detailEntity.Subtotal = subtotal;

        await _detailRepository.UpdateAsync(x => x.Id == detailId, detailEntity);
        return detailEntity;
    }

    public async Task<bool> DeleteDetailAsync(int detailId)
    {
        var detailEntity = await _detailRepository.GetAsync(x => x.Id == detailId);
        if (detailEntity == null)
        {
            return false;
        }

        await _detailRepository.DeleteAsync(detailEntity);
        return true;
    }
}
