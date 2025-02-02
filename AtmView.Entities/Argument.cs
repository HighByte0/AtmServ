namespace AtmView.Entities
{
    public class Argument
    {
        public int Id { get; set; }
        public string ArgName { get; set; }
        public string ArgValue { get; set; }
        public string AlertId { get; set; }
        public Alert Alert { get; set; }
    }
}