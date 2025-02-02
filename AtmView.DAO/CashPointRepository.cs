using AtmView.DAO.Common;
using AtmView.Entities;
using System.Data.SqlClient;
namespace AtmView.DAO
{
    public class CashPointRepository : GenericRepository<CashPoint, int>, ICashPointRepository
    {
        public CashPointRepository(AtmViewContext context)
            : base(context)
        {

        }

        public void DeleteCashPoint(int CashPointId, string ConnectionString)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteCashPoint";

            SqlParameter param = new SqlParameter { ParameterName = "@CashPointId", Value = CashPointId };
            cmd.Parameters.Add(param);

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
