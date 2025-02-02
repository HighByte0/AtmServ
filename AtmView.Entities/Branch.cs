using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace AtmView.Entities
{
    [Table("Branch")]
    public class Branch : Entity<int>
    {
        [MaxLength(50)]
        public string Name { get; set; }


        [MaxLength(250)]
        public string Description { get; set; }


        //public int Region_Id { get; set; }
        //[ForeignKey("Region_Id")]
        //public virtual Region Region { get; set; }


        public int Address_Id { get; set; }
        [ForeignKey("Address_Id")]
        public virtual Address Address { get; set; }
        [XmlIgnore]
        public virtual ICollection<Order> Orders { get; set; }
        // public virtual IEnumerable<Atm> Atms { get; set; }
        //public virtual List<Atm> Atms { get; set; }
    }
}
