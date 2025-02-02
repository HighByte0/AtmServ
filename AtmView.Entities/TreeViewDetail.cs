using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("TreeViewDetail")]
    public class TreeViewDetail : Entity<int>
    {
        public string ParentId { get; set; }
        public string FilsId { get; set; }
        public int ParentType { get; set; }
        public int FilsType { get; set; }
    }
}
