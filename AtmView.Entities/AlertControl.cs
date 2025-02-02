using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    public class AlertControl: Entity<int>
    {
        public string AlertId { get; set; }
        public string Atms { get; set; }
        public int JobCtrlId { get; set; }
        public virtual JobControle JobCtrl { get; set; }
        public DateTime ExecutionDate { get; set; }
        public bool SmsSended { get; set; }
        public bool EmailSended { get; set; }
        public string SendEmailOutput { get; set; }
        public string SendSmsOutput { get; set; }
    }
}
