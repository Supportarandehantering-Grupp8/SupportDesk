using SupportDesk.Application.Tickets;
using SupportDesk.Application.Enums;
using SupportDesk.Application.Models;
using SupportDesk.Infrastructure.Data;
using System.Text.Json;

namespace SupportDesk.Infrastructure.Tickets;

public class JsonTicketRepository : ITicketRepository
{
    private readonly string _filePath = DataPaths.Tickets;

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
    };
    public async Task AddAsync(SupportTicket ticket)
    {
        var tickets = await LoadTicketsAsync();

        tickets.Add(ticket);

        await SaveTicketsAsync(tickets);
    }

    public async Task<IReadOnlyList<SupportTicket>> GetAllAsync()
    {
        var tickets = await LoadTicketsAsync();

        return tickets;
    }

    public async Task<SupportTicket?> GetByIdAsync(Guid id)
    {
        var tickets = await LoadTicketsAsync();

        var ticket = tickets.FirstOrDefault(t => t.Id == id);

        return ticket;
    }

    public async Task<IReadOnlyList<SupportTicket>> GetByStatusAsync(TicketStatus status)
    {
        var tickets = await LoadTicketsAsync();

        var ticketsByStatus = tickets.Where(t => t.Status == status).ToList();

        return ticketsByStatus;
    }

    public async Task UpdateAsync(SupportTicket ticket)
    {
        var tickets = await LoadTicketsAsync();

        var index = tickets.FindIndex(t => t.Id == ticket.Id);

        if (index == -1)
            throw new ArgumentException("Ticket could not be found", nameof(ticket));

        tickets[index] = ticket;

        await SaveTicketsAsync(tickets);
    }

    private async Task<List<SupportTicket>> LoadTicketsAsync()
    {
        if (!File.Exists(_filePath))
            return [];

        var json = await File.ReadAllTextAsync(_filePath);

        var tickets = JsonSerializer.Deserialize<List<SupportTicket>>(json, _options)
            ?? throw new JsonException("Failed to load tickets from the JSON file");

        return tickets;
    }

    private async Task SaveTicketsAsync(IReadOnlyList<SupportTicket> tickets)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        string newJson = JsonSerializer.Serialize(tickets, _options);

        var tempPath = _filePath + ".tmp";

        await File.WriteAllTextAsync(tempPath, newJson);

        File.Move(tempPath, _filePath, overwrite: true);
    }
}
