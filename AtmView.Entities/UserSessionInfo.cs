using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("UserSessionInfo")]
    public class UserSessionInfo : Entity<string>
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public String Id { get; set; }
        public string AdressIP { get; set; }
        public string UserId { get; set; }

    }
}
