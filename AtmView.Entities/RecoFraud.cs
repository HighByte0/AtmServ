using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("RecoFraud")]
    public class RecoFraud : Entite
    {
        [Key]
        public int TransactionID { get; set; }
        public int TransactionRouteIndicator { get; set; }
        public string ForwardingInstitutionID { get; set; }
        public string ReceivingInstitutionID { get; set; }
        public string CardNumber { get; set; }
        public string ARN { get; set; }
        public DateTime TransactionDate { get; set; }
        public int MerchantID { get; set; }
        public double FraudAmount { get; set; }
        public string FraudCurrCode { get; set; }
        //public DateTime VICProcessingDate { get; set; }
        public string NotificationCode { get; set; }
        public string AccountSequenceNumber { get; set; }
        public int InsuranceYear { get; set; }
        public string FraudType { get; set; }
        //public DateTime ExpiryDate { get; set; }
        public string DebitCreditIndicator { get; set; }
        public string TransactionGenerationMethod { get; set; }
        public string ElectronicCommerceIndicator { get; set; }


    }
}
