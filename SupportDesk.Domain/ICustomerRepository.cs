using SupportDesk.Domain.Models;

namespace SupportDesk.Domain;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task<bool> UpdateAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
}
