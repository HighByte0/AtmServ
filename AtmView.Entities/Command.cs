using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Command")]
    public class Command : Entity<int>
    {
        public string Name { get; set; }
        public string Script { get; set; }
        public string Type { get; set; }
        public bool NeedArguments { get; set; }
        public int TimeOut { get; set; }
        public bool IsJobCmd { get; set; }

        //public string Arg1 { get; set; }
        //public string Arg2 { get; set; }
        //public string Arg3 { get; set; }

        public virtual ICollection<JobCommand> JobCommands { get; set; }

    }
}
