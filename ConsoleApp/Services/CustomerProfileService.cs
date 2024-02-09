using ConsoleApp.Entities;
using ConsoleApp.Repositories;
namespace ConsoleApp.Services;

public class CustomerProfileService
{
    private readonly CustomerProfileRepository _customerProfileRepository;
    private readonly CustomerRepository _customerRepository;


    public CustomerProfileService(CustomerProfileRepository customerProfileRepository, CustomerRepository customerRepository)
    {
        _customerProfileRepository = customerProfileRepository;
        _customerRepository = customerRepository;
    }


    public async Task<CustomerProfileEntity> CreateCustomerProfileAsync(int customerId, string firstName, string lastName, string phoneNumber)
    {
        var customerExists = await _customerRepository.GetAsync(x => x.Id == customerId) != null;
        if (!customerExists)
        {
            throw new InvalidOperationException("Ingen kund finns med angivet ID.");
        }

        var existingProfile = await _customerProfileRepository.ExistingAsync(x => x.CustomerId == customerId);
        if (existingProfile)
        {
            throw new InvalidOperationException("En profil för denna kund finns redan.");
        }

        var customerProfileEntity = new CustomerProfileEntity
        {
            CustomerId = customerId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber
        };

        await _customerProfileRepository.CreateAsync(customerProfileEntity);
        return customerProfileEntity;
    }


    public async Task<CustomerProfileEntity> GetCustomerProfileByIdAsync(int customerId)
    {
        return await _customerProfileRepository.GetAsync(x => x.CustomerId == customerId);
    }


    public async Task<IEnumerable<CustomerProfileEntity>> GetAllCustomerProfilesAsync()
    {
        return await _customerProfileRepository.GetAsync();
    }


    public async Task<CustomerProfileEntity> UpdateCustomerProfileAsync(int customerId, CustomerProfileEntity updatedCustomerProfile)
    {
        var customerProfileEntity = await _customerProfileRepository.GetAsync(x => x.CustomerId == customerId);
        if (customerProfileEntity == null)
        {
            throw new KeyNotFoundException("Kundprofilen hittades inte med ID: " + customerId);
        }

        customerProfileEntity.FirstName = updatedCustomerProfile.FirstName;
        customerProfileEntity.LastName = updatedCustomerProfile.LastName;
        customerProfileEntity.PhoneNumber = updatedCustomerProfile.PhoneNumber;

        await _customerProfileRepository.UpdateAsync(x => x.CustomerId == customerId, customerProfileEntity);
        return customerProfileEntity;
    }


    public async Task<bool> DeleteCustomerProfileAsync(int customerId)
    {
        var customerProfileEntity = await _customerProfileRepository.GetAsync(x => x.CustomerId == customerId);
        if (customerProfileEntity == null)
        {
            return false;
        }

        await _customerProfileRepository.DeleteAsync(customerProfileEntity);
        return true;
    }
}
