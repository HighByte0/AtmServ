using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("JobType")]
    public class JobType : Entity<int>
    {
        public string Label { get; set; }


        public virtual ICollection<Job> Jobs { get; set; }

    }
}
