
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Tests.Domain;

public class SupportTicket_Tests
{
    [Fact]
    public void Constructor_Should_CreateTicketWhenValuesAreValid()
    {
        string title = "Kan inte logga in";
        string description = "Det går inte att logga in via hemsidan";
        var customerId = Guid.NewGuid();
        TicketPriority priority = TicketPriority.High;

        var ticket = new SupportTicket(title, description, customerId, priority);

        Assert.NotEqual(Guid.Empty, ticket.Id);
        Assert.Equal(title, ticket.Title);
        Assert.Equal(description, ticket.Description);
        Assert.Equal(priority, ticket.Priority);
        Assert.Equal(TicketStatus.New, ticket.Status);
        Assert.NotEqual(default, ticket.CreatedAt);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_Should_ThrowWhenTitleIsInvalid(string? title)
    {
        var exception = Assert.Throws<ArgumentException>(() => 
            new SupportTicket(title!, "Det går inte att logga in via hemsidan", Guid.NewGuid(), TicketPriority.Normal)
        );

        Assert.Equal("Rubrik får inte vara tom.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_Should_ThrowWhenDescriptionIsInvalid(string? description)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new SupportTicket("Kan inte logga in", description!, Guid.NewGuid(), TicketPriority.Normal)
        );

        Assert.Equal("Beskrivning får inte vara tom.", exception.Message);
    }
}
