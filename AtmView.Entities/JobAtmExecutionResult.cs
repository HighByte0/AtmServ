using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("JobAtmExecutionResult")]
    public class JobAtmExecutionResult : Entity<int>
    {

        public int Job_Id { get; set; }



        public string Atm_Id { get; set; }



        public DateTime ExecutionDate { get; set; }

        public string ExecutionHour { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [ForeignKey("Job_Id")]
        public virtual Job Job { get; set; }

        public int ExecutionNumber { get; set; }

        public string Result { get; set; }  //  ko/ok
        public string Output { get; set; }

        [ForeignKey("JobControle_Id")]
        public virtual JobControle JobControle { get; set; }
        public int JobControle_Id { get; set; }

        //public virtual ICollection<Job> Jobs { get; set; }
    }
}
