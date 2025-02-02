using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("EncaisseMax")]
    public class EncaisseMax : Entity<int>
    {


        public int Agency_Id { get; set; }
        [ForeignKey("Agency_Id")]
        public virtual Branch Branch { get; set; }

        public Int64? EncaisseMaximum { get; set; }
        public Int64? DefinedEncaisseMaximum { get; set; }
        public DateTime? Date { get; set; }


    }



}
