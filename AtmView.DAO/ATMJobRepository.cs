using AtmView.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AtmView.DAO
{
    public class ATMJobRepository
    {

        public List<JobAtm> GetAtmJobListToExecute(string connectionString)
        {
            List<JobAtm> list = new List<JobAtm>();

            //string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;

            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_GetAtmJobListToExecute";
            //add any parameters the stored procedure might require
            SqlDataReader rdr = null;
            try
            {


                cnn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        list.Add(new JobAtm
                        {
                            Atm_Id = rdr["Atm_Id"].ToString(),
                            Job_Id = int.Parse(rdr["Job_Id"].ToString())
                        });

                    }
                }


                //list.Add(new Atm { Id = "atm02", Description = "desc2" });
            }
            catch
            {
                rdr = null;
            }
            finally
            {
                cnn.Close();

            }
            return list;
        }

        public void DeleteJob(string connectionString, int JobId)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_DeleteJob";
            //Command.Parameters.Add("@Duree", SqlDbType.Int)
            SqlParameter param = new SqlParameter { ParameterName = "@JobId", Value = JobId };
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
