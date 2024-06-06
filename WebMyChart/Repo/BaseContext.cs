using System.Data.SqlClient;
using System.Data;

namespace WebMyChart.Repo
{
    public class BaseContext
    {
        private string connectionString;
        IConfiguration configuration;
        public DataTable ExecuteReaderWithParams(string datasource, SqlCommand Command)
        {
            using (SqlConnection Con = new SqlConnection(datasource))
            {
                using (SqlCommand Com = new SqlCommand())
                {
                    DataTable dt = new DataTable();
                    try
                    {

                        Con.Open();
                        Command.Connection = Con;
                        Command.CommandTimeout = 500;
                        //NpgsqlDataReader reader = Command.ExecuteReader();
                        //dt.Load(reader);
                        dt.Load(Command.ExecuteReader());
                        Command.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        string aa = ex.Message;
                        Con.Close();
                        Com.Dispose();
                        Com.Parameters.Clear();
                        return dt;
                    }
                    Con.Close();
                    Com.Dispose();
                    return dt;
                }
            }

            //SqlConnection Con = new SqlConnection(datasource);
            //SqlCommand Com = new SqlCommand();
            //NpgsqlTransaction Transections;



        }

        public void ExecuteNonQueryWithParams(string datasource, SqlCommand Command)
        {
            SqlConnection Con = new SqlConnection(datasource);
            SqlCommand Com = new SqlCommand();
            //NpgsqlTransaction Transections;

            DataTable dt = new DataTable();
            try
            {

                Con.Open();
                Command.Connection = Con;
                Command.CommandTimeout = 500;
                dt.Load(Command.ExecuteReader());
                Command.Parameters.Clear();
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
                Con.Close();
                Com.Dispose();
                Com.Parameters.Clear();
            }
            Con.Close();
            Com.Dispose();
        }
    }
}
