using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CassetteStock")]
    public class CassetteStock : Entity<int>
    {
        public CassetteStock()
        {
        }

        public CassetteStock(string currency, string idCassette, int presented, int rejected, int start, string stateCassette, string type)
        {
            Currency = currency;
            IdCassette = idCassette;
            Presented = presented;
            Rejected = rejected;
            Start = start;
            StateCassette = stateCassette;
            Type = type;
        }

        public CassetteStock(int cashStock_Id,string type, string currency, int start, int presented, int rejected, string idCassette, string stateCassette, int value, string edition)
        {
            CashStock_Id = cashStock_Id;
            Type = type;
            Currency = currency;
            Start = start;
            Presented = presented;
            Rejected = rejected;
            IdCassette = idCassette;
            StateCassette = stateCassette;
            Value = value;
            Edition = edition;
        }

    


        //public int State_Id { get; set; }
        //[ForeignKey("State_Id")]
        //public virtual State State { get; set; }



        public int CashStock_Id { get; set; }
        [ForeignKey("CashStock_Id")]
        public virtual CashStock CashStock { get; set; }

        public string Currency { get; set; }
        public string Type { get; set; }


        public int Start { get; set; }

        public int Presented { get; set; }

        public int Rejected { get; set; }

        public string IdCassette { get; set; }

        public string StateCassette { get; set; }

        public int Value { get; set; }

        public string Edition { get; set; }


    }
}
