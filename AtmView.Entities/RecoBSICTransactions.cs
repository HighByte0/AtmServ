using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtmView.Entities
{
    [Table("RecoTransactions")]
    public class RecoBSICTransactions : Entite
    {
        public string Id { get; set; }
        //public string Hashcode { get; set; }
        //public int TransactionCode { get; set; }
        public string FileNamePosition { get; set; }
        public int TCRNbr { get; set; }
        //public string Type { get; set; }
        public int TypeID { get; set; }
        public int MerchantId { get; set; }
        public long TerminalNbr { get; set; }
        /*public long MerchantNbr { get; set; }  
        public string MerchantName { get; set; }
        public string MerchantCity { get; set; }
        public long MerchantCountryCode { get; set; }  
        public long MerchantCategoryCode { get; set; }  
        public string MerchantType { get; set; }*/
        public long RemittanceNbr { get; set; }
        public int TerminalID { get; set; }
        public int CardID { get; set; }
        public string CardNumber { get; set; }
        /*public int PaymentProdID { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CardVerificationID { get; set; }
        public string CardCaptureID { get; set; }  */
        public DateTime TransactionDate { get; set; }
        public int AuthCode { get; set; }
        public int AuthCodeSourceID { get; set; }
        public double Amount { get; set; }
        public string REFFNumber { get; set; }
        public string AcquirerREFFNumber { get; set; }
        public string CardAcceptorTerminalID { get; set; }
        public long VoucherNbr { get; set; }
        public long TransactionCurr { get; set; }
        public double FacturationAmount { get; set; }
        public string DestCurrCode { get; set; }
        public string DestCurrExp { get; set; }
        public string InterchangeFees { get; set; }
        public string InterchangeFeeCurr { get; set; }
        public string InterchangeFeeCurrExp { get; set; }
        public string InterchangeFeeSign { get; set; }

        public RecoBSICTransactions()
        {
            Id = Guid.NewGuid().ToString(); // Generate a unique identifier
        }
    }

}
