using System.Collections.Generic;

namespace AtmView.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name_Fr { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
        public string Lien { get; set; }
        public int Order { get; set; }
        public virtual ICollection<SousMenu> sousMenus { get; set; }
    }
}