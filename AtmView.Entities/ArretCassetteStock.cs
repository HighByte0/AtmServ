using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("ArretCassetteStock")]
    public class ArretCassetteStock : Entity<int>
    {

        //public int State_Id { get; set; }
        //[ForeignKey("State_Id")]
        //public virtual State State { get; set; }



        public int AtmArretJournee_Id { get; set; }
        [ForeignKey("AtmArretJournee_Id")]
        public virtual AtmArreteJoune AtmArreteJoune { get; set; }

        public string Currency { get; set; }
        public string Type { get; set; }

        public int Rejected { get; set; }
        public int Value { get; set; }
        public int Total { get; set; }
        public string IdCassette { get; set; }
        public string Edition { get; set; }


    }
}
