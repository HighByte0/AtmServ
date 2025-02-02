using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{

    [Table("BugHistory")]
    public class BugHistory : Entity<int>
    {

        public int Bug_Id { get; set; }

        [ForeignKey("Bug_Id")]
        public virtual Bug Bug { get; set; }

        public string Changes { get; set; }

        public string UserId { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
