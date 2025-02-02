using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{

    [Table("NodeType")]
    public class NodeType : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
