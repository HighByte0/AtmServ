using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("RCTransactions")]
    public class RCTransactions
    {

        public int FileID { get; set; }
        public string FilePosition { get; set; }//emplacement de la ligne dans le fichier
        public int TCRNbr { get; set; }

        public int TypeID { get; set; }
        [Key]
        [Column(Order = 1)]
        public string AtmID { get; set; }//
        [Key]
        [Column(Order = 2)]
        [DisplayFormat(DataFormatString = "{0:s}",
              ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
        [Key]
        [Column(Order = 3)]
        public int TransactionNumber { get; set; }
        public string CardNumber { get; set; }
        public string AutorisationNumber { get; set; }
        public string Rib { get; set; }

        public int Amount { get; set; }
        public bool isCashPresented { get; set; }

        public bool isCashTaken { get; set; }

        public bool isCashRetracted { get; set; }

        public bool isCashoutError { get; set; }

        public bool ExistInHost { get; set; }

        public bool IsRejected { get; set; }

        public string TrxPictures { get; set; }
        public bool isSuspecious { get; set; }

        public double? Duration { get; set; }
        public string ExtraInfos { get; set; }
        public long TerminalNbr { get; set; }
        public int MerchantID { get; set; }
        public int TerminalID { get; set; }

        public string RefferenceNumber { get; set; }
        public long RemittanceNbr { get; set; }
        public int CardID { get; set; }
        public string AccountNumber { get; set; }
        public int AuthorizationCodeSourceID { get; set; }
        public string AcquirerRefferenceNumber { get; set; }
        public long VoucherNbr { get; set; }
        public long TransactionCurrency { get; set; }
        public double FacturationAmount { get; set; }
        public string DestinationCurrencyCode { get; set; }
        public string DestinationCurrencyExpence { get; set; }
        public string InterchangeFees { get; set; }
        public string InterchangeFeeCurrency { get; set; }
        public string InterchangeeFeeCurrencyExpence { get; set; }
        public string InterchangeFeeSign { get; set; }


        /*
          public int FileID { get; set; }
        public string FilePosition { get; set; }//emplacement de la ligne dans le fichier
        public int TCRNbr { get; set; }

        public int TypeID { get; set; }
        [Key]
        [Column(Order = 1)]
        public string AtmID { get; set; }//
        [Key]
        [Column(Order = 2)]
        [DisplayFormat(DataFormatString = "{0:s}",
              ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public int TransactionNumber { get; set; }
        public string CardNumber { get; set; }
        public string AutorisationNumber { get; set; }
        public string Rib { get; set; }
        public int Amount { get; set; }
        public long TransactionCurrency { get; set; }
        public bool isCashPresented { get; set; }

        public bool isCashTaken { get; set; }

        public bool isCashRetracted { get; set; }

        public bool isCashoutError { get; set; }

        public bool ExistInHost { get; set; }

        public bool IsRejected { get; set; }

        public string TrxPictures { get; set; }
        public bool isSuspecious { get; set; }

        public double? Duration { get; set; }
        public string ExtraInfos { get; set; }
        public long TerminalNbr { get; set; }
        public int MerchantID { get; set; }
        public int TerminalID { get; set; }
        public int CardID { get; set; }
        public string RefferenceNumber { get; set; }
        public long RemittanceNbr { get; set; }
        public string Location { get; set; }
        public string IPAddress { get; set; }
        public string NetworkCode { get; set; }//Visa, Mastercard, American Express
        public string AcquirerID { get; set; }//ID of the acquiring bank or institution
        public string IssuerID { get; set; }//The institution that issued the customer’s card 
        public long ServiceCharge { get; set; }
        public long TaxAmount { get; set; }
        public long CashbackAmount { get; set; }
        public string ChargebackFee { get; set; }
        public DateTime ClearingDate { get; set; }
        public string AuthenticationMethod { get; set; }
        public int AuthorizationCodeSourceID { get; set; }
        public string AcquirerRefferenceNumber { get; set; }
        public long VoucherNbr { get; set; }
        public double FacturationAmount { get; set; }
        public string DestinationCurrencyCode { get; set; }
        public string DestinationCurrencyExpence { get; set; }
        public string InterchangeFees { get; set; }
        public string InterchangeFeeCurrency { get; set; }
        public string InterchangeeFeeCurrencyExpence { get; set; }
        public string InterchangeFeeSign { get; set; }*/






    }
}
