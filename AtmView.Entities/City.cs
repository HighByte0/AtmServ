using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("City")]
    public class City : Entity<int>
    {
        [MaxLength(50)]
        public string Name { get; set; }


        [MaxLength(250)]
        public string Description { get; set; }

    }
}
