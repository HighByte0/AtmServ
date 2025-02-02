using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CameraRecordingHistory")]
    public class CameraRecordingHistory : Entity<string>
    {
        [Key]
        public int Id { get; set; } 

        [MaxLength(100)]
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int? Fps { get; set; }
        public string CameraId { get; set; }
        public string Path { get; set; }

        [MaxLength(100)]
        public string Codec { get; set; }
        public bool OperationSuccess { get; set; }
        public double SizeInMB { get; set; }
        public DateTime OperationDate { get; set; }
        public string Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }
        

    }
}