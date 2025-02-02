using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AtmView.Entities
{
    [Table("TmkProcessSteps")]
    public class TmkProcessSteps
    {
        public int Id { get; set; }
        public string StepName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime? Timestamp { get; set; }

        // Foreign Key to link step to summary
        public int TmkProcessSummaryId { get; set; }
        public TmkProcessSummary TmkProcessSummary { get; set; } 
    }
}
