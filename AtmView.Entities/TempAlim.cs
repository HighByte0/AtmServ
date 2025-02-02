using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("TempAlim")]
    public class TempAlim : Entity<int>
    {

        //[Key]
        //public int TempAlim_Id { get; set; }
        public DateTime EntryTime { get; set; }
        public String Data { get; set; }
        public String Filename { get; set; }
        public int EntryType { get; set; }
        public string Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }
    }
}
