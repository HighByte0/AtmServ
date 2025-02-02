using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BugPriority")]
    public class BugPriority : Entity<int>
    {

        public string Name { get; set; }////1 bloquant , 2 majeur , 3 mineur




    }
}
