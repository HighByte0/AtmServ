using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CaisseAgence")]
    public class CaisseAgence : Entity<int>
    {
        [MaxLength(50)]
        public string Name { get; set; }


        [MaxLength(250)]
        public string Description { get; set; }

    }
}
