using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Customers;
using SupportDesk.Domain.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace SupportDesk.Presentation.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _customerService; 

    [ObservableProperty] 
    private string _customerName = string.Empty; 

    [ObservableProperty] 
    private string _customerEmail = string.Empty; 

    [ObservableProperty]
    private Customer? _selectedCustomer;

    partial void OnSelectedCustomerChanged(Customer? value)
    {
        if (value is null)
            return;

        CustomerName = value.CustomerName;
        CustomerEmail = value.CustomerEmail;
    }



    public ObservableCollection<Customer> Customers { get; } = new();
    
    
    
    public CustomersViewModel(ICustomerService customerService) 
    {
        _customerService = customerService;
    }

    public async Task LoadCustomersAsync() 
    {
        var customers = await _customerService.GetAllAsync();

        Customers.Clear();  

        foreach (var customer in customers)
        {
            Customers.Add(customer); 
        }
    }

    private void ResetForm()
    {
        SelectedCustomer = null;
        CustomerName = string.Empty;
        CustomerEmail = string.Empty;
    }

    [RelayCommand]
    public async Task CreateCustomerAsync() 
    {
        await _customerService.CreateAsync(CustomerName, CustomerEmail);
        await LoadCustomersAsync(); 

        ResetForm();
    }

    [RelayCommand]
    public async Task UpdateCustomerAsync() 
    {
        if (SelectedCustomer is null)
            return;

        await _customerService.UpdateAsync(SelectedCustomer.Id,CustomerName,CustomerEmail);

        await LoadCustomersAsync();

        ResetForm();
    }
}