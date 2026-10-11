using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.Pages;

public sealed partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void ResetTestDataButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "Återställ testdata",
            Content = "Vill du radera alla kunder och ärenden? Detta går inte att ångra.",
            PrimaryButtonText = "Återställ",
            CloseButtonText = "Avbryt",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = XamlRoot
        };

        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary)
            return;

        try
        {
            var localAppData = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);

            var supportDeskFolder = Path.Combine(localAppData, "SupportDesk");

            var ticketsPath = Path.Combine(supportDeskFolder, "tickets.json");
            var customersPath = Path.Combine(supportDeskFolder, "customers.json");
            var oldCustomersPath = Path.Combine(localAppData, "customers.json");

            DeleteFileIfExists(ticketsPath);
            DeleteFileIfExists(customersPath);
            DeleteFileIfExists(oldCustomersPath);

            await ShowMessageAsync(
                "Klart!",
                "Alla sparade kunder och ärenden har raderats. Starta om appen för att uppdatera alla sidor.");
        }
        catch (Exception ex)
        {
            await ShowMessageAsync("Fel vid återställning", ex.Message);
        }
    }

    private static void DeleteFileIfExists(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
    }

    private async Task ShowMessageAsync(string title, string message)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            CloseButtonText = "OK",
            XamlRoot = XamlRoot
        };

        await dialog.ShowAsync();
    }
}