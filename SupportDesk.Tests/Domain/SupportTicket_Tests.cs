using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Tests.Domain;

public class SupportTicket_Tests
{
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