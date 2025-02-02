using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("RklAtmProfile")]
    public class RklAtmProfile : Entity<int>
    {
        public int Id { get; set; }
        //public string Key { get; set; }
        public string DecryptKey { get; set; }
        public string SigKey { get; set; }
        public string PublicKeyEncoding { get; set; }
        public string HashIdentifier { get; set; }
        //public string UsePk { get; set; }
        public string PublicKeyAtm { get; set; }
        public string SigKeyAtm { get; set; }
        public int AtmProfileId { get; set; }
    }
}