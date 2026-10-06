
using System.Text.Json;
using SupportDesk.Application.Customers;
using SupportDesk.Domain.Models;

namespace SupportDesk.Infrastructure.Customers;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly string _filePath;

    public JsonCustomerRepository(string filePath)
    {
        _filePath = filePath;
    }



    public async Task<List<Customer>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Customer>();
        }

        try
        {
            var json = await File.ReadAllTextAsync(_filePath);

            var customers = JsonSerializer.Deserialize<List<Customer>>(json);

            if (customers == null)
            {
                return new List<Customer>();
            }

            return customers;
        }
        catch (JsonException)
        {
            return new List<Customer>();
        }
    }

    public async Task AddAsync(Customer customer)
    {
        var customers = await GetAllAsync();

        customers.Add(customer);

        var json = JsonSerializer.Serialize(customers);

        await File.WriteAllTextAsync(_filePath, json);
    }




    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var customers = await GetAllAsync();

        foreach (var customer in customers)
        {
            if (customer.Id == id)
            {
                return customer;
            }
        }

        return null;
    }



    public async Task<bool> UpdateAsync(Customer customer)
    {
        var customers = await GetAllAsync();

        for (int i = 0; i < customers.Count; i++)
        {
            if (customers[i].Id == customer.Id)
            {
                customers[i] = customer;

                var json = JsonSerializer.Serialize(customers);

                await File.WriteAllTextAsync(_filePath, json);

                return true;
            }
        }

        return false;
    }

}   