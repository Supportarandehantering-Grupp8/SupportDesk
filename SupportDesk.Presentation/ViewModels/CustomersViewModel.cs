using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Customers;
using SupportDesk.Domain.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

    [ObservableProperty]
    private string _errorMessage = string.Empty;


    partial void OnSelectedCustomerChanged(Customer? value)
    {
        if (value is null)
            return;

        CustomerName = value.CustomerName;
        CustomerEmail = value.CustomerEmail;
        ErrorMessage = string.Empty;
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
        ErrorMessage = string.Empty;
    }


    [RelayCommand]
    public async Task CreateCustomerAsync()
    {
        try
        {
            ErrorMessage = string.Empty;

            await _customerService.CreateAsync(CustomerName, CustomerEmail);

            await LoadCustomersAsync();

            ResetForm();
        }
        catch (ArgumentException ex)
        {
            ErrorMessage = ex.Message;
        }
    }


    [RelayCommand]
    public async Task UpdateCustomerAsync()
    {
        if (SelectedCustomer is null)
            return;

        try
        {
            ErrorMessage = string.Empty;

            await _customerService.UpdateAsync(
                SelectedCustomer.Id,
                CustomerName,
                CustomerEmail);

            await LoadCustomersAsync();

            ResetForm();
        }
        catch (ArgumentException ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}