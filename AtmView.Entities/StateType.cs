using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("StateType")]
    public class StateType : Entity<int>
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public string CssClass { get; set; }
        public string Color { get; set; }

        public virtual IEnumerable<State> States { get; set; }
    }
}
