using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{

    [Table("AtmRemarque")]
    public class AtmRemarque : Entity<int>
    {
        public string Atm_Id { get; set; }

        public string Remarque { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }
    }
}
