using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("RklKeyUse")]
    public class RklKeyUse : Entity<int>
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Use { get; set; }
        public string dwUse { get; set; }
        public int RklProfileId { get; set; }
        [ForeignKey("RklProfileId")]
        public virtual RklAtmProfile RklAtmProfile { get; set; }
    }
}