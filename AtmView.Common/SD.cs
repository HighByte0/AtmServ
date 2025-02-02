using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Common
{
    public static class SD
    {
        public const string Proc_AvTrans_Get = "usp_GetAvTransactions";
        public const string Proc_AvTransEvents_Get = "usp_GetTransactionEvents";
        public const string Proc_AvTransErrors_Get = "usp_GetTransactionErrors";
        public const string Proc_AvTransEvents_AVG_Get = "usp_GetAvgTranxEvents";
        public const string Proc_DeleteState = "usp_DeleteState";

        // TASK SISA CERTIF
        public const string CheckObsoletePackages = "CheckObsoletePackages";

        public const string DeleteOldData = "DeleteOldData";
        

        //File Audit Tracker
        public const string FileAuditTracker = "FileAuditTracker";
        public const string FileAuditTracker_dllFilePath = "dllFilePath";
        public const string FileAuditTracker_pdbFilePath = "pdbFilePath";
        public const string FileAuditTracker_FolderPath = "FolderPath";

        // Alerts
        public const string HighRejectRate = "HighRejectRate";

        public const string CashOk = "CashOk";
        public const string CashOkAll = "CashOkAll";
        public const string CashOk_thresholdExaustionValue = "thresholdExaustionValue";
        public const string CashOk_thresholdWarningValue = "thresholdWarningValue";
        public const string CashOk_thresholdOkValue = "thresholdOkValue";
        public const string CashWarning = "CashWarning";
        public const string CashWarningAll = "CashWarningAll";
        public const string CashWarning_thresholdExaustionValue = "thresholdExaustionValue";
        public const string CashWarning_thresholdWarningValue = "thresholdWarningValue";
        public const string CashExhaution = "CashExhaution";
        public const string CashExhautionAll = "CashExhautionAll";
        public const string CashExhaution_thresholdExaustionValue = "thresholdExaustionValue";
        public const string CashExhaution_thresholdWarningValue = "thresholdWarningValue";
        public const string CashExhaution_CreateBug = "CreateBug";

        public const string Inactive = "Inactive";
        public const string Inactive_MaxNbrOfHours = "MaxNbrOfHours";

        public const string InactiveAll = "InactiveAll";
        public const string Inactive_MaxNbrOfHoursAll = "MaxNbrOfHoursAll";
        public const string Inactive_AlertControlCheck = "AlertControlCheck";


        public const string MaintenanceMode = "MaintenanceMode";
        public const string Reconciliation = "Reconciliation";

        public const string AtmIncidentEscalation = "AtmIncidentEscalation";

        public const string Incident = "Incident";
        public const string IncorrectReplenishmentCash = "IncorrectReplenishmentCash";
        public const string InService = "InService";
        public const string InServiceAll = "InServiceAll";
        public const string OutService = "OutService";
        public const string Incident_CreateBugAfter = "CreateBugAfter";
        public const string Incident_CDM = "CDM";
        public const string Incident_IDC = "IDC";
        public const string Incident_EPP = "EPP";
        public const string Incident_JRN = "JRN";
        public const string Incident_RCP = "RCP";
        public const string Incident_SIU = "SIU";
        public const string Incident_TTU = "TTU";

        public const string Incident_GENERICATMERROR = "GENERICATMERROR";
        public const string Incident_CONSOMMABLEERROR = "CONSOMMABLEERROR";
        public const string Incident_CASHERROR = "CASHERROR";
        public const string Incident_HARDWAREERROR = "HARDWAREERROR";
        public const string Incident_OFFLINECOMMUNICATION = "OFFLINECOMMUNICATION";
        public const string Incident_SECURITYISSUE = "SECURITYISSUE";

        public const string OutServiceAll = "OutServiceAll";
        public const string ReplenishmentsOrders = "Replenishments Orders";
        public const string Security = "Security";
        public const string Security_CreateBug = "CreateBug";

        public const string SlowAtms = "SlowAtms";
        public const string SlowAtms_GoodDuration = "GoodDuration";
        public const string SlowAtms_WorstDuration = "WorstDuration";

        // Templates Arguments
        public const string Arg_Atm_Id = "Atm_Id";
        public const string Arg_N_Ecart = "Number_Ecarts";

        public const string Arg_CashStockTotal = "CashStockTotal";
        public const string Arg_Data = "Data";
        public const string Arg_Src = "Src";

    }
}
