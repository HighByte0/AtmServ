using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("JournalEntry")]
    public class JournalEntry : Entite// Entity<int>
    {
        [Key, Column(Order = 1)]
        public string Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [Key, Column(Order = 2)]
        public DateTime EntryTime { get; set; }


        public String Data { get; set; }


        public String Filename { get; set; }
        public int EntryType { get; set; }

    }
}
