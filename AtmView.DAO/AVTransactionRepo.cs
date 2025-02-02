using AtmView.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AtmView.DAO
{


    public class AVTransactionRepo
    {


        public List<AVTransaction> GetListTransaction(string connectionString, AVTransactionParams avParams)
        {
            List<AVTransaction> list = new List<AVTransaction>();

            //string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;

            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_GetTransactionByParams";
            //add any parameters the stored procedure might require

            SqlParameter param = new SqlParameter { ParameterName = "@StartDate", Value = avParams.StartDate };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@EndDate", Value = avParams.EndDate };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@AtmId", Value = avParams.AtmId };
            cmd.Parameters.Add(param);


            SqlDataReader rdr = null;
            try
            {


                cnn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        list.Add(new AVTransaction
                        {
                            AtmID = rdr["AtmID"].ToString(),
                            TransactionDate = DateTime.Parse(rdr["TransactionDate"].ToString()),
                            TransactionNumber = Int32.Parse(rdr["TransactionNumber"].ToString()),
                            AutorisationNumber = rdr["AutorisationNumber"].ToString(),
                            CardNumber = rdr["CardNumber"].ToString(),
                            Rib = rdr["Rib"].ToString(),
                            Type = rdr["Type"].ToString(),
                            Amount = Int32.Parse(rdr["Amount"].ToString()),
                            Statut = Int32.Parse(rdr["Statut"].ToString()),

                            isCashPresented = bool.Parse(rdr["isCashPresented"].ToString()),

                            isCashTaken = bool.Parse(rdr["isCashTaken"].ToString()),
                            isCashRetracted = bool.Parse(rdr["isCashRetracted"].ToString()),
                            isCashoutError = bool.Parse(rdr["isCashoutError"].ToString()),
                            ExistInHost = bool.Parse(rdr["ExistInHost"].ToString())

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
            rdr = null;
            return list;
        }



        public void Update(string connectionString, AVTransactionParams avParams)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_UpdateTransaction";
            //Command.Parameters.Add("@Duree", SqlDbType.Int)
            SqlParameter param = new SqlParameter { ParameterName = "@AtmId", Value = avParams.AtmId };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@TransactionDate", Value = avParams.TransactionDate };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@TransactionNumber", Value = avParams.TransactionNumber };
            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@AutorisationNumber", Value = avParams.AutorisationNumber };
            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@Statut", Value = avParams.Statut };
            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@ExistInHost", Value = avParams.ExistInHost };
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

