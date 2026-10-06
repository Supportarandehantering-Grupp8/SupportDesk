namespace SupportDesk.Domain.Models;

using System.Net.Mail;
using System.Text.Json.Serialization;

public class Customer
{
    public string CustomerName { get; private set; }  
    public string CustomerEmail { get; private set; }
    [JsonInclude]
    public Guid Id { get; private set; } = Guid.NewGuid();


    public Customer(string customerName, string customerEmail)
    {
        CustomerName = NormalizeCustomerName(customerName);
        CustomerEmail = NormalizeCustomerEmail(customerEmail);
    }



    public void ChangeCustomerName(string customerName)
    {
        CustomerName = NormalizeCustomerName(customerName);
    }

    public void ChangeCustomerEmail(string customerEmail)
    {
        CustomerEmail = NormalizeCustomerEmail(customerEmail);
    }


    public void UpdateCustomer(string customerName, string customerEmail)
    {
        var normalizedName = NormalizeCustomerName(customerName);
        var normalizedEmail = NormalizeCustomerEmail(customerEmail);

        CustomerName = normalizedName;
        CustomerEmail = normalizedEmail;
    }





    private static string NormalizeCustomerName(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name is required.");

        return customerName.Trim();
    }

    private static string NormalizeCustomerEmail(string customerEmail)
    {
        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new ArgumentException("Customer email is required.");

        customerEmail = customerEmail.Trim().ToLower();

        if (!MailAddress.TryCreate(customerEmail, out _))
            throw new ArgumentException("Customer email is invalid.");

        return customerEmail;
    }
}   




