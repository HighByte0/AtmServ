using AtmView.DAO;
using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public class AtmMonitoringService
    {

        public List<AtmMonitoringData> GetAtmInfoMonitoring(string connectionString, string userId)
        {
            List<AtmMonitoringData> list = new List<AtmMonitoringData>();

            list = new AtmMonitoringRepository().GetAtmInfoMonitoring(connectionString, userId);
            return list;
        }

    }
}
