using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("ErrTypeId")]
    public class ErrTypeId : Entity<int>
    {
        public int? State_Id { get; set; }
        [ForeignKey("State_Id")]
        public virtual State State { get; set; }

        public int? Bug_Id { get; set; }
        [ForeignKey("Bug_Id")]
        public virtual Bug Bug { get; set; }

        public int ErrorType_Id { get; set; }

        public ErrTypeId(int errorkindid)
        {
            this.ErrorType_Id = errorkindid;
        }
        public ErrTypeId(int errorkindid, int stateId)
        {
            this.ErrorType_Id = errorkindid;
            this.State_Id = stateId;
        }
        public ErrTypeId()
        {
        }
    }
}