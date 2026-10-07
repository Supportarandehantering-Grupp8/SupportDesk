using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Customers;
using SupportDesk.Domain.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace SupportDesk.Presentation.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;  // Viewmodel kan prata med CustomerService

    [ObservableProperty]  // OBS! (binding) Håller i Värdet(_customerName åt UI) (Använder _customerName) Rad43
    private string _customerName = string.Empty; // Lagrar namnet som användaren skriver i UI:t

    [ObservableProperty] // OBS! Håller i Värdet(_customerEmail åt UI)  (Använder _customerEmail) Rad43
    private string _customerEmail = string.Empty; //Användaren skriver

    [ObservableProperty]
    private Customer? _selectedCustomer;  //Använderen väljer



    public ObservableCollection<Customer> Customers { get; } = new(); // Customers = Kunder som visas
    // ObservableProperty gör så att UI märker om något läggs till eller tas bort från Listan.
    
    
    public CustomersViewModel(ICustomerService customerService) //ge mig något som följer Interfacet, i detta fall CustomerService.
    {
        _customerService = customerService;
    }

    public async Task LoadCustomersAsync()  //Hämtar Kunder
    {
        var customers = await _customerService.GetAllAsync();

        Customers.Clear();   // först töms listan som visas, 

        foreach (var customer in customers)  // går igenom kunderna jag hämtade
        {
            Customers.Add(customer); // lägger in dom en efter en
        }
    }

    [RelayCommand]
    public async Task CreateCustomerAsync()  //Registrerar kund
    {
        await _customerService.CreateAsync(CustomerName, CustomerEmail); // Kunden som registreras går till CustomerService och fortsätter ned till json
        await LoadCustomersAsync();  // laddar om uppdaterade listan på skärmen
    }

    [RelayCommand]
    public async Task UpdateCustomerAsync()  //Uppdaterar kund
    {
        if (SelectedCustomer is null)
            return;

        await _customerService.UpdateAsync(SelectedCustomer.Id,CustomerName,CustomerEmail);

        await LoadCustomersAsync();
    }
}