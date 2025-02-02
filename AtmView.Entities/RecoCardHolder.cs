using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("RecoCardHolder")]
    public class RecoCardHolder
    {
        public int Id { get; set; }
        public int CardID { get; set; }
        public int PaymentProdID { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CardVerificationID { get; set; }
        public string CardCaptureID { get; set; }
    }

}
