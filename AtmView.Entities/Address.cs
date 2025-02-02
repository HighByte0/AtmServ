using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Address")]
    public class Address : Entity<int>
    {
        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        //public virtual IEnumerable<Branch> Branchs { get; set; }
        //public virtual IEnumerable<Region> Regions { get; set; }
        //public virtual IEnumerable<Atm> Atms { get; set; }


        public virtual List<Branch> Branchs { get; set; }
        public virtual List<Region> Regions { get; set; }
        public virtual List<Atm> Atms { get; set; }
    }
}
