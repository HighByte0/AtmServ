using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AtmView.DAO
{
    public class AtmMonitoringRepository
    {

        public List<AtmMonitoringData> GetAtmInfoMonitoring(string connectionString, string userId)
        {
            List<AtmMonitoringData> list = new List<AtmMonitoringData>();



            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
#if true
            cmd.CommandText = "GetAtmInfoMonitoring2";
#else
            cmd.CommandText = "GetAtmInfoMonitoring";
#endif
            //add any parameters the stored procedure might require

            SqlParameter param = new SqlParameter { ParameterName = "@UserId", Value = userId };
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
                        AtmMonitoringData item = new AtmMonitoringData();
                        item.AtmId = rdr["AtmId"].ToString();
                        item.AtmName = rdr["AtmName"].ToString();
                        item.profile = rdr["AtmProfile"].ToString();
                        if (!string.IsNullOrEmpty(rdr["MaxStateId"].ToString()))
                            item.StateId = Int32.Parse(rdr["MaxStateId"].ToString());
#if true
                        //Connected StateTypeId StateTypeId2
                        //if (!string.IsNullOrEmpty(rdr["Connected"].ToString()))
                        //    item.Connected = Int32.Parse(rdr["Connected"].ToString());
                        //if (!string.IsNullOrEmpty(rdr["StateTypeId"].ToString()))
                        //    item.StateTypeId = Int32.Parse(rdr["StateTypeId"].ToString());
                        if (!string.IsNullOrEmpty(rdr["StateTypeId2"].ToString()))
                            item.StateTypeId = Int32.Parse(rdr["StateTypeId2"].ToString());
#else
                        if (!string.IsNullOrEmpty(rdr["StateType_Id"].ToString()))
                            item.StateTypeId = Int32.Parse(rdr["StateType_Id"].ToString());
#endif

                        item.StateLabel = rdr["StateLabel"].ToString();
                        item.CssClass = rdr["CssClass"].ToString();
                        item.Color = rdr["Color"].ToString();
                        if (!string.IsNullOrEmpty(rdr["StateDate"].ToString()))
                            item.StateDate = DateTime.Parse(rdr["StateDate"].ToString());
                        // Modif Mdo BPM
                        if (!string.IsNullOrEmpty(rdr["LastSeen"].ToString()))
                            item.LastSeen = DateTime.Parse(rdr["LastSeen"].ToString());
                        if (!string.IsNullOrEmpty(rdr["LastTransaction"].ToString()))
                            item.LastTransaction = DateTime.Parse(rdr["LastTransaction"].ToString());
                        //Mdfs LastReboot
                        if (!string.IsNullOrEmpty(rdr["LastReboot"].ToString()))
                            item.LastReboot = DateTime.Parse(rdr["LastReboot"].ToString());
                        //FIn Modif
                        if (!string.IsNullOrEmpty(rdr["AtmErrorId"].ToString()))
                            item.AtmErrorId = Int32.Parse(rdr["AtmErrorId"].ToString());

                        if (!string.IsNullOrEmpty(rdr["ErrStartDate"].ToString()))
                            item.ErrStartDate = DateTime.Parse(rdr["ErrStartDate"].ToString());

                        if (!string.IsNullOrEmpty(rdr["ErrEndDate"].ToString()))
                            item.ErrEndDate = DateTime.Parse(rdr["ErrEndDate"].ToString());

                        if (!string.IsNullOrEmpty(rdr["ActionCorrectiveId"].ToString()))
                            item.ActionCorrectiveId = Int32.Parse(rdr["ActionCorrectiveId"].ToString());
                        item.ActionCorrectiveName = rdr["ActionCorrectiveName"].ToString();
                        item.acUserId = rdr["acUserId"].ToString();
                        //item.Error = rdr["Error"].ToString();
                        //if (!string.IsNullOrEmpty(rdr["IdErrorType"].ToString()))
                        //    item.IdErrorType = Int32.Parse(rdr["IdErrorType"].ToString());

                        //if (!string.IsNullOrEmpty(rdr["IdatmRemarque"].ToString()))
                        //    item.IdAtmRemarque = Int32.Parse(rdr["IdatmRemarque"].ToString());
                        //item.Remarque = rdr["Remarque"].ToString();

                        if (!string.IsNullOrEmpty(rdr["Bug_Id"].ToString()))
                            item.BugId = Int32.Parse(rdr["Bug_Id"].ToString());

                        if (item.StateTypeId == 4 && item.BugId.HasValue)
                            item.EtatErrorIncident = "2D";

                        if (item.StateTypeId == 4 && !item.BugId.HasValue)
                            item.EtatErrorIncident = "1D";

                        item.ComponentStates = new List<ComponentState>();
                        item.Remarques= new List<AtmRemarque>();
                        list.Add(item);
                    }

                    rdr.NextResult();

                    List<ComponentState> cmpStateList = new List<ComponentState>();
                    List<AtmRemarque>  Remarques = new List<AtmRemarque>();
                    while (rdr.Read())
                    {
                        ComponentState elt = new ComponentState();

                        elt.State_Id = Int32.Parse(rdr["state_id"].ToString());
                        elt.Component_Id = Int32.Parse(rdr["Component_Id"].ToString());
                        elt.StateComponent_Id = Int32.Parse(rdr["StateComponent_Id"].ToString());
                        elt.Component = new Component { Id = Int32.Parse(rdr["Component_Id"].ToString()), Label = rdr["ComponentLabel"].ToString() };

                        cmpStateList.Add(elt);
                    }
                    list.ForEach(elt => elt.ComponentStates.AddRange(cmpStateList.Where(x => x.State_Id == elt.StateId)));
                    rdr.NextResult();
                    while (rdr.Read())
                    {
                        AtmRemarque elt = new AtmRemarque();

                        elt.Id = Int32.Parse(rdr["Id"].ToString());
                        elt.Atm_Id = rdr["Atm_Id"].ToString();
                        elt.Remarque = rdr["Remarque"].ToString();

                        Remarques.Add(elt);
                    }
                    list.ForEach(elt => elt.Remarques.AddRange(Remarques.Where(x => x.Atm_Id == elt.AtmId)));

                    foreach (var elt in list)
                    {
                        // Vérifiez si la liste des remarques de l'élément n'est pas vide
                        if (elt.Remarques.Any())
                        {
                            // Ajoutez "3D" à la propriété EtatErrorIncident
                            elt.EtatErrorIncident = "3D";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
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
