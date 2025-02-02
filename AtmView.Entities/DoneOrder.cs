using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("DoneOrder")]
    public class DoneOrder : Entity<int>
    {

        public DateTime DoneOrder_Date { get; set; }
        public int DoneOrder_Amount { get; set; }
        public OrderState DoneOrderState { get; set; } // 0:OK ; 1: ERROR ; 2: ERROR WITH DEFICIT ; 3: ERROR WITH EXCEDENT ; 4: 

        public string Atm_Id { get; set; }


        public int? CashPoint_Id { get; set; }
        [ForeignKey("CashPoint_Id")]
        public virtual CashPoint CashPoint { get; set; }

        public int CashProvider_Id { get; set; }
        [ForeignKey("CashProvider_Id")]
        public virtual CashProvider CashProvider { get; set; }

        //public int? CashStock_Id { get; set; }
        //[ForeignKey("CashStock_Id")]
        //public virtual CashStock cashStocks { get; set; }

        public int StateBefore_Id { get; set; }
        [ForeignKey("StateBefore_Id")]
        public virtual State StateBefore { get; set; }

        public int? StateAfter_Id { get; set; }
        [ForeignKey("StateAfter_Id")]
        public virtual State StateAfter { get; set; }

        /// <summary>
        ///  c est la diferrernce des totaux 
        /// </summary>
        public int AddedAmount { get; set; }

    }


    public enum OrderState
    {
        //OK, //0,
        //En_Cours, //1,
        //Error  //2

        OK, //0,
        Error, //1
        ERRORWITHDEFICIT, //2
        ERRORWITHEXCEDENT  //3



    }
}
