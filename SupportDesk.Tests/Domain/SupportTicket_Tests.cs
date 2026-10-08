
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

    [Fact]
    public void ChangeStatus_ShouldThrow_WhenChangingToInProgressWithoutTechnician()
    {
        var ticket = new SupportTicket(
            "Test title",
            "Test description",
            Guid.NewGuid(),
            TicketPriority.Normal
        );

        Assert.Throws<InvalidOperationException>(() =>
            ticket.ChangeStatus(TicketStatus.InProgress));
    }

    [Fact]
    public void ChangeStatus_ShouldWork_WhenTechnicianIsAssigned()
    {
        var ticket = new SupportTicket(
            "Test title",
            "Test description",
            Guid.NewGuid(),
            TicketPriority.Normal
        );

        ticket.AssignTechnician("Ali");

        ticket.ChangeStatus(TicketStatus.InProgress);

        Assert.Equal(TicketStatus.InProgress, ticket.Status);
    }

    [Fact]
    public void ChangePriority_ShouldUpdatePriority()
    {
        var ticket = new SupportTicket(
            "Test title",
            "Test description",
            Guid.NewGuid(),
            TicketPriority.Low
        );

        ticket.ChangePriority(TicketPriority.High);

        Assert.Equal(TicketPriority.High, ticket.Priority);
    }

    [Fact]
    public void AddComment_ShouldAddComment()
    {
        var ticket = new SupportTicket(
            "Test title",
            "Test description",
            Guid.NewGuid(),
            TicketPriority.Normal
        );

        ticket.AddComment("Test comment");

        Assert.Single(ticket.Comments);
        Assert.Equal("Test comment", ticket.Comments[0].Text);
    }

    [Fact]
    public void AddComment_ShouldThrow_WhenCommentIsEmpty()
    {
        var ticket = new SupportTicket(
            "Test title",
            "Test description",
            Guid.NewGuid(),
            TicketPriority.Normal
        );

        Assert.Throws<ArgumentException>(() =>
            ticket.AddComment(" "));
    }
}
