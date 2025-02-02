using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("RecentAtmState")]
    public class RecentAtmState : Entity<int>
    {

        public string Atm_Id { get; set; }


        public int State_Id { get; set; }


        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }


        [ForeignKey("State_Id")]
        public virtual State State { get; set; }
        public bool Connected { get; set; }
        public int LastStateType { get; set; }
        [ForeignKey("LastStateType")]
        public StateType StateType { get; set; }
        public string md5Hash { get; set; }
#if true//lastseen_lasttrx
        public DateTime LastSeen { get; set; }
        public DateTime LastTransaction { get; set; }
#endif//lastseen_lasttrx
        public int NotifState { get; set; }
#if true//monitoring
        public uint statehistory { get; set; }
        public bool checkflag { get; set; }
        public bool cashalert { get; set; }
        public DateTime? LastReboot { get; set; }
        public string LastRebootReason { get; set; }
        public double? cashStock { get; set; }
#endif//monitoring
    }
}
