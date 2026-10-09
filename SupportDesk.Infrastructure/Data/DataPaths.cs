namespace SupportDesk.Infrastructure.Data;

internal static class DataPaths
{
    internal static readonly string Tickets = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SupportDesk",
        "tickets.json"
    );

    internal static readonly string Customers = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SupportDesk",
        "customers.json"
    );
}