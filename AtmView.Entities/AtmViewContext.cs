using AtmView.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Web.Util;

namespace AtmView.Entities
{

    public class AtmViewContext : DbContext
    {

        public AtmViewContext()
#if DEBUG
            : base("Name=AVAtmViewContextD")
#else
            : base("Name=AVAtmViewContextR")
#endif
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //builder.Entity<AutorisationProfilDonnee>()..WillCascadeOnDelete(false);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        //tmk
        public DbSet<TmkProcessSteps> TmkProcessStepss { get; set; }
        public DbSet<TmkProcessSummary> TmkProcessSummaries { get; set; }
        //password History
        public DbSet<PasswordHistory> PasswordHistories { get; set; }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<Atm> Atms { get; set; }
        public DbSet<CameraRecordingHistory> CameraRecordingHistory { get; set; }

        public DbSet<AtmView.Entities.AtmProfile> AtmsProfiles { get; set; }
        public DbSet<AtmView.Entities.ErrorType> AtmErrTypes { get; set; }
        public DbSet<AtmView.Entities.ErrTypeId> ErrTypeIds { get; set; }
        public DbSet<AtmView.Entities.AtmProfilCommon> mProfilCommon { get; set; }
        public DbSet<RklAtmProfile> RklAtmProfile { get; set; }
        public DbSet<RklKeyUse> RklKeyUse { get; set; }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<NodeType> NodeTypes { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<TreeViewDetail> TreeViewDetails { get; set; }
        public DbSet<ContextualMenu> ContextualMenus { get; set; }
        public DbSet<AtmContact> AtmContacts { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<StateType> StateTypes { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentState> ComponentStates { get; set; }
        public DbSet<StateFieldInt> StateFieldsInt { get; set; }
        public DbSet<StateFieldStr> StateFieldsStr { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<CassetteStock> CassetteStocks { get; set; }
        public DbSet<JournalEntry> JournalEntrys { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CashStock> CashStocks { get; set; }
        public DbSet<UserAtm> UserAtms { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<CommandControl> CommandControls { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAtm> JobAtms { get; set; }
        public DbSet<JobCommand> JobCommands { get; set; }
        public DbSet<JobCommandExecutionResult> JobCommandExecutionResults { get; set; }
        public DbSet<StateJob> StateJobs { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<JobAtmExecutionResult> JobAtmExecutionResults { get; set; }
        public DbSet<JobControle> JobControles { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<BugComment> BugComments { get; set; }
        public DbSet<BugHistory> BugHistories { get; set; }
        public DbSet<ActionCorrective> ActionCorrectives { get; set; }
        public DbSet<BugComponent> BugComponents { get; set; }

        public DbSet<BugCategory> BugCategories { get; set; }
        public DbSet<BugPriority> BugPriorities { get; set; }
        public DbSet<BugStatut> BugStatuts { get; set; }

        public DbSet<JournalEntryMontly> JournalEntryMontlys { get; set; }

        public DbSet<BugAttachment> BugAttachments { get; set; }

        public DbSet<BugAtm> BugAtms { get; set; }
        public DbSet<AtmError> AtmErrors { get; set; }
        public DbSet<AVTransaction> AVTransactions { get; set; }
        public DbSet<TransactionEcart> TransactionEcarts { get; set; }

        public DbSet<AVAtmConfig> AVAtmConfigs { get; set; }

        public DbSet<AtmRemarque> AtmRemarques { get; set; }

        public DbSet<UserSessionInfo> UserSessionInfos { get; set; }

        //Inventory
        public DbSet<Im_Atm_Inventory> Im_Atm_Inventorys { get; set; }
        public DbSet<Im_Computer_Inventory> Im_Computer_Inventorys { get; set; }
        public DbSet<Im_Move_Inventory> Im_Move_Inventorys { get; set; }

        //CASH Management
        public DbSet<DoneOrder> DoneOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CashProvider> CashProviders { get; set; }
        public DbSet<Parameters> Parameters { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<Predictor> Predictors { get; set; }
        public DbSet<TempAlim> TempAlims { get; set; }
        public DbSet<Holyday> Holydays { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<RecentAtmState> RecentAtmStates { get; set; }
        public DbSet<TransationType> TransationTypes { get; set; }
        //FROM AMINA
        public DbSet<LastDealyTrx> LastDealyTrxs { get; set; }
        public DbSet<DoneOrderAgency> DoneOrderAgencys { get; set; }
        public DbSet<FactorAgency> FactorAgencys { get; set; }
        public DbSet<PredictorAgency> PredictorAgencys { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<FactorDepositrAgency> FactorDepositAgencys { get; set; }
        public DbSet<PredictorDepositAgency> PredictorDepositAgencys { get; set; }
        public DbSet<EncaisseMax> EncaisseMaxs { get; set; }
        public DbSet<AVTransactionAgency> AVTransactionAgencys { get; set; }
        public DbSet<CashPoint> CashPoints { get; set; }
        public DbSet<CashPointProfile> CashPointProfiles { get; set; }
        public DbSet<Matelas> Matelas { get; set; }
        public DbSet<CashPointContact> CashPointContact { get; set; }
        public DbSet<AtmRejectStatut> AtmRejectStatuts { get; set; }
        public DbSet<CaisseAgence> CaisseAgences { get; set; }
        public DbSet<OrderCategory> OrderCategories { get; set; }

        //Cash alerts:
        public DbSet<AtmCashAlert> AtmCashAlerts { get; set; }
        public DbSet<AtmCashAlertExhaution> AtmCashAlertExhautions { get; set; }
        public DbSet<AtmCashAlertWarning> AtmCashAlertWarnings { get; set; }
        // Atm Deconnection:
        public DbSet<AtmCommError> AtmCommError { get; set; }
        public DbSet<AtmMaintenanceMode> AtmMaintenanceMode { get; set; }

        //Cash Ayoub:
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SousMenu> SousMenus { get; set; }
        public DbSet<RoleSousMenu> RoleSousMenus { get; set; }
        //Cash Alerts Ayoub:
        public DbSet<Template> Templates { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AlertControl> AlertControls { get; set; }
        public DbSet<Argument> Arguments { get; set; }
        public DbSet<Patch> Patches { get; set; }
        public DbSet<PatchAtm> PatchAtm { get; set; }

        //invoice
        public System.Data.Entity.DbSet<Client> Clients { get; set; }
        public System.Data.Entity.DbSet<Invoice> Invoices { get; set; }
        public System.Data.Entity.DbSet<InvoiceItem> InvoiceItems { get; set; }

        public System.Data.Entity.DbSet<OrderType> OrderTypes { get; set; }
        //Dashbord
        public System.Data.Entity.DbSet<ArretJourne> ArretJournes { get; set; }
        public System.Data.Entity.DbSet<Reservation> Reservations { get; set; }
        //
        public System.Data.Entity.DbSet<ConfigCassette> ConfigCassettes { get; set; }

        public System.Data.Entity.DbSet<CassetteSetup> CassetteSetup { get; set; }


        //Mdfs Amina For Atm End Date

        public DbSet<ArretCassetteStock> ArretCassetteStocks { get; set; }
        public DbSet<AtmArreteJoune> AtmArreteJounes { get; set; }
        public DbSet<Pr_EventsErrorType> Pr_EventsErrorTypes { get; set; }
        public DbSet<Pr_EventsType> Pr_EventsTypes { get; set; }
        public DbSet<Pr_TransactionEvents> Pr_TransactionEvents { get; set; }

        public DbSet<Pr_TransactionError> Pr_TransactionErrors { get; set; }
        public DbSet<BinConfiguration> BinConfiguration { get; set; }
        public DbSet<Pr_LastDailyTransactionError> Pr_LastDailyTransactionError { get; set; }
        public DbSet<Pr_DailyTransactionEvents> Pr_LastDailyTransactionEvents { get; set; }
        //Payway Table for Coris
        public DbSet<view_authorization> view_authorizations { get; set; }
        // tables temporaire de sauvegarde des authorizations pour la réconciliation

        public DbSet<FrequentCommand> FrequentCommands { get; set; }
        public DbSet<Package> Packages { get; set; }

        // Table Added to associate Nodes and Bins
        //public DbSet<BinsAssociation> BinsAssociations { get; set; }
        public DbSet<ONLINE_AUTHORIZATION> ONLINE_AUTHORIZATIONS { get; set; }
        //public DbSet<FPTrainingModel> FPTrainingData { get; set; }
        public DbSet<FPTrainingModel> FPTrainingData { get; set; }

        public DbSet<FPState> FPStates { get; set; }
        public DbSet<BinCategory> BinCategory { get; set; }


        public DbSet<RecoBSICTransactions> RecoBSICTransactions { get; set; }
        //public DbSet<FileHashcode> FileHashcodes { get; set; }
        public DbSet<RecoTransactionType> RecoTransactionTypes { get; set; }
        public DbSet<RecoMerchant> RecoMerchants { get; set; }
        public DbSet<RecoCardHolder> RecoCardHolders { get; set; }
        //public DbSet<RecoFraud> RecoFraud { get; set; }

        public DbSet<RCTransactions> RCTransactions { get; set; }
        public DbSet<RecoFileInfo> RecoFileInfos { get; set; }
        public DbSet<RecoParams> RecoParams { get; set; }
        public DbSet<RecoParams_TransactionType> RecoParams_TransactionType { get; set; }
        //public DbSet<TransactionType> TransactionTypes { get; set; }



        // public DbSet<BinCategory> BinCategory { get; set; }


        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();

        }


        //public void Refresh(object overwriteCurrentValues, RecentAtmState recentatmState)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
