using System.Data;
using System.Data.SqlClient;
using WebMyChart.Model;

namespace WebMyChart.Repo
{
    public class ProductionRepo
    {
        private string _configuration;
        BaseContext conn = new BaseContext();
        DataTable dt = new DataTable();
        string SQL = "";

        public ProductionRepo(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnection");
        }
        public int ConvertToInt(string value)
        {
            try { return int.Parse(value); } catch { return 0; }
        }

        public decimal ConvertToDecimal(string value)
        {
            try { return Convert.ToDecimal(value); } catch { return 0; }
        }
        public string test()
        {
            return "test test";
        }
        public DataTable GetData()
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            SQL = @"SELECT [ID]
                          ,[Country]
                          ,[Corn]
                          ,[Wheat]
                      FROM [Production]";
            //SQL += " AND Users.ID = @ID ";
            //command.Parameters.AddWithValue("@ID", item.ID);
            command.CommandText = SQL;
            dt = conn.ExecuteReaderWithParams(_configuration, command);
            return dt;
        }
        public bool CheckData(int id)
        {
            bool chk = false;
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            SQL = @"SELECT [ID] FROM [Production]  WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.CommandText = SQL;

            dt = conn.ExecuteReaderWithParams(_configuration, command);

            if (dt.Rows.Count > 0)
                chk = true;

            return chk;
        }
        public bool CheckDup(ProductionClass data)
        {
            bool chk = false;
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            SQL = @"SELECT [ID] FROM [Production]  WHERE ID = @ID OR Country LIKE @Country";
            command.Parameters.AddWithValue("@ID", data.ID);
            command.Parameters.AddWithValue("@Country", data.Country);
            command.CommandText = SQL;

            dt = conn.ExecuteReaderWithParams(_configuration, command);

            if (dt.Rows.Count > 0)
                chk = true;

            return chk;
        }
        public DataTable UpdateData(ProductionClass data)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            SQL = @"UPDATE Production 
                    SET  Country = @Country
                        ,Corn = @Corn
                        ,Wheat = @Wheat 
                    WHERE ID = @ID";


            command.Parameters.AddWithValue("@Country", data.Country);
            command.Parameters.AddWithValue("@Corn", data.Corn);
            command.Parameters.AddWithValue("@Wheat", data.Wheat);
            command.Parameters.AddWithValue("@ID", data.ID);
            command.CommandText = SQL;

            dt = conn.ExecuteReaderWithParams(_configuration, command);
            return dt;
        }

        public DataTable DeleteData(int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            SQL = @"DELETE FROM [Production] WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.CommandText = SQL;

            dt = conn.ExecuteReaderWithParams(_configuration, command);
            return dt;
        }

        public ProductionClass InsertData(ProductionClass data)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            SQL = @"INSERT INTO Production 
                    VALUES (@ID, @Country, @Corn, @Wheat)";


            command.Parameters.AddWithValue("@ID", data.ID);
            command.Parameters.AddWithValue("@Country", data.Country);
            command.Parameters.AddWithValue("@Corn", data.Corn);
            command.Parameters.AddWithValue("@Wheat", data.Wheat);
            command.CommandText = SQL;

            dt = conn.ExecuteReaderWithParams(_configuration, command);
            return data;
        }
    }
}
