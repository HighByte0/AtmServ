using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BugAttachment")]
    public class BugAttachment : Entity<int>
    {
        public int Bug_Id { get; set; }

        [ForeignKey("Bug_Id")]
        public virtual Bug Bug { get; set; }

        public string UserId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Byte[] Attachment { get; set; }


    }
}
