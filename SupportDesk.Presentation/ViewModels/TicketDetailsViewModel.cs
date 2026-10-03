using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Enums;
using SupportDesk.Application.Models;
using SupportDesk.Application.Tickets;
using SupportDesk.Presentation.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels;

public partial class TicketDetailsViewModel(
    ITicketService ticketService,
    INavigationService navigationService) : ObservableObject
{
    private readonly ITicketService _ticketService = ticketService;
    private readonly INavigationService _navigationService = navigationService;

    public List<TicketPriority> Priorities { get; } =
        Enum.GetValues<TicketPriority>().ToList();

    public List<TicketStatus> Statuses { get; } =
        Enum.GetValues<TicketStatus>().ToList();

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

    [RelayCommand]
    private async Task SaveTicketAsync()
    {
        var request = new UpdateTicketRequest
        {
            TicketId = TicketId,
            Title = Title,
            Description = Description,
            Technician = Technician ?? string.Empty,
            Priority = Priority,
            Status = Status
        };

        await _ticketService.UpdateTicketAsync(request);
    }

    [RelayCommand]
    private async Task AddCommentAsync()
    {
        if (string.IsNullOrWhiteSpace(NewComment))
        {
            return;
        }

        await _ticketService.AddCommentAsync(
            TicketId,
            NewComment);

        NewComment = string.Empty;

        await LoadTicketAsync(TicketId);
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.NavigateTo(AppPage.Tickets);
    }
}