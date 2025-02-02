using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AtmView.Entities;


namespace AtmView.Models
{
    [Table("PasswordHistory")]
    public class PasswordHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string HashedPassword { get; set; }
        public DateTime ChangedDate { get; set; }
    }

}