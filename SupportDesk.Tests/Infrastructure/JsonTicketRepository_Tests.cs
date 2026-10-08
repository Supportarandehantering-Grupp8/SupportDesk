using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;
using SupportDesk.Infrastructure.Tickets;

namespace SupportDesk.Tests.Infrastructure;

public class JsonTicketRepository_Tests
{
    [Fact]
    public async Task GetByIdAsync_Should_ReturnSameTicketAfterDeserialization()
    {
        var tempPath = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.json"
        );

        try
        {
            var repository = new JsonTicketRepository(tempPath);

            var ticket = new SupportTicket(
                "title",
                "description",
                Guid.NewGuid(),
                TicketPriority.High
            );

            await repository.AddAsync(ticket);

            var ticketFromJson = await repository.GetByIdAsync(ticket.Id);

            Assert.NotNull(ticketFromJson);
            Assert.Equal(ticket.Title, ticketFromJson.Title);
            Assert.Equal(ticket.Description, ticketFromJson.Description);
            Assert.Equal(ticket.Id, ticketFromJson.Id);
            Assert.Equal(ticket.Priority, ticketFromJson.Priority);
        }
        finally
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }
}
