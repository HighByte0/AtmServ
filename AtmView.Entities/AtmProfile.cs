﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AtmProfile")]
    public class AtmProfile : Entity<int>
    {
        //[Key]
        public string AtmCons { get; set; }
        //------------------------
        public string CdmServName { get; set; }
        public string IdcServName { get; set; }
        public string JrnServName { get; set; }
        public string PinServName { get; set; }
        public string RcpServName { get; set; }
        public string SiuServName { get; set; }
        public string TtuServName { get; set; }
        //------------------------
        public string JrnlDir { get; set; }
        public string RebootCmd { get; set; }
        public string GoInServCmd { get; set; }
        public string GooutServCmd { get; set; }
        public string AttachmentDir { get; set; }
        public string InServPhrase { get; set; }
        public string OutServPhrase { get; set; }
        //------------------------
        public string ErrAttachFile { get; set; }
        public int xfs_version { get; set; }

        //ASbr MRhz - Add new columns
        public string Programlist { get; set; }
        public string MonitoringServices { get; set; }
        public string UseProfilXFSVersion { get; set; }
        public int CaptureMode { get; set; }
        public int CaptureMtd { get; set; }
        public string JrnSaveDir { get; set; }
        public string JrnlBackupDir { get; set; }
        
        public string TrxPicturesDir { get; set; }
        public string MoneticSerIp { get; set; }
        public string MoneticSerPort { get; set; }
        public string InactiveChkEnabled { get; set; }
        public string InactiveHours { get; set; }
        //lkh 
        public string JrnNameFilter { get; set; }
        public int launchwaittime { get; set; }
    }
}