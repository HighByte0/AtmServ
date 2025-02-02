using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BugComponent")]
    public class BugComponent : Entite
    {

        [Key, Column(Order = 0)]
        public int Bug_Id { get; set; }


        [Key, Column(Order = 1)]
        public int Component_Id { get; set; }

        [ForeignKey("Component_Id")]
        public virtual Component Component { get; set; }

        [ForeignKey("Bug_Id")]
        public virtual Bug Bug { get; set; }

    }
}
