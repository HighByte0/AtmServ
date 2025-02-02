using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AtmRejectStatut")]
    public class AtmRejectStatut : Entity<int>
    {
        [MaxLength(250)]
        public string Label { get; set; }

        [MaxLength(250)]
        public string Label_fr { get; set; }
        public int StatutId { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(50)]
        public string Highlight { get; set; }

        public bool Failure { get; set; }




    }
}
