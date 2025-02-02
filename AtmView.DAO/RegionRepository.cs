using AtmView.DAO.Common;
using AtmView.Entities;
using System.Data.SqlClient;

namespace AtmView.DAO
{
    public class RegionRepository : GenericRepository<Region, int>, IRegionRepository
    {
        public RegionRepository(AtmViewContext context)
            : base(context)
        {

        }

        public void DeleteRegion(int regionId, string ConnectionString)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteRegion";

            SqlParameter param = new SqlParameter { ParameterName = "@RegionId", Value = regionId };
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
