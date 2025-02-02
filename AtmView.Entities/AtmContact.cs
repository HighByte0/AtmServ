using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AtmContact")]
    public class AtmContact //: Entity<int>
    {
        [Key, Column(Order = 0)]
        public string Atm_Id { get; set; }

        [Key, Column(Order = 1)]
        public int Contact_Id { get; set; }

        public int? Ordre { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [ForeignKey("Contact_Id")]
        public virtual Contact Contact { get; set; }
    }
}
