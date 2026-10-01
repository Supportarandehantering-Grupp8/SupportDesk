using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Enums;
using SupportDesk.Application.Models;
using SupportDesk.Application.Tickets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SupportDesk.Presentation.ViewModels;

public partial class TicketDetailsViewModel(
    ITicketService ticketService) : ObservableObject
{
    private readonly ITicketService _ticketService = ticketService;

    [ObservableProperty]
    private Guid ticketId;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string? technician;

    [ObservableProperty]
    private TicketPriority priority;

    [ObservableProperty]
    private TicketStatus status;

    [ObservableProperty]
    private string newComment = string.Empty;

    [ObservableProperty]
    private List<TicketComment> comments = [];



    public async Task LoadTicketAsync(Guid id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);

        if (ticket is null)
        {
            return;
        }

        TicketId = ticket.Id;
        Title = ticket.Title;
        Description = ticket.Description;
        Technician = ticket.Technician;
        Priority = ticket.Priority;
        Status = ticket.Status;
        Comments = ticket.Comments;
    }
}