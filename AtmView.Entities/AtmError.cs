using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{

    [Table("AtmError")]
    public class AtmError : Entity<int>
    {
        public string Atm_Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public virtual ICollection<ErrTypeId> ErrTypeIds { get; set; }
        public int State_Id { get; set; }

        // public string Description { get; set; }
        public int? ActionCorrective_Id { get; set; }
        //donc on change celui la pour eviter laconfusion ? on le laisse puisque on lui as compris car ca va neccessiter un changement de la base sur le serveur okon teste ?ok
        [ForeignKey("ActionCorrective_Id")]
        public virtual ActionCorrective ActionCorrective { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [ForeignKey("State_Id")]
        public virtual State State { get; set; }

    }
}
