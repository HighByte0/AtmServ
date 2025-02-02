using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    public class JobControle : Entity<int>
    {
        [ForeignKey("Job_Id")]
        public virtual Job Job { get; set; }
        public int Job_Id { get; set; }
        public int JobState_Id { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string Atms { get; set; }
        public string RestToExec { get; set; }
        public int nbOfAtms { get; set; }
        public int nbOfFails { get; set; }
        public int nbOfSuccess { get; set; }
        public DateTime LastExecution { get; set; }
        public int NumberOfReexecution { get; set; }
    }
}
