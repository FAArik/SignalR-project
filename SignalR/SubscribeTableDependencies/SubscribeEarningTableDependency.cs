using SignalR.Hubs;
using SignalR.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR.SubscribeTableDependencies
{
    public class SubscribeEarningTableDependency
    {
        SqlTableDependency<Earning> tableDependency;
        DashboardHub dashboardHub;
        public SubscribeEarningTableDependency(DashboardHub dashboardHub, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            tableDependency = new SqlTableDependency<Earning>(connectionstring, "Earnings");
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency()
        {
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Earning> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                await dashboardHub.SendEarnings();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Earning)} sql table dependency error:{e.Error.Message}");
        }
    }
}
