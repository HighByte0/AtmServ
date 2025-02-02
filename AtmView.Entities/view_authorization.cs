using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("view_authorization")]
    public class view_authorization
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(100)]
        public string TRANACTION_CODE { get; set; }

        [MaxLength(100)]
        public string RETR_REF_NUMB { get; set; }
        [MaxLength(100)]
        public string AUTH_CODE { get; set; }
        [MaxLength(114)]
        public string CARD_NUMB { get; set; }
        [MaxLength(100)]
        public string CARD_EXPI_DATE { get; set; }

        [MaxLength(100)]
        public string CARD_ACCP_TERM_ID { get; set; }

        public DateTime SYST_DATETIME { get; set; }
        public Int64 TRAN_AMOUNT { get; set; }
        public Int64 BILL_AMOUT { get; set; }
        [MaxLength(100)]
        public string TRAN_CURR { get; set; }
        [MaxLength(100)]
        public string DEST_INTE_IDEN { get; set; }
        [MaxLength(100)]
        public string CONFIRM_STATUS { get; set; }
        [MaxLength(100)]
        public string RESP_CODE { get; set; }
        [MaxLength(100)]
        public string REVE_STAT { get; set; }



    }

}


