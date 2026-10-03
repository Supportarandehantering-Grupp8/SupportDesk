
using SupportDesk.Application.Models;

namespace SupportDesk.Application.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;




    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository; 

    }



    public async Task<Customer> CreateAsync(string customerName, string customerEmail)
    {
        var customer = new Customer(customerName, customerEmail);

        await _customerRepository.AddAsync(customer);

        return customer;
    }
    
    
    public async Task<List<Customer>> GetAllAsync()
    {
        return await _customerRepository.GetAllAsync();
    }
    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    
    
    public async Task<bool> UpdateAsync(Guid id, string customerName, string customerEmail)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if(customer ==null)
        {
            return false;

        }

        customer.UpdateCustomer(customerName, customerEmail);



        return await _customerRepository.UpdateAsync(customer);
    }
}

