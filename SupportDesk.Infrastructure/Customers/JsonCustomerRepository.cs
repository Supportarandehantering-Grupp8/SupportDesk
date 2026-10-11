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
            return new List<Customer>();

        try
        {
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Customer>>(json)
                   ?? new List<Customer>();
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

        await SaveCustomersAsync(customers);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var customers = await GetAllAsync();
        return customers.FirstOrDefault(c => c.Id == id);
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        var customers = await GetAllAsync();

        var index = customers.FindIndex(c => c.Id == customer.Id);

        if (index == -1)
            return false;

        customers[index] = customer;

        await SaveCustomersAsync(customers);
        return true;
    }

    private async Task SaveCustomersAsync(List<Customer> customers)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        var json = JsonSerializer.Serialize(customers);
        await File.WriteAllTextAsync(_filePath, json);
    }
}