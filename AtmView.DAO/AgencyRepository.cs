using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.DAO
{
    public class AgencyRepository : GenericRepository<Agency, string>, IAgencyRepository
    {
        public AgencyRepository(AtmViewContext context)
            : base(context)
        {

        }

        public void DeleteAgency(string AgencyId, string ConnectionString)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteAgency";

            SqlParameter param = new SqlParameter { ParameterName = "@AgencyId", Value = AgencyId };
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
