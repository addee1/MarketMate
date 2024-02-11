using ConsoleApp.Entities;
using ConsoleApp.Repositories;
namespace ConsoleApp.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }


    public async Task<CustomerEntity> CreateCustomerAsync(string email, string password) // Metod som skapar en Customer OM epostaddressen inte redan finns
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Email == email);

        if (customerEntity != null)
        {
            return null;
        }

        var newCustomer = new CustomerEntity { Email = email, Password = password };
        await _customerRepository.CreateAsync(newCustomer);

        return newCustomer;
    }

    public async Task<CustomerEntity> GetCustomerByIdAsync(int customerId) // Metod som hämtar en Customer baserat på dens Id
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == customerId);
        return customerEntity;
    }


    public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
    {
        return await _customerRepository.GetAsync(x => x.Email == email);
    }


    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync() // Metod som hämtar ALLA Customers
    {
        var customers = await _customerRepository.GetAsync();
        return customers;
    }


    public async Task<CustomerEntity> UpdateCustomerAsync(int customerId, CustomerEntity updatedCustomer)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == customerId);
        if (customerEntity == null)
        {
            // Om inte kunden med det angivna Id finns gör detta...
            throw new KeyNotFoundException("Kunden hittades inte med ID: " + customerId);
        }

        // Uppdaterar lösenordet.
        customerEntity.Password = updatedCustomer.Password;


        await _customerRepository.UpdateAsync(x => x.Id == customerId, customerEntity);

        return customerEntity;
    }


    public async Task<bool> DeleteCustomerAsync(int customerId) // Metod som raderar en Customer baserat på Id
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == customerId);
        if (customerEntity == null)
        {
            return false;
        }

        await _customerRepository.DeleteAsync(customerEntity);
        return true;
    }

}
