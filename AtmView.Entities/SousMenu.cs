using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    public class SousMenu
    {
        public int Id { get; set; }
        public string Name_Fr { get; set; }
        public string Name { get; set; }
        public string Lien { get; set; }
        public string Class { get; set; }
        public string Script { get; set; }
        public string Controller { get; set; }
        [ForeignKey("Menu_Id")]
        virtual public Menu Menu { get; set; }
        public string Module { get; set; }
        public int Menu_Id { get; set; }
        public bool AdminOnly { get; set; }

        public SousMenu(int id, string name_Fr, string name_En, string lien, string script, Menu menu, int menu_Id)
        {
            Id = id;
            Name_Fr = name_Fr;
            Name = name_En;
            Lien = lien;
            Script = script;
            Menu = menu;
            Menu_Id = menu_Id;
        }

        public SousMenu()
        {
        }
    }
}