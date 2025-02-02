using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Incident")]
    public class Incident : Entity<int>
    {
        public string Numero { get; set; }
        public DateTime? Date { get; set; }
        public string Owner { get; set; }
        public string Assainer { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int State { get; set; }

        public string Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }




    }
}
