using AtmView.DAO.Common;
using AtmView.Entities;
using System.Data.SqlClient;

namespace AtmView.DAO
{
    public class BankRepository : GenericRepository<Bank, int>, IBankRepository
    {
        public BankRepository(AtmViewContext context)
            : base(context)
        {

        }

        public void DeleteBank(int bankId, string ConnectionString)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteBank";
            //Command.Parameters.Add("@Duree", SqlDbType.Int)
            SqlParameter param = new SqlParameter { ParameterName = "@BankId", Value = bankId };
            cmd.Parameters.Add(param);
            //cmd.Parameters.Add("@JobId", SqlDbType.Int);
            //cmd.Parameters("@JobId",).Value = 1;
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                cnn.Close();

            }
        }
    }
}
