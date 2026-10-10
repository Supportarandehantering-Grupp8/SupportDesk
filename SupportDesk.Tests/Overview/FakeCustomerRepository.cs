using SupportDesk.Application.Customers;
using SupportDesk.Domain.Models;

namespace SupportDesk.Tests.Overview;

internal class FakeCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new();

    public Task<List<Customer>> GetAllAsync()
    {
        return Task.FromResult(_customers);
    }

    public Task AddAsync(Customer customer)
    {
        _customers.Add(customer);
        return Task.CompletedTask;
    }

    public Task<bool> UpdateAsync(Customer customer)
    {
        var index = _customers.FindIndex(
            existingCustomer => existingCustomer.Id == customer.Id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        _customers[index] = customer;
        return Task.FromResult(true);
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        var customer = _customers.FirstOrDefault(
            customer => customer.Id == id);

        return Task.FromResult(customer);
    }
}
