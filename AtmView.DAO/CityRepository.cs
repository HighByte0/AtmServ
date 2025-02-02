using AtmView.DAO.Common;
using AtmView.Entities;
using System.Data.SqlClient;

namespace AtmView.DAO
{
    public class CityRepository : GenericRepository<City, int>, ICityRepository
    {
        public CityRepository(AtmViewContext context)
            : base(context)
        {

        }

        public void DeleteCity(int cityId, string ConnectionString)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteCity";

            SqlParameter param = new SqlParameter { ParameterName = "@CityId", Value = cityId };
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

