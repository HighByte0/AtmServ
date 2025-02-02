using AtmView.DAO.Common;
using AtmView.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AtmView.DAO
{
    public class AVTransactionRepository : GenericRepo<AVTransaction>, IAVTransactionRepository
    {
        private readonly AtmViewContext _context;
        public AVTransactionRepository(AtmViewContext context)
            : base(context)
        {
            _context = context;
        }
        public List<AVTransaction> GetTransactions(DateTime SartDate, DateTime EndDate, string ConnectionString, IEnumerable<UserAtm> atmuser)
        {
            bool k = true;
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_GetTransactions";
            DataTable AtmUsers = new DataTable();
            if (atmuser != null)
            {
                AtmUsers.Columns.Add("User_Id", typeof(string));
                AtmUsers.Columns.Add("Atm_Id", typeof(string));
                foreach (UserAtm str in atmuser)
                {
                    DataRow row = AtmUsers.NewRow();
                    row["User_Id"] = str.User_Id;
                    row["Atm_Id"] = str.Atm_Id;
                    AtmUsers.Rows.Add(row);
                }
                k = false;
            }

            SqlParameter param1 = new SqlParameter { ParameterName = "@StartDate", Value = SartDate };
            cmd.Parameters.Add(param1);
            SqlParameter param2 = new SqlParameter { ParameterName = "@EnDate", Value = EndDate };
            cmd.Parameters.Add(param2);
            if (k == false)
            {
                SqlParameter param3 = new SqlParameter { ParameterName = "@UserAtms", Value = AtmUsers };
                cmd.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter { ParameterName = "@IsAdmin ", Value = 0 };
                cmd.Parameters.Add(param4);
            }
            else
            {
                SqlParameter param4 = new SqlParameter { ParameterName = "@IsAdmin ", Value = 1 };
                cmd.Parameters.Add(param4);
            }
            List<AVTransaction> TransactionList = new List<AVTransaction>();
            DataTable dt = new DataTable();
            try
            {
                cnn.Open();
                //SqlDataReader reader = cmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 100;
                da.Fill(dt);
                AVTransaction trx = null;
                foreach (DataRow reader in dt.Rows)
                {
                    trx = new AVTransaction();
                    float inputValue = 0;
                    trx.AtmID = reader["AtmID"].ToString();
                    trx.TransactionDate = Convert.ToDateTime(reader["TransactionDate"].ToString());
                    trx.TransactionNumber = int.Parse(reader["TransactionNumber"].ToString());
                    trx.AutorisationNumber = reader["AutorisationNumber"].ToString();
                    trx.CardNumber = reader["CardNumber"].ToString();
                    trx.Rib = reader["Rib"].ToString();
                    trx.Type = reader["Type"].ToString();
                    trx.Amount = int.Parse(reader["Amount"].ToString());
                    trx.Statut = int.Parse(reader["Statut"].ToString());
                    trx.isCashPresented = bool.Parse(reader["isCashPresented"].ToString());
                    trx.isCashTaken = bool.Parse(reader["isCashTaken"].ToString());
                    trx.isCashRetracted = bool.Parse(reader["isCashRetracted"].ToString());
                    trx.isCashoutError = bool.Parse(reader["isCashoutError"].ToString());
                    trx.ExistInHost = bool.Parse(reader["ExistInHost"].ToString());
                    trx.IsRejected = bool.Parse(reader["IsRejected"].ToString());
                    trx.TrxPictures = reader["TrxPictures"].ToString();
                    trx.ExtraInfos = reader["ExtraInfos"].ToString();
                    string f = reader["Duration"].ToString();

                    trx.Duration = float.TryParse(reader["Duration"].ToString(), out inputValue)?float.Parse(reader["Duration"].ToString()):0 ;

                    TransactionList.Add(trx);
                }
            }
#pragma warning disable CS0168 // La variable 'e' est déclarée, mais jamais utilisée
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' est déclarée, mais jamais utilisée
            {
            }
            finally
            {
                cnn.Close();

            }
            dt = null;
            return TransactionList;
        }

        public IEnumerable<T> GetTransactions_usp<T>(string connectionString, string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandTimeout: 100, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public AVTransaction FindTransaction(string atmId, DateTime transactionDate, int transactionNumber)
        {
            return this._context.AVTransactions
                .FirstOrDefault(x => x.AtmID == atmId && x.TransactionDate == transactionDate && x.TransactionNumber == transactionNumber);
        }

    }
}

