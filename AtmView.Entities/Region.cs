using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Region")]
    public class Region : Entity<int>
    {

        [MaxLength(50)]
        public string Name { get; set; }


        [MaxLength(250)]
        public string Description { get; set; }


        public int Address_Id { get; set; }
        [ForeignKey("Address_Id")]
        public virtual Address Address { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string abbreviation { get; set; }

        //public virtual IEnumerable<Agence> Agences { get; set; }

    }
}
