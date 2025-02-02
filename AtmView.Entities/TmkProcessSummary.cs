using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AtmView.Entities
{
    [Table("TmkProcessSummary")]
    public class TmkProcessSummary
    {
        public int Id { get; set; }
        public string AtmId { get; set; }
        public int? Success { get; set; }
        public int? CmdctrlTMKRSAId { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation property for related steps
        public ICollection<TmkProcessSteps> TmkProcessStepss { get; set; } 

    }

}
