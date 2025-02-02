using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    public class Alert
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public bool Etat { get; set; }
        //Module 23/09/19
        public string Module { get; set; }

        public ICollection<Argument> Arguments { get; set; }
        public Template Template { get; set; }
        [Display(Name = "Template")]
        [ForeignKey("Template")]
        public int Template_Id { get; set; }
        public bool RiseOnStateChanged { get; set; }
        public int RiseInterval { get; set; }
        public int RenotifyInterval { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSms { get; set; }
        public int? JobId { get; set; }
        public string Parameters { get; set; }

        public int EmailTo { get; set; }
        public int EmailCc { get; set; }
        public int PhoneTo { get; set; }
        public string Emails { get; set; }
        public string CcEmails { get; set; }
        public string Phones { get; set; }

        public Template RaiseAlert()
        {
            if (Etat)
            {
                Template.Content = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + Template.Path);
                //foreach(var item in Arguments)
                //{
                //    Template.Content = Template.Content.Replace(item.ArgName,item.ArgValue);
                //}
            }
            return Template;
        }
    }
}