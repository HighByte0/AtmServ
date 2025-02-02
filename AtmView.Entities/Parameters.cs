using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Parameters")]
    public class Parameters : Entity<int>
    {
        public string name { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public string section { get; set; }
        public string type { get; set; }
        public int? CashProvider_Id { get; set; }
        [ForeignKey("CashProvider_Id")]
        public virtual CashProvider CashProvider { get; set; }
        //public string Atm_Id { get; set; }
        //[ForeignKey("Atm_Id")]
        //public virtual Atm Atm { get; set; }
        public int? CashPoint_Id { get; set; }
        [ForeignKey("CashPoint_Id")]
        public virtual CashPoint CashPoint { get; set; }

    }
}
