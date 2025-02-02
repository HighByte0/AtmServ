using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BugCategory")]
    public class BugCategory : Entity<int>
    {

        public string Name { get; set; }//bug / nouvelle deamnde




    }
}
