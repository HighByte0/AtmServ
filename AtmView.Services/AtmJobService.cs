using AtmView.DAO;
using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public class AtmJobService
    {
        public List<JobAtm> GetAtmJobListToExecute(string strConn)
        {
            List<JobAtm> list = new List<JobAtm>();

            list = new ATMJobRepository().GetAtmJobListToExecute(strConn);
            return list;
        }

        public void DeleteJob(string connectionString, int JobId)
        {
            new ATMJobRepository().DeleteJob(connectionString, JobId);
        }
    }
}
