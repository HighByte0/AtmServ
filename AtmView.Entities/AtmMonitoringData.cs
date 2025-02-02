using System;
using System.Collections.Generic;

namespace AtmView.Entities
{
    public class AtmMonitoringData
    {
        public string AtmId { get; set; }
        public string AtmName { get; set; }
        public string profile { get; set; }
        public int StateId { get; set; }
        public int StateTypeId { get; set; }

        public string StateLabel { get; set; }
        public string CssClass { get; set; }
        public string Color { get; set; }
        public DateTime? StateDate { get; set; }
        //Modif MDO Pour BPMMAURITANIE
        public DateTime? LastSeen { get; set; }
        public DateTime? LastTransaction { get; set; }
        public DateTime? LastReboot { get; set; }
        public int? AtmErrorId { get; set; }
        public DateTime? ErrStartDate { get; set; }
        public DateTime? ErrEndDate { get; set; }
        public int? ActionCorrectiveId { get; set; }
        public string ActionCorrectiveName { get; set; }
        public string acUserId { get; set; }

        //public string Error { get; set; }
        //public int? IdErrorType { get; set; }
        //public int? IdAtmRemarque { get; set; }
        //public string Remarque { get; set; }

        public List<AtmRemarque> Remarques { get; set; }

        public int? BugId { get; set; }
        public string EtatErrorIncident { get; set; }
        public List<ComponentState> ComponentStates { get; set; }
        public List<CameraConfig> CamerasConfig { get; set; }
        public CameraRecordingHistory LastTransactionRecord { get; set; }
    }
}
