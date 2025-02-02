using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AtmProfilCommon")]
    public class AtmProfilCommon : Entity<int>
    {
        public string Programslist { get; set; }
        //"123": cmd|jp|state
        public string MonitoringServices { get; set; }
        public string JrnSaveDir { get; set; }
        public string TxPicturesDir { get; set; }
        public int CaptureMode { get; set; }
        public int CaptureMtd { get; set; }
        public bool UseProfilXFSVersion { get; set; }
        public string MoneticSerIp { get; set; }
        public int MoneticSerPort { get; set; }
        public string BankName { get; set; }
        public bool EnableSniffing { get; set; }
        public int LaunchWaitTime { get; set; }
        //Days to keep logs
        public int LogKeepTime { get; set; }
    }
}