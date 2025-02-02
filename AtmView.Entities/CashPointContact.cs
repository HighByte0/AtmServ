using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CashPointContact")]
    public class CashPointContact //: Entity<int>
    {
        [Key, Column(Order = 0)]
        public int CashPoint_Id { get; set; }

        [Key, Column(Order = 1)]
        public int Contact_Id { get; set; }

        public int? Ordre { get; set; }

        [ForeignKey("CashPoint_Id")]
        public virtual CashPoint CashPoint { get; set; }

        [ForeignKey("Contact_Id")]
        public virtual Contact Contact { get; set; }
    }
}
