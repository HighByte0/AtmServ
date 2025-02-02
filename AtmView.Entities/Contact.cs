using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Contact")]
    public class Contact : Entity<int>
    {

        [MaxLength(100)]
        public string Position { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string Company { get; set; }

        [MaxLength(100)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Function { get; set; }

        public virtual IEnumerable<AtmContact> AtmContacts { get; set; }//ICollection


        //public string AtmId { get; set; }
        //[ForeignKey("AtmId")]
        //public virtual Atm Atm { get; set; }

    }
}
