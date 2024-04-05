using Microsoft.AspNetCore.SignalR;
using SignalR.Repository;

namespace SignalR.Hubs;

public class DashboardHub:Hub
{
    EarningRepository repository;
    public DashboardHub(IConfiguration configuration)
    {
        var connectionstring=configuration.GetConnectionString("DefaultConnection");
        this.repository = new EarningRepository(connectionstring);
    }

    public async Task SendEarnings()
    {
        var earnings = repository.GetEarnings(); 
        await Clients.All.SendAsync("ReceiveEarnings", earnings);
    }
}
