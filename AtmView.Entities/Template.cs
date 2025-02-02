using System.ComponentModel.DataAnnotations;

namespace AtmView.Entities
{
    public class Template
    {
        public int Id { get; set; }
        [Display(Name = "Template")]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Content_fr { get; set; }
        public string SmsContent { get; set; }
        public string Path { get; set; }
        public string Params { get; set; }
        public string Template_pdf { get; set; }


    }
}