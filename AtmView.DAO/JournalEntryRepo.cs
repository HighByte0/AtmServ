using AtmView.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AtmView.DAO
{
    public class JournalEntryRepo
    {



        public object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }

        public List<JournalEntry> GetJournalEntryDataArchive(JournalEntyRequest request, string connectionString)
        {
            List<JournalEntry> list = new List<JournalEntry>();


            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_GetJournalEntryDataArchive";
            //add any parameters the stored procedure might require

            SqlParameter param = new SqlParameter { ParameterName = "@AtmId", Value = request.AtmId };
            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@StartDate", Value = ToDBNull(request.StartDate) };

            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@EndDate", Value = ToDBNull(request.EndDate) };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@SearchText", Value = request.SearchText };
            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@UserId", Value = request.UserId };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@IsAdmin", Value = request.IsAdmin };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@TodayJournal", Value = request.TodayJournal };
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
                        list.Add(new JournalEntry
                        {
                            Atm_Id = (String)rdr["Atm_Id"],
                            Data = (String)rdr["Data"],
                            EntryTime = (DateTime)rdr["EntryTime"],
                            // Filename = int.Parse(rdr["Filename"].ToString())
                            Filename = rdr["Filename"].ToString()
                        });


                    }
                }

            }
#pragma warning disable CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
            {

                rdr = null;
            }
            finally
            {
                cnn.Close();

            }
            return list;
        }

        public int InsertJournalEntryArchive(AtmView.Entities.JournalEntry journalEntry, string connectionString)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_InsertJournalEntryArchive";
            //add any parameters the stored procedure might require

            SqlParameter param = new SqlParameter { ParameterName = "@AtmId", Value = journalEntry.Atm_Id };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@EntryTime", Value = journalEntry.EntryTime };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@Data", Value = journalEntry.Data };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@Filename", Value = journalEntry.Filename };
            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@EntryType", Value = journalEntry.EntryType };
            cmd.Parameters.Add(param);
            int returnValue = 0;
            try
            {
                cnn.Open();
                returnValue = cmd.ExecuteNonQuery();
            }
#pragma warning disable CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
            {
                // rdr = null;
            }
            finally
            {
                cnn.Close();

            }
            return returnValue;
        }


        public List<JournalEntry> GetJournalEntryData(JournalEntyRequest request, string connectionString)
        {
            List<JournalEntry> list = new List<JournalEntry>();


            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PS_GetJournalEntryData";
            //add any parameters the stored procedure might require

            SqlParameter param = new SqlParameter { ParameterName = "@AtmId", Value = request.AtmId };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@UserId", Value = request.UserId };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@IsAdmin", Value = request.IsAdmin };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@TodayJournal", Value = request.TodayJournal };
            cmd.Parameters.Add(param);
            //param = new SqlParameter { ParameterName = "@StartDate", Value = request.StartDate };
            //cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@StartDate", Value = ToDBNull(request.StartDate) };
            //request.StartDate == null ? DBNull.Value : request.StartDate.Value 
            cmd.Parameters.Add(param);

            param = new SqlParameter { ParameterName = "@EndDate", Value = ToDBNull(request.EndDate) };
            cmd.Parameters.Add(param);
            param = new SqlParameter { ParameterName = "@SearchText", Value = request.SearchText };
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
                        list.Add(new JournalEntry
                        {
                            Atm_Id = (String)rdr["Atm_Id"],
                            Data = (String)rdr["Data"],
                            EntryTime = (DateTime)rdr["EntryTime"],
                            // Filename = int.Parse(rdr["Filename"].ToString())
                            Filename = rdr["Filename"].ToString()
                        });


                    }
                }

            }
#pragma warning disable CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
            {

                rdr = null;
            }
            finally
            {
                cnn.Close();

            }
            return list;
        }
    }
}
