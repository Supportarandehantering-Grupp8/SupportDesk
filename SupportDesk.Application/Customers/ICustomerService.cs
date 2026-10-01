using SupportDesk.Domain.Models;

namespace SupportDesk.Application.Customers;

public interface ICustomerService
{
    Task<Customer> CreateAsync(string customerName, string customerEmail);
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, string customerName, string customerEmail);
}