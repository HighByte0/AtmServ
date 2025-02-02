using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("DoneOrderAgency")]
    public class DoneOrderAgency : Entity<int>
    {

        public DateTime DoneOrderAgency_Date { get; set; }
        public int DoneOrderAgencyDelivery_Amount { get; set; }
        public int DoneOrderAgencyCollecte_Amount { get; set; }

        public orderStateAgency DoneOrderStateAgency { get; set; } // 0:OK ; 1: ERROR ; 2: ERROR WITH DEFICIT ; 3: ERROR WITH EXCEDENT ; 4: 

        public int Agency_Id { get; set; }
        [ForeignKey("Agency_Id")]
        public virtual Branch Branch { get; set; }

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
        public int AddedAmountAgency { get; set; }

    }


    public enum orderStateAgency
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
