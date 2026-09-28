using SupportDesk.Domain.Models;
namespace SupportDesk.Application.Customers;

public interface ICustomerRepository
{
    void AddCustomer(Customer customer);
    List<Customer> GetAllCustomers();
    Customer? FindCustomer(string name);
    bool RemoveCustomer(Customer customer);
}
