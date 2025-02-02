using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Component")]
    public class Component : Entity<int>
    {
        public string Label { get; set; }
        public int Priority { get; set; }

        public virtual ICollection<ComponentState> ComponentStates { get; set; }
        public virtual ICollection<BugComponent> BugComponents { get; set; }

    }
}
