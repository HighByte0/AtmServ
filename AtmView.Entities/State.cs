using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace AtmView.Entities
{
    [Table("State")]
    public class State : Entity<int>
    {
        public int StateType_Id { get; set; }
        [ForeignKey("StateType_Id")]
        public virtual StateType StateType { get; set; }

        public DateTime StateDate { get; set; }
        public bool ToSave { get; set; }

        public string Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        public DateTime LastTransaction { get; set; }

        public virtual ICollection<ComponentState> ComponentStates { get; set; }
        public virtual ICollection<CashStock> CashStocks { get; set; }
        //public virtual ICollection<AtmError> AtmErrors { get; set; }
        public virtual ICollection<ErrTypeId> ErrTypeIds { get; set; }
        public string HasErrors { get; set; }

        public TcpState msc_state { get; set; }//monetic server connection state
    }
}
