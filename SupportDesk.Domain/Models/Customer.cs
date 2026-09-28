
namespace SupportDesk.Domain.Models;

public class Customer
{
    public string CustomerName { get; private set; }
    public string CustomerEmail { get; private set; }
    public Guid Id { get; private set; } = Guid.NewGuid();


    public Customer(string customerName, string customerEmail)
    {
        CustomerName = customerName;
        CustomerEmail = customerEmail;
    }
}


