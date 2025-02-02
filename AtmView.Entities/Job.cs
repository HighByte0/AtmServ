using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Job")]
    public class Job : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }//apres cette date le job ne s execute pas
        public int? RetryInterval { get; set; } //si le job echou alors a quel interval il faut le re executer : en minutes
        public int? RetryTimes { get; set; }////si le job echou alors combien de fois il faut le re executer 
        public DateTime? FirstStartDate { get; set; }//la 1re date ou  le job a ete execute
        public int? Frequence { get; set; }//daily, monthly weekly.... enumeration
        public bool? IsFinished { get; set; }

        public int JobType_Id { get; set; }  // JobType immediat /periodique/planifie : planifie??

        [ForeignKey("JobType_Id")]
        public virtual JobType JobType { get; set; }

        public string StartHour { get; set; }// example 12:30 ce champs a remplir quelque soit la frequence/type
        public int? DayOfWeek { get; set; } //1,2...7 ce champs a remplir qi la frequece est weekly
        public int? DayOfMonth { get; set; } //1,2...31 ce champs a remplir qi la frequece est monthly

        public virtual ICollection<JobAtm> JobAtms { get; set; }
        public virtual ICollection<JobCommand> JobCommands { get; set; }
        public virtual ICollection<JobAtmExecutionResult> JobAtmExecutionResults { get; set; }
        //public virtual ICollection<JobCommandExecutionResult> JobCommandExecutionResults { get; set; }

        //ajouter un bool Is Active= false : si un utilisateur cree un job imediat et la valeur isactive= false alors le job ne s execute jusque ce que isactive = true
        public bool IsActive { get; set; }

        //si le job est planifie'scheduled alors on doit remplir le champs  ScheduledDate et StartHour aussi
        public DateTime? ScheduledDate { get; set; }

        public bool Deleted { get; set; }

        //liste des commandes associées: crer une table jobcommande( jobid commande id, rank=> l ordr de la commande dans le job, arg1, arg2, arg3)

        //Creer une table JobCommandExecution Qui contient le resultat que chaque commande executer d un job avec les
        //colonnes id, jobid, commandeId, executionDate, atmId, executiondate, output



    }
}
