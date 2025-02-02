namespace AtmView.Entities
{
    public class RoleSousMenu
    {
        public int Id { get; set; }
        public string IdRole { get; set; }
        public string IdSousMenu { get; set; }

        public RoleSousMenu()
        {
        }

        public RoleSousMenu(int id, string idRole, string idSousMenu)
        {
            Id = id;
            IdRole = idRole;
            IdSousMenu = idSousMenu;
        }

        public RoleSousMenu(string idRole, string idSousMenu)
        {
            IdRole = idRole;
            IdSousMenu = idSousMenu;
        }
        public RoleSousMenu(RoleSousMenu roleSousMenu)
        {
            Id = roleSousMenu.Id;
            IdRole = roleSousMenu.IdRole;
            IdSousMenu = roleSousMenu.IdSousMenu;
        }
    }
}