using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CommandControl")]
    public class CommandControl : Entity<int>
    {

        public string Atm_Id { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        public int Command_Id { get; set; }

        [ForeignKey("Command_Id")]
        public virtual Command Command { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public int ExecutStatus { get; set; }

        public string Arg1 { get; set; }
        public string Arg2 { get; set; }
        public string Arg3 { get; set; }
        public int Result { get; set; }
        public string Error { get; set; }
        public string Output { get; set; }

        public string User_Id { get; set; }
        public bool NotifState { get; set; }
        public int JobControleId { get; set; }
        public int Order { get; set; }
        public bool CanIgnore { get; set; }
    }
}
