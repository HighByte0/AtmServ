using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Matelas")]
    public class Matelas : Entity<int>
    {

        public DateTime Date { get; set; }
        public int MatelasAmount { get; set; }
        public int SoldeFinJourneeAmount { get; set; }
        public int CashPoint_Id { get; set; }
        [ForeignKey("CashPoint_Id")]
        public virtual CashPoint CashPoint { get; set; }

        public int CashStock_Id { get; set; }
        [ForeignKey("CashStock_Id")]
        public virtual CashStock cashStocks { get; set; }

        public int CashStock_Id2 { get; set; }
        [ForeignKey("CashStock_Id2")]
        public virtual CashStock cashStock2 { get; set; }

        public float MatelasPredicted { get; set; }



    }



}
