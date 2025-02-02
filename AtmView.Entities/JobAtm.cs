using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("JobAtm")]
    public class JobAtm : Entite
    {

        [Key, Column(Order = 0)]
        public int Job_Id { get; set; }


        [Key, Column(Order = 1)]
        public string Atm_Id { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [ForeignKey("Job_Id")]
        public virtual Job Job { get; set; }


        // public virtual ICollection<Job> Jobs { get; set; }
    }
}
