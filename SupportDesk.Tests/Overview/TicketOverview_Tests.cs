using SupportDesk.Domain.Enums;

namespace SupportDesk.Tests.Overview;

public class TicketOverview_Tests
{
    [Fact]
    public async Task Search_ShouldBeCaseInsensitive()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "sKrivaren",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("Skrivaren", result[0].Title);
    }

    [Fact]
    public async Task Search_ShouldFindPartialTitle()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "Skriv",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("Skrivaren", result[0].Title);
    }

    [Fact]
    public async Task Search_ShouldFindDescription()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "skriva ut dokument",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("Skrivaren", result[0].Title);
    }

    [Fact]
    public async Task Search_ShouldFindCustomerName()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "Sebastian",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("Skrivaren", result[0].Title);
    }

    [Fact]
    public async Task StatusFilter_ShouldReturnOnlySelectedStatus()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "",
            TicketStatus.Resolved,
            null,
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal(TicketStatus.Resolved, result[0].Status);
    }

    [Fact]
    public async Task Search_ShouldFindAllTicketsForCustomer()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "Ahli",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, ticket => ticket.Title == "Datorn startar inte");
        Assert.Contains(result, ticket => ticket.Title == "E-post fungerar inte");
    }

    [Fact]
    public async Task Search_ShouldReturnEmptyWhenCustomerHasNoTickets()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "Scott",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Empty(result);
    }

    [Fact]
    public async Task SearchAndStatusFilter_ShouldWorkTogether()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "Malin",
            TicketStatus.Resolved,
            null,
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("Problem med nätverket", result[0].Title);
        Assert.Equal(TicketStatus.Resolved, result[0].Status);
    }

    [Fact]
    public async Task TechnicianFilter_ShouldReturnOnlySelectedTechnician()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "",
            null,
            "Peter",
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("E-post fungerar inte", result[0].Title);
        Assert.Equal("Peter", result[0].Technician);
    }

    [Fact]
    public async Task THEBIGONE()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "Malin",
            TicketStatus.Resolved,
            "Alex",
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("Problem med nätverket", result[0].Title);
        Assert.Equal(TicketStatus.Resolved, result[0].Status);
        Assert.Equal("Alex", result[0].Technician);
    }

    [Fact]
    public async Task Sort_ShouldOrderNewestFirst()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Equal(4, result.Count);

        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].CreatedAt >= result[i + 1].CreatedAt);
        }
    }

    [Fact]
    public async Task Sort_ShouldOrderOldestFirst()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "",
            null,
            null,
            TicketSortOrder.Oldest);

        Assert.Equal(4, result.Count);

        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].CreatedAt <= result[i + 1].CreatedAt);
        }
    }

    [Fact]
    public async Task Search_ShouldIgnoreLeadingAndTrailingSpaces()
    {
        var data = await TicketOverviewTestData.CreateAsync();

        var result = await data.TicketService.GetBySearchAsync(
            "   Skriv   ",
            null,
            null,
            TicketSortOrder.Newest);

        Assert.Single(result);
        Assert.Equal("Skrivaren", result[0].Title);
    }
}