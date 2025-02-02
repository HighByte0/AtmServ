using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Log")]
    public class Log : Entity<int>

    {

        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }
        public bool IsHttpPost { get; set; }
        public DateTime ActionDate { get; set; }

        public string User_Id { get; set; }
        public string Session_Id { get; set; }
        public string AtmId { get; set; }
        public string IpAdresse { get; set; }

    }
}
