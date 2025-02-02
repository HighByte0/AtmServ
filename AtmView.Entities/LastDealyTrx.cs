using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace AtmView.Entities
{
    [Table("LastDealyTrx")]

    public class LastDealyTrx : Entite, IEquatable<LastDealyTrx>

    {

        [Key]
        [Column(Order = 1)]
        public String AtmID { get; set; }
        [Key]
        [Column(Order = 2)]
        [DisplayFormat(DataFormatString = "{0:s}",
               ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
        public String AutorisationNumber { get; set; }
        [Key]
        [Column(Order = 3)]
        public int TransactionNumber { get; set; }
        public String CardNumber { get; set; }
        public String Rib { get; set; }
        public String Type { get; set; }
        public int Amount { get; set; }
        public int Statut { get; set; }
        public bool isCashPresented { get; set; }
        public bool isCashTaken { get; set; }
        public bool isCashRetracted { get; set; }
        public bool isCashoutError { get; set; }
        public bool ExistInHost { get; set; }
        public bool IsRejected { get; set; }
        //public Int64? SoldeOuverture { get; set; }
        //public Int64? SoldeCloture{ get; set; }
        public String TrxPictures { get; set; }
        public double? Duration { get; set; }
        public string ExtraInfos { get; set; }
        public bool is_FraudP { get; set; }
        public string FraudReason { get; set; }
        public bool Equals(LastDealyTrx other)
        {
            if (other == null)
                return false;

            if (this.AtmID == other.AtmID && this.TransactionNumber == other.TransactionNumber && this.TransactionDate.Date == other.TransactionDate.Date)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            LastDealyTrx trxObj = obj as LastDealyTrx;
            if (trxObj == null)
                return false;
            else
                return Equals(trxObj);
        }


        public static bool operator ==(LastDealyTrx trx1, LastDealyTrx trx2)
        {
            if (((object)trx1) == null || ((object)trx2) == null)
                return Object.Equals(trx1, trx2);

            return trx1.Equals(trx2);
        }

        public static bool operator !=(LastDealyTrx trx1, LastDealyTrx trx2)
        {
            if (((object)trx1) == null || ((object)trx2) == null)
                return !Object.Equals(trx1, trx2);

            return !(trx1.Equals(trx2));
        }
        public int GetHashCode()
        {
            return this == null ? 0 : (this.AtmID.GetHashCode() ^ this.TransactionNumber.GetHashCode());
        }



    }
}
