using AtmView.DAO.Common;
using AtmView.Entities;
using System.Data.SqlClient;

namespace AtmView.DAO
{
    public class BranchRepository : GenericRepository<Branch, int>, IBranchRepository
    {
        public BranchRepository(AtmViewContext context)
            : base(context)
        {

        }

        public void DeleteBranch(int branchId, string ConnectionString)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteBranch";

            SqlParameter param = new SqlParameter { ParameterName = "@BranchId", Value = branchId };
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
