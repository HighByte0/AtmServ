using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    public class PatchAtm : Entity<int>
    {
        public int Id { get; set; }
        public Atm Atm { get; set; }
        [ForeignKey("Atm")]
        public string Atm_Id { get; set; }
        public Patch Patch { get; set; }
        [ForeignKey("Patch")]
        public int? Patch_Id { get; set; }
    }
}
