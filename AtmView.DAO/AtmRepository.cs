using AtmView.DAO.Common;
using AtmView.Entities;
using System.Data.SqlClient;

namespace AtmView.DAO
{
    public class AtmRepository : GenericRepository<Atm, string>, IAtmRepository
    {
        public AtmRepository(AtmViewContext context)
            : base(context)
        {

        }

        public void DeleteAtm(string atmId, string ConnectionString)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteATM";

            SqlParameter param = new SqlParameter { ParameterName = "@AtmId", Value = atmId };
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
