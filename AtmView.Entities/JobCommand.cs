using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("JobCommand")]
    public class JobCommand : Entite
    {
        [Key]
        public int Id { get; set; }
        //[Key, Column(Order = 0)]
        [Column(Order = 0)]
        public int Job_Id { get; set; }


        //[Key, Column(Order = 1)]
        [Column(Order = 1)]
        public int Command_Id { get; set; }

        [ForeignKey("Command_Id")]
        public virtual Command Command { get; set; }

        [ForeignKey("Job_Id")]
        public virtual Job Job { get; set; }

        public int Order { get; set; }// lorder de la commande dans le job

        public string Agr1 { get; set; }

        public string Agr2 { get; set; }

        public string Agr3 { get; set; }
        public bool CanIgnore { get; set; }

        //public virtual ICollection<Job> Jobs { get; set; }

    }
}
