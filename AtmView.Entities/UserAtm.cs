using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("UserAtm")]
    public class UserAtm : Entite
    {

        [Key, Column(Order = 0)]
        public string User_Id { get; set; }


        [Key, Column(Order = 1)]
        public string Atm_Id { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }


        //[ForeignKey("User_Id")]
        //public virtual IdentityUser User { get; set; }
    }
}
