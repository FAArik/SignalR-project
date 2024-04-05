using SignalR.Models;
using System.Data;
using System.Data.SqlClient;

namespace SignalR.Repository
{
    public class EarningRepository
    {
        string connectionString;
        public EarningRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Earning> GetEarnings()
        {
            List<Earning> earnings = new List<Earning>();
            Earning earning;

            var data = getEarningsFromDb();
            foreach (DataRow row in data.Rows)
            {
                earning = new Earning
                {
                    TxHash = Convert.ToString(row["TxHash"]),
                    From = Convert.ToString(row["From"]),
                    To = Convert.ToString(row["To"]),
                    Amount = Convert.ToString(row["Amount"]),
                };
                earnings.Add(earning);
            }
            return earnings;
        }

        public DataTable getEarningsFromDb()
        {
            var query = "SELECT * FROM Earnings";
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            data.Load(reader);
                        }
                    }
                    return data;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
