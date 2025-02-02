using System;

namespace AtmView.Entities
{
    public class Patch : Entity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
        public string Date { get; set; }
        public string AtmProfile { get; set; }
        public string Upload { get; set; }
    }
}
