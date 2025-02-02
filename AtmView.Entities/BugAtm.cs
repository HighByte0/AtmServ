using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BugAtm")]
    public class BugAtm : Entite
    {

        [Key, Column(Order = 0)]
        public int Bug_Id { get; set; }


        [Key, Column(Order = 1)]
        public string Atm_Id { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [ForeignKey("Bug_Id")]
        public virtual Bug Bug { get; set; }

    }
}
