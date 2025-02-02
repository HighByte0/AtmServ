using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("ErrorType")]
    public class ErrorType : Entity<int>
    {
        public int errcode { get; set; }
        public string label { get; set; }
        public string descr { get; set; }
        public int? actioncorrectivid { get; set; }
        public bool IsFailure { get; set; } 
    }
}