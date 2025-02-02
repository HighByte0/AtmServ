using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Bug")]
    public class Bug : Entity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        //public int CategoryID { get; set; }//bug nouvelle demande

        //public int PriorityID { get; set; }//1 bloquant , 2 majeur , 3 mineur

        //public int StatusID { get; set; }//1 nouveau , 2 affecte, 3 realisé, 4 cloture

        public string Creator { get; set; }

        public string AssignedUser { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? ResolutionDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<BugAtm> BugAtms { get; set; }

        public virtual ICollection<BugComment> BugComments { get; set; }
        public virtual ICollection<BugComponent> BugComponents { get; set; }
        public virtual ICollection<ErrTypeId> ErrTypeIds__ { get; set; }

        public virtual ICollection<BugHistory> BugHistories { get; set; }
        public virtual ICollection<BugAttachment> BugAttachments { get; set; }

        public int? AtmError_Id { get; set; }

        [ForeignKey("AtmError_Id")]
        public virtual AtmError AtmError { get; set; }

        public int? ActionCorrective_Id { get; set; }

        [ForeignKey("ActionCorrective_Id")]
        public virtual ActionCorrective ActionCorrective { get; set; }


        public int BugCategory_Id { get; set; }

        [ForeignKey("BugCategory_Id")]
        public virtual BugCategory BugCategory { get; set; }


        public int BugPriority_Id { get; set; }

        [ForeignKey("BugPriority_Id")]
        public virtual BugPriority BugPriority { get; set; }


        public int BugStatut_Id { get; set; }

        [ForeignKey("BugStatut_Id")]
        public virtual BugStatut BugStatut { get; set; }
    }
}
