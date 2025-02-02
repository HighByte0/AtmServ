using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("RecoMerchant")]
    public class RecoMerchant : Entite
    {
        [Key]
        public int Id { get; set; }
        public long MerchantNbr { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCity { get; set; }
        public long MerchantCountryCode { get; set; }
        public long MerchantCategoryCode { get; set; }
        public string MerchantType { get; set; }
        public long MerchantProvinceCode { get; set; }
    }
}
