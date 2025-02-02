using AtmStateLib;
using AtmView.Entities;
using AtmView.Hubs;
using AtmView.Models.State;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Web.Services;
using Unity.Services;
using log4net;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Net.NetworkInformation;
using AtmView.Monitoring;
using AtmView.Common;
using AtmView.Services.Utils;

//using NameValuePair = System.Collections.Generic.KeyValuePair<string, string>;
namespace AtmView.Services
{
    /// <summary>
    /// Description résumée de AVAtmInsertState
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class AVAtmInsertState : BaseService<AVAtmInsertState> //System.Web.Services.WebService
    {

        protected readonly ILog ExceptionLogger = LogManager.GetLogger("ExceptionLogger");
        protected readonly ILog TraceLogger = LogManager.GetLogger("TraceLogger");
        public AVAtmInsertState()
            : base()
        {
        }



        [Dependency]
        public IStateService _StateService
        {
            get;
            set;
        }

        [Dependency]
        public IOrderService _OrderService
        {
            get;
            set;
        }

        [Dependency]
        public IDoneOrderService _DoneOrderService
        {
            get;
            set;
        }

        [Dependency]
        public IAtmErrorService _AtmErrorService
        {
            get;
            set;
        }
        [Dependency]
        public IActionCorrectiveService _ActionCorrectiveService
        {
            get;
            set;
        }

        [Dependency]
        public IRecentAtmStateService _RecentAtmStateService
        {
            get;
            set;
        }

        [Dependency]
        public ParametersService _ParametersService
        {
            get;
            set;
        }

        private AtmView.Entities.AtmViewContext db2 = new AtmView.Entities.AtmViewContext();
#if true//trace_cash
        private static object trace_cash_obj = new object();
#endif//trace_cash

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        [WebMethod]
        public void InsertState(StateModel stateModel)
        {
            try
            {

                //stateModel
                //mapping entre l objet stateModel et state

                if (stateModel != null)
                {
                    List<ComponentState> cptstes = new List<ComponentState>();

                    List<CashStock> CashStocksList = new List<CashStock>();
                    if (stateModel.CashStocks != null)
                    {
                        foreach (var elt in stateModel.CashStocks)
                        {
                            List<CassetteStock> CassetteStocksList = new List<CassetteStock>();
                            if (elt.CassetteStocks != null)
                            {
                                foreach (var cassette in elt.CassetteStocks)
                                {
                                    CassetteStocksList.Add(new
                                    CassetteStock
                                    {
                                        IdCassette = cassette.IdCassette,
                                        Currency = cassette.Currency,
                                        Type = cassette.Type,
                                        Start = cassette.Start,
                                        Presented = cassette.Presented,
                                        Rejected = cassette.Rejected,
                                        StateCassette = cassette.StateCassette
                                    }
                                    );
                                }
                            }

                            CashStocksList.Add
                                (
                                new CashStock
                                {
                                    Retract = elt.Retract,
                                    Total = elt.Total,
                                    Treshold = elt.Treshold,
                                    CassetteStocks = CassetteStocksList
                                }
                                );

                        }

                    }
                    if (stateModel.ComponentStates != null)
                    {

                        foreach (var elt in stateModel.ComponentStates)
                        {
                            cptstes.Add(new ComponentState
                            {
                                Component_Id = elt.Component_Id,
                                Description = elt.Description,
                                LastDate = elt.LastDate,
                                StateComponent_Id = elt.StateComponent_Id

                            });
                        }
                    }

                    State state = new State
                    {
                        Atm_Id = stateModel.Atm_Id,
                        //StateType = new StateType { Id = stateModel.StateType_Id },
                        StateType_Id = stateModel.StateType.Id,
                        StateDate = stateModel.StateDate,
                        LastTransaction = stateModel.LastTransaction,
                        ComponentStates = cptstes,
                        CashStocks = CashStocksList,


                    };

                    _StateService.Create(state);

                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.Error(ex.Message);
                ExceptionLogger.Error(ex.StackTrace);
            }

        }

        [WebMethod]
        public string SystemRebooting(string atmID, DateTime _date)
        {
            return "Got it";
        }

        [WebMethod]
        public StateResult UploadState(string atmID, DateTime _statedate, short atm_checkmode, short atm_jrnlmode, byte[] state_bytes, short _msstate, int insert_counter_)
        {
            StateResult result = null;
            var handleStateTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    var msc_state = Enum.IsDefined(typeof(TcpState), (int)_msstate) ? (TcpState)_msstate : TcpState.Unknown;

                    TraceLogger.Error("State Date" + _statedate);
                    if(_statedate == null || _statedate.AddDays(10) > DateTime.Now)
                    {
                        _statedate = DateTime.Now;
                    }
                    ExceptionLogger.Error("appele handlestate");

                    result = HandleState(atmID, DateTime.Now, atm_checkmode, atm_jrnlmode, state_bytes, msc_state, insert_counter_);
                }
                catch (Exception ex)
                {
                    result = new StateResult();
                    result.excepocurred = true;
                    result.exmessage = ex.Message;
                }
            });
            handleStateTask.Wait();
            return result;
        }
        private StateResult HandleState(string atmID, DateTime _statedate, short atm_checkmode, short atm_jrnlmode, byte[] state_bytes, TcpState _msstate, int insert_counter_)
        {
            int buff_size = state_bytes.Length;
            var state = new AtmStateLib.WFSATM();
            int readofs = 0;
            var result = new StateResult();
            ExceptionLogger.Error("handlestate buff_size");
            if (buff_size > 0)
            {
                ExceptionLogger.Error("handlestate buff_size ok");

                result.logmsg += Environment.NewLine + "ReadWFSStateData() {";
                //readofs = AtmState2.ReadWFSStateData2(ref state, state_bytes);
                var buffer_ptr = Marshal.AllocHGlobal(buff_size);
                Marshal.Copy(state_bytes, 0, buffer_ptr, buff_size);
                readofs = AtmState.ReadWFSStateData(ref state, buffer_ptr, buff_size);

                System.Diagnostics.Debug.Assert((readofs == buff_size));
                Console_Writeline("xfs state size: " + buff_size);
                Marshal.FreeHGlobal(buffer_ptr);
                result.logmsg += Environment.NewLine + "ReadWFSStateData() }";

                try
                {
                    Console_Writeline("AtmInsertState()");
                    //Console_Writeline("AtmInsertState() "+state);
                    Entities.Atm atm = db2.Atms.Where(a => a.Id == atmID).SingleOrDefault();
#if true
                    UInt32 active_services = 0x0;
                    var dbprofil = db2.AtmsProfiles.Where(x => (x.AtmCons == atm.Profile)).FirstOrDefault();
                    if ((!string.IsNullOrEmpty(dbprofil.CdmServName)) && (!dbprofil.CdmServName.Equals("NA", StringComparison.InvariantCultureIgnoreCase))) { active_services |= (CpnFlag.CDM | CpnFlag.CASH); }
                    if ((!string.IsNullOrEmpty(dbprofil.IdcServName)) && (!dbprofil.IdcServName.Equals("NA", StringComparison.InvariantCultureIgnoreCase))) { active_services |= CpnFlag.IDC; }
                    if ((!string.IsNullOrEmpty(dbprofil.JrnServName)) && (!dbprofil.JrnServName.Equals("NA", StringComparison.InvariantCultureIgnoreCase))) { active_services |= CpnFlag.JRN; }
                    if ((!string.IsNullOrEmpty(dbprofil.PinServName)) && (!dbprofil.PinServName.Equals("NA", StringComparison.InvariantCultureIgnoreCase))) { active_services |= CpnFlag.PIN; }
                    if ((!string.IsNullOrEmpty(dbprofil.RcpServName)) && (!dbprofil.RcpServName.Equals("NA", StringComparison.InvariantCultureIgnoreCase))) { active_services |= CpnFlag.RCP; }
                    if ((!string.IsNullOrEmpty(dbprofil.SiuServName)) && (!dbprofil.SiuServName.Equals("NA", StringComparison.InvariantCultureIgnoreCase))) { active_services |= CpnFlag.SIU; }
                    if ((!string.IsNullOrEmpty(dbprofil.TtuServName)) && (!dbprofil.TtuServName.Equals("NA", StringComparison.InvariantCultureIgnoreCase))) { active_services |= CpnFlag.TTU; }
                    //UInt32 na_services = ~active_services;
#endif
#if true//components
                    List<Entities.ComponentState/*Model*/> cptstes = new List<Entities.ComponentState/*Model*/>();
                    //DateTime statedate = DateTime.Now;
                    // component CDM

                    UInt32 all_is_ok = 0x0;
                    Entities.ComponentState/*Model*/ CDM = new Entities.ComponentState/*Model*/();
                    CDM.Component_Id = 1;
                    if (CpnFlag.CDM == (active_services & CpnFlag.CDM))
                    {
                        CDM.LastDate = _statedate;
                        CDM.IntFields = new System.Collections.ObjectModel.Collection<StateFieldInt> { };
                        CDM.StrFields = new System.Collections.ObjectModel.Collection<StateFieldStr> { };

                        CDM.IntFields.Add(new StateFieldInt("cdm.status", state.cdm.status));

                        if (state.cdm.status == /*0*/iopcts.XFSAPIh.WFS_SUCCESS)
                        {
                            if ((CDMLabels.Device[state.cdm.fwDevice].type == FState.ok)
                                                    && ((CDMLabels.Dispenser[state.cdm.fwDispenserstate].type == FState.ok) || (CDMLabels.Dispenser[state.cdm.fwDispenserstate].type == FState.warning))
                                                    && (CDMLabels.IntermediateStacker[state.cdm.fwintermediateStacker].type == FState.ok)
                                                    )
                            //if (((state.cdm.fwDevice == /*0*/iopcts.XFSCDMh.WFS_CDM_DEVONLINE) || (state.cdm.fwDevice == iopcts.XFSCDMh.WFS_CDM_DEVBUSY))
                            //&& state.cdm.fwDispenserstate == /*0*/iopcts.XFSCDMh.WFS_CDM_DISPOK
                            //&& state.cdm.fwintermediateStacker == /*0*/iopcts.XFSCDMh.WFS_CDM_ISEMPTY)
                            {
                                CDM.Description = " Cash Dispenser OK";
                                CDM.StateComponent_Id = 1;
                                all_is_ok |= CpnFlag.CDM;
                            }
                            else
                            {
                                CDM.Description = " Cash Dispenser ERROR";
                                CDM.StateComponent_Id = 2;
                            }
                            CDM.IntFields.Add(new StateFieldInt("Device", state.cdm.fwDevice));
                            CDM.IntFields.Add(new StateFieldInt("SafeDoor", state.cdm.fwSafeDoor));
                            CDM.IntFields.Add(new StateFieldInt("Dispenserstate", state.cdm.fwDispenserstate));
                            CDM.IntFields.Add(new StateFieldInt("IntermediateStacker", state.cdm.fwintermediateStacker));
                            CDM.IntFields.Add(new StateFieldInt("DevicePosition", state.cdm.wDevicePosition));
                            CDM.IntFields.Add(new StateFieldInt("AntiFraudModule", state.cdm.wAntiFraudModule));
                            if (state.cdm.positions.Count() > 0)
                            {
                                CDM.IntFields.Add(new StateFieldInt("Shutter01", state.cdm.positions[0].fwShutter));
                                CDM.IntFields.Add(new StateFieldInt("Transport01", state.cdm.positions[0].fwTransport));
                            }
                            CDM.StrFields.Add(new StateFieldStr("szExtra", state.cdm.szExtra));
                        }
                        else
                        {
                            CDM.Description = " Cash Dispenser STATE ERROR";
                            CDM.StateComponent_Id = 2;
                        }
                    }
                    else
                    {
                        CDM.Description = "N/A";
                        CDM.StateComponent_Id = 1;
                    }

                    cptstes.Add(CDM);
                    //component IDC
                    Entities.ComponentState/*Model*/ IDC = new Entities.ComponentState/*Model*/();
                    IDC.IntFields = new System.Collections.ObjectModel.Collection<StateFieldInt> { };
                    IDC.StrFields = new System.Collections.ObjectModel.Collection<StateFieldStr> { };
                    IDC.IntFields.Add(new StateFieldInt("idc.status", state.idc.status));
                    IDC.Component_Id = 2;
                    if (CpnFlag.IDC == (active_services & CpnFlag.IDC))
                    {
                        IDC.LastDate = _statedate;
                        if (state.idc.status == /*0*/iopcts.XFSAPIh.WFS_SUCCESS)
                        {
                            if ((IDCLabels.Device[state.idc.fwDevice].type == FState.ok))
                            //if ((state.idc.fwDevice == /*0*/iopcts.XFSIDCh.WFS_IDC_DEVONLINE || state.idc.fwDevice == /*6*/iopcts.XFSIDCh.WFS_IDC_DEVBUSY))
                            {
                                IDC.Description = " Card Reader OK";
                                IDC.StateComponent_Id = 1;
                                all_is_ok |= CpnFlag.IDC;
                            }
                            else
                            {
                                IDC.Description = "Card Reader ERROR";
                                IDC.StateComponent_Id = 2;
                            }
                            IDC.IntFields.Add(new StateFieldInt("Device", state.idc.fwDevice));
                            IDC.IntFields.Add(new StateFieldInt("Media", state.idc.fwMedia));
                            IDC.IntFields.Add(new StateFieldInt("RetainBin", state.idc.fwRetainBin));
                            IDC.IntFields.Add(new StateFieldInt("Security", state.idc.fwSecurity));
                            IDC.IntFields.Add(new StateFieldInt("Cards", state.idc.usCards));
                            IDC.IntFields.Add(new StateFieldInt("ChipModule", state.idc.fwChipModule));
                            IDC.IntFields.Add(new StateFieldInt("MagReadModule", state.idc.fwMagReadModule));
                            IDC.IntFields.Add(new StateFieldInt("AntiFraudModule", state.idc.wAntiFraudModule));
                            IDC.StrFields.Add(new StateFieldStr("Extra", state.idc.szExtra));
                        }
                        else
                        {
                            IDC.Description = "Card Reader STATE ERROR";
                            IDC.StateComponent_Id = 2;
                        }
                    }
                    else
                    {
                        IDC.Description = "N/A";
                        IDC.StateComponent_Id = 1;
                    }
                    cptstes.Add(IDC);
                    // component PIN
                    Entities.ComponentState/*Model*/ PIN = new Entities.ComponentState/*Model*/();
                    PIN.Component_Id = 3;
                    if (CpnFlag.PIN == (active_services & CpnFlag.PIN))
                    {
                        PIN.LastDate = _statedate;
                        PIN.IntFields = new System.Collections.ObjectModel.Collection<StateFieldInt> { };
                        PIN.StrFields = new System.Collections.ObjectModel.Collection<StateFieldStr> { };
                        PIN.IntFields.Add(new StateFieldInt("pin.status", state.pin.status));
                        if (state.pin.status == /*0*/iopcts.XFSAPIh.WFS_SUCCESS)
                        {
                            if ((PINLabels.Device[state.pin.fwDevice].type == FState.ok)
                                                    && (PINLabels.EncStat[state.pin.fwEncStat].type == FState.ok))
                            {
                                PIN.Description = " Pin pad OK";
                                PIN.StateComponent_Id = 1;
                                all_is_ok |= CpnFlag.PIN;
                            }
                            else
                            {
                                PIN.Description = "Pin Pad STATE ERROR";
                                PIN.StateComponent_Id = 2;
                            }
                            PIN.IntFields.Add(new StateFieldInt("Device", state.pin.fwDevice));
                            PIN.IntFields.Add(new StateFieldInt("EncStat", state.pin.fwEncStat));
                            PIN.IntFields.Add(new StateFieldInt("AntiFraudModule", state.pin.wAntiFraudModule));
                            PIN.StrFields.Add(new StateFieldStr("Extra", state.pin.szExtra));
                        }
                        else
                        {
                            PIN.Description = "Pin Pad STATE ERROR";
                            PIN.StateComponent_Id = 2;
                        }
                    }
                    else
                    {
                        PIN.Description = "N/A";
                        PIN.StateComponent_Id = 1;
                    }
                    cptstes.Add(PIN);
                    // component JRN
                    Entities.ComponentState/*Model*/ JRN = new Entities.ComponentState/*Model*/();
                    JRN.IntFields = new System.Collections.ObjectModel.Collection<StateFieldInt> { };
                    JRN.StrFields = new System.Collections.ObjectModel.Collection<StateFieldStr> { };
                    JRN.IntFields.Add(new StateFieldInt("jrn.status", state.jrn.status));
                    JRN.Component_Id = 4;
                    if (CpnFlag.JRN == (active_services & CpnFlag.JRN))
                    {
                        JRN.LastDate = _statedate;
                        if (state.jrn.status == /*0*/iopcts.XFSAPIh.WFS_SUCCESS)
                        {
                            if ((PTRLabels.Device[state.jrn.fwDevice].type == FState.ok)
                                                    /*&& (PTRLabels.Media[state.jrn.fwMedia].type == FState.ok)*/)
                            {
                                //fwMedia does not apply to journal printers
                                JRN.Description = " Journal Printer OK";
                                JRN.StateComponent_Id = 1;
                                all_is_ok |= CpnFlag.JRN;
                            }
                            else
                            {
                                JRN.Description = "Journal Printer ERROR";
                                JRN.StateComponent_Id = 2;
                            }
                            JRN.IntFields.Add(new StateFieldInt("Device", state.jrn.fwDevice));
                            int idx = iopcts.XFSPTRh.WFS_PTR_SUPPLYUPPER;
                            JRN.IntFields.Add(new StateFieldInt("Paper(upper)", state.jrn.fwPaper[idx]));
                            JRN.StrFields.Add(new StateFieldStr("Extra", state.jrn.szExtra));
                        }
                        else
                        {
                            JRN.Description = "Journal Printer STATE ERROR";
                            JRN.StateComponent_Id = 2;
                        }
                    }
                    else
                    {
                        JRN.Description = "N/A";
                        JRN.StateComponent_Id = 1;
                    }
                    cptstes.Add(JRN);
                    // component RCP
                    Entities.ComponentState/*Model*/ RCP = new Entities.ComponentState/*Model*/();
                    RCP.IntFields = new System.Collections.ObjectModel.Collection<StateFieldInt> { };
                    RCP.StrFields = new System.Collections.ObjectModel.Collection<StateFieldStr> { };
                    RCP.IntFields.Add(new StateFieldInt("rcp.status", state.rcp.status));
                    RCP.Component_Id = 5;
                    if (CpnFlag.RCP == (active_services & CpnFlag.RCP))
                    {
                        RCP.LastDate = _statedate;
                        if (state.rcp.status == /*0*/iopcts.XFSAPIh.WFS_SUCCESS)
                        {
                            if ((PTRLabels.Device[state.rcp.fwDevice].type == FState.ok)
                                && (PTRLabels.Media[state.rcp.fwMedia].type == FState.ok))
                            {
                                RCP.Description = " Reciept Printer OK";
                                RCP.StateComponent_Id = 1;
                                all_is_ok |= CpnFlag.RCP;
                            }
                            else
                            {
                                RCP.Description = "Reciept Printer ERROR";
                                RCP.StateComponent_Id = 2;
                            }
                            RCP.IntFields.Add(new StateFieldInt("Device", state.rcp.fwDevice));
                            RCP.IntFields.Add(new StateFieldInt("Media", state.rcp.fwMedia));
                            int idx = iopcts.XFSPTRh.WFS_PTR_SUPPLYUPPER;
                            RCP.IntFields.Add(new StateFieldInt("Paper(upper)", state.rcp.fwPaper[idx]));
                            RCP.StrFields.Add(new StateFieldStr("Extra", state.rcp.szExtra));
                        }
                        else
                        {
                            RCP.Description = "Reciept Printer STATE ERROR";
                            RCP.StateComponent_Id = 2;
                        }
                    }
                    else
                    {
                        RCP.Description = "N/A";
                        RCP.StateComponent_Id = 1;
                    }
                    cptstes.Add(RCP);

                    // component SIU
                    Entities.ComponentState/*Model*/ SIU = new Entities.ComponentState/*Model*/();
                    SIU.Component_Id = 6;
                    if (CpnFlag.SIU == (active_services & CpnFlag.SIU))
                    {
                        SIU.LastDate = _statedate;
                        SIU.IntFields = new System.Collections.ObjectModel.Collection<StateFieldInt> { };
                        SIU.StrFields = new System.Collections.ObjectModel.Collection<StateFieldStr> { };
                        SIU.IntFields.Add(new StateFieldInt("siu.status", state.siu.status));
                        if (iopcts.XFSAPIh.WFS_SUCCESS == state.siu.status)
                        {
                            if (
                                //state_atm_state == /*1*/iopcts.XFSSIUh.WFS_SIU_CLOSED 
                                //&& 
                                (SIULabels.Device[state.siu.fwDevice].type == FState.ok))
                            {
                                SIU.Description = " SUI OK";
                                SIU.StateComponent_Id = 1;
                                all_is_ok |= CpnFlag.SIU;
                            }
                            else
                            {
                                SIU.Description = "SUI ERROR";
                                SIU.StateComponent_Id = 2;
                            }
                            SIU.IntFields.Add(new StateFieldInt("Device", state.siu.fwDevice));
                            SIU.IntFields.Add(new StateFieldInt("Sensors(opswitch)", state.siu.fwSensors[iopcts.XFSSIUh.WFS_SIU_OPERATORSWITCH]));
                            SIU.IntFields.Add(new StateFieldInt("Indicators(OPENCLOSE)", state.siu.fwIndicators[iopcts.XFSSIUh.WFS_SIU_OPENCLOSE]));
                            SIU.IntFields.Add(new StateFieldInt("Indicators(FASCIALIGHT)", state.siu.fwIndicators[iopcts.XFSSIUh.WFS_SIU_FASCIALIGHT]));
                            SIU.IntFields.Add(new StateFieldInt("AntiFraudModule", state.siu.wAntiFraudModule));
                            SIU.StrFields.Add(new StateFieldStr("Extra", state.siu.szExtra));
                        }
                        else
                        {
                            SIU.Description = "SUI STATE ERROR";
                            SIU.StateComponent_Id = 2;
                        }
                    }
                    else
                    {
                        SIU.Description = "N/A";
                        SIU.StateComponent_Id = 1;
                    }
                    cptstes.Add(SIU);
                    // component TTU
                    Entities.ComponentState/*Model*/ TTU = new Entities.ComponentState/*Model*/();
                    TTU.Component_Id = 7;
                    if (CpnFlag.TTU == (active_services & CpnFlag.TTU))
                    {
                        TTU.LastDate = _statedate;
                        TTU.IntFields = new System.Collections.ObjectModel.Collection<StateFieldInt> { };
                        TTU.StrFields = new System.Collections.ObjectModel.Collection<StateFieldStr> { };
                        TTU.IntFields.Add(new StateFieldInt("ttu.status", state.ttu.status));
                        if (state.ttu.status == iopcts.XFSAPIh.WFS_SUCCESS)
                        {
                            if ((TTULabels.Device[state.ttu.fwDevice].type == FState.ok)
                                                    )
                            {
                                TTU.Description = "TTU OK";
                                TTU.StateComponent_Id = 1;
                                all_is_ok |= CpnFlag.TTU;
                            }
                            else
                            {
                                TTU.Description = "TTU ERROR";
                                TTU.StateComponent_Id = 2;
                            }
                            TTU.IntFields.Add(new StateFieldInt("Device", state.ttu.fwDevice));
                            TTU.StrFields.Add(new StateFieldStr("Extra", state.ttu.szExtra));
                        }
                        else
                        {
                            TTU.Description = "TTU ERROR";
                            TTU.StateComponent_Id = 2;
                        }
                    }
                    else
                    {
                        TTU.Description = "N/A";
                        TTU.StateComponent_Id = 1;
                    }
                    cptstes.Add(TTU);
                    //Console_Writeline("ok1");
                    //Console.ReadLine();
                    //Entities.Atm atm = db2.Atms.Where(a => a.Id == atmID).SingleOrDefault();
                    //bool is_ncr = (atm != null) && (!string.IsNullOrEmpty(atm.Profile)) && (atm.Profile == "NCR");
                    //float cashstockdivisor = (is_ncr ? 1 : 100);
                    double cashstockdivisor = 1.0;
                    if (!string.IsNullOrEmpty(atm.Profile))
                    {
                        switch (atm.Profile.ToUpper())
                        {
                            case "WN":
                                double.TryParse(WebConfigurationManager.AppSettings["WN_CASHSTOCKDIVISOR"], out cashstockdivisor);
                                break;
                            case "NCR":
                                double.TryParse(WebConfigurationManager.AppSettings["NCR_CASHSTOCKDIVISOR"], out cashstockdivisor);
                                break;
                            case "SIGMA":
                                double.TryParse(WebConfigurationManager.AppSettings["SIGMA_CASHSTOCKDIVISOR"], out cashstockdivisor);
                                break;
                        }
                    }
                    List<Entities.CashStock/*Model*/> CashStocksList = new List<Entities.CashStock/*Model*/>();
                    if (iopcts.XFSAPIh.WFS_SUCCESS == state.cashUnits.status)
                    {
                        UInt32 cassette_ok = 0x0;
                        uint cassette_bits = 0x0;
                        int total_bill = 0;
                        uint retracted_count = 0;
#if true//trace_cash
                        lock (trace_cash_obj)
                        {
                            TraceLogger.Info("CashUnits[" + state.cashUnits.usCount + "]{");
                            TraceLogger.Info("AtmId: " + atm.Profile + "-" + atmID);
#endif//trace_cash
                            List<Entities.CassetteStock/*Model*/> CassetteStocksList = new List<Entities.CassetteStock/*Model*/>();
                            for (int i = 0; i < state.cashUnits.usCount; i++)
                            {
                                WFSCDMCASHUNIT cashunit_i = state.cashUnits.lppList[i];
                                bool status_i = (cashunit_i.usStatus == iopcts.XFSCDMh.WFS_CDM_STATCUOK);
                                string status_lbl = status_i ? "OK" : "NOK";
                                if (cashunit_i.usStatus < AtmStateLib.CULabels.K7Status.Length)
                                {
                                    status_lbl = AtmStateLib.CULabels.K7Status[cashunit_i.usStatus].label;
                                }
#if true//trace_cash
                                TraceLogger.Info("cashunit_" + i);
                                TraceLogger.Info("CashUnitName: " + cashunit_i.lpszCashUnitName);
                                TraceLogger.Info("usStatus: " + cashunit_i.usStatus);
#endif//trace_cash
                                //if (cashunit_i.ulInitialCount == 0) { status_i = true; }
                                if (iopcts.XFSCDMh.WFS_CDM_TYPEBILLCASSETTE == cashunit_i.usType)
                                {

                                    string IdCassette = new string(cashunit_i.cUnitID);
                                    string Currency = new string(cashunit_i.cCurrencyID);
                                    int type = (int)(cashunit_i.ulValues / cashstockdivisor);

                                    if (atm.Profile.ToUpper() == "NCR")
                                    {
                                        switch (type)
                                        {
                                            case 5: type = 10000; break;
                                            case 10: type = 5000; break;
                                            case 20: type = 2000; break;
                                            case 50: type = 1000; break;
                                        }

                                        Currency = "CFA";

                                    }
#if true//trace_cash
                                    //if (atmID.Equals("48200054"))
                                    {
                                        TraceLogger.Info("WFS_CDM_TYPEBILLCASSETTE");
                                        TraceLogger.Info("  ulInitialCount: " + cashunit_i.ulInitialCount);
                                        TraceLogger.Info("         ulCount: " + cashunit_i.ulCount);
                                        TraceLogger.Info("ulDispensedCount: " + cashunit_i.ulDispensedCount);
                                        TraceLogger.Info("ulPresentedCount: " + cashunit_i.ulPresentedCount);
                                        TraceLogger.Info("   ulRejectCount: " + cashunit_i.ulRejectCount);
                                        TraceLogger.Info("ulRetractedCount: " + cashunit_i.ulRetractedCount);
                                        TraceLogger.Info("IdCassette: " + IdCassette);
                                        TraceLogger.Info("Currency: " + Currency);
                                        TraceLogger.Info("type: " + type);
                                        TraceLogger.Info("ulMaximum: " + cashunit_i.ulMaximum);
                                        TraceLogger.Info("ulMinimum: " + cashunit_i.ulMinimum);
                                        TraceLogger.Info("ulValues: " + cashunit_i.ulValues);
                                        TraceLogger.Info("usNumber: " + cashunit_i.usNumber);
                                        TraceLogger.Info("usNumPhysicalCUs: " + cashunit_i.usNumPhysicalCUs);
                                    }
#endif//trace_cash
                                    CassetteStocksList.Add(new
                                    Entities.CassetteStock/*Model*/
                                    {
                                        IdCassette = IdCassette,
                                        Currency = Currency,
                                        Type = type.ToString(),
                                        Start = (int)cashunit_i.ulInitialCount,
                                        Presented = (int)cashunit_i.ulPresentedCount,
                                        Rejected = (int)cashunit_i.ulRejectCount,
                                        StateCassette = status_lbl,
                                    }
                                    );
                                    int remaining = (int)cashunit_i.ulCount;
                                    if (atm.Profile.ToLower() != "sigma")
                                    {
                                        remaining = (int)(cashunit_i.ulInitialCount - cashunit_i.ulPresentedCount - cashunit_i.ulRejectCount);
                                    }
                                    int bill_i = (remaining * type);
                                    total_bill += bill_i;
#if true//trace_cash
                                    TraceLogger.Info("bill: " + remaining + "*" + type + " = " + bill_i);
#endif//trace_cash
                                }
                                else if (iopcts.XFSCDMh.WFS_CDM_TYPERETRACTCASSETTE == cashunit_i.usType)
                                {
#if true//trace_cash
                                    TraceLogger.Info("WFS_CDM_TYPERETRACTCASSETTE");
                                    TraceLogger.Info("ulCount: " + cashunit_i.ulCount);
#endif//trace_cash
                                    retracted_count += cashunit_i.ulCount;//.ulRetractedCount;
                                }
                                else
                                {
#if true//trace_cash
                                    TraceLogger.Info("usType: " + cashunit_i.usType);
#endif//trace_cash
                                }
                                ///////////////////////////////////////////////////////////////////////
                                if (((iopcts.XFSCDMh.WFS_CDM_TYPEBILLCASSETTE == cashunit_i.usType)
                                    || (iopcts.XFSCDMh.WFS_CDM_TYPERETRACTCASSETTE == cashunit_i.usType))
                                    && (cashunit_i.ulInitialCount > 0))
                                {
                                    cassette_bits |= (uint)(0x1 << i);
                                    if (status_i)
                                    {
                                        cassette_ok |= (uint)(0x1 << i);
                                    }
                                }
                            }
#if true//trace_cash
                            TraceLogger.Info("retracted_count: " + retracted_count);
                            TraceLogger.Info("total_bill: " + total_bill);
#endif//trace_cash
                            CashStocksList.Add(
                                new Entities.CashStock/*Model*/
                                {
                                    Retract = (int)retracted_count,
                                    Total = total_bill,
                                    Treshold = 70,
                                    CassetteStocks = CassetteStocksList
                                }
                                );
                            string s1 = Convert.ToString(cassette_ok, 2);//cassette_ok.ToString("X")
                            string s2 = Convert.ToString(cassette_bits, 2);//cassette_bits.ToString("X")
#if true//trace_cash
                            TraceLogger.Info("cassettes: " + s1 + "/" + s2);
#endif//trace_cash
                            if (cassette_ok == cassette_bits)
                            {
                                all_is_ok |= CpnFlag.CASH;
                            }
#if true//trace_cash
                            TraceLogger.Info("}cashUnits");
                        }//trace_cash_obj
#endif//trace_cash
                    }//cashunits
#endif//components
                    Entities.State/*Model*/ _state = new Entities.State/*Model*/
                    {
                        Atm_Id = atmID,
                        //  StateType = new Entities.StateType/*Model*/ { Id = 3 },
                        StateType_Id = (int)StateEnum.inservice,
                        msc_state = _msstate,
                        //insert_counter = insert_counter_,
                        StateDate = _statedate/*DateTime.Now*/,
                        LastTransaction = _statedate/*DateTime.Now*/,
                        ComponentStates = cptstes,
                        CashStocks = CashStocksList,
                    };

                    //Console_Writeline("ok2");
                    //Console.ReadLine();

                    Console_Writeline("SIU STATUS: " + state.siu.status);
                    _state.StateType_Id = (int)StateEnum.unknwon;//unknown
                    if (iopcts.XFSAPIh.WFS_SUCCESS == state.siu.status)
                    {
                        if ((iopcts.XFSSIUh.WFS_SIU_DEVONLINE == state.siu.fwDevice)
                                                        || (iopcts.XFSSIUh.WFS_SIU_DEVBUSY == state.siu.fwDevice)
                                                        ) {
                            //fwIndicators[WFS_SIU_OPENCLOSE] 
                            //Specifies the state of the Open/ Closed Indicator as one of the following flags: 
                            //WFS_SIU_NOT_AVAILABLE The status is not available. 
                            //WFS_SIU_CLOSED The terminal is closed for a consumer. 
                            //WFS_SIU_OPEN The terminal is open to be used by a consumer.   
                            int state_atm_state = state.siu.fwIndicators[iopcts.XFSSIUh.WFS_SIU_OPENCLOSE];
                            int state_atm_sopstate = state.siu.fwSensors[iopcts.XFSSIUh.WFS_SIU_OPERATORSWITCH];
                            Console_Writeline("SIU OPENCLOSE: " + state_atm_state);
                            Console_Writeline("SIU OPERATORSWITCH: " + state_atm_sopstate);
                            if (state_atm_sopstate != iopcts.XFSSIUh.WFS_SIU_NOT_AVAILABLE)
                            {
                                UInt16 maintenance_flag = (iopcts.XFSSIUh.WFS_SIU_MAINTENANCE | iopcts.XFSSIUh.WFS_SIU_SUPERVISOR);
                                if ((state_atm_sopstate & maintenance_flag) != 0)
                                {
                                    _state.StateType_Id = (int)StateEnum.maintenance;

                                }
                                else if (state_atm_sopstate == iopcts.XFSSIUh.WFS_SIU_RUN)
                                {
                                    if (iopcts.XFSSIUh.WFS_SIU_OPEN == state_atm_state)
                                    {
                                        _state.StateType_Id = (int)StateEnum.inservice;
                                    }
                                    else if (iopcts.XFSSIUh.WFS_SIU_CLOSED == state_atm_state)
                                    {
                                        _state.StateType_Id = (int)StateEnum.outofserv;
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Assert((iopcts.XFSSIUh.WFS_SIU_NOT_AVAILABLE == state_atm_state));
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Assert(false);
                                }
                            }
                        }
                        else
                        {
                            Console_Writeline("SIU DEVICE NOT OK : " + state.siu.fwDevice);
                        }
                    }//xfs
                    if (_state.StateType_Id == (int)StateEnum.unknwon)//still unknown
                    {
                        _state.StateType_Id = atm_checkmode;
                    }
                    if (_state.StateType_Id == (int)StateEnum.unknwon)//still unknown
                    {
                        _state.StateType_Id = atm_jrnlmode;
                    }
                    if (_state.StateType_Id == (int)StateEnum.unknwon)//still unknown
                    {
                        _state.StateType_Id = (int)StateEnum.outofserv;
                    }
                    //_msstate
                    Console_Writeline("_state.StateType_Id: " + _state.StateType_Id);
                    Console_Writeline("all_is_ok: " + Convert.ToString(all_is_ok, 2));
                    ExceptionLogger.Error("_state.StateType_Id: " + _state.StateType_Id);
                    if (_state.StateType_Id == (int)StateEnum.inservice)
                    {//in service
                        if (all_is_ok != active_services)
                        {
                            _state.StateType_Id = (int)StateEnum.warning;
                        }
                    }
                    List<ErrTypeId> errsList = HandleErrors(state, _state);
                    _state.ErrTypeIds = errsList; //Detail of the atm error


                    //Console_Writeline("=> _state.StateType_Id: " + _state.StateType_Id);
                    //bool success = true;
                    ////Console_Writeline("ok3");
                    //Console_Writeline("_state.Atm_Id: " + _state.Atm_Id);
                    //Console_Writeline("ip @: " + GetLocalIPAddress());
                    ExceptionLogger.Error("_state.StateType_Id: place 1 ");

                    var recentatmState = _RecentAtmStateService.GetAll(elt => (elt.Atm_Id == atmID)).AsQueryable().AsNoTracking().FirstOrDefault();
#if true//state-md5
                    string CurrrentHash = "";
                    try
                    {

                        CurrrentHash = StateHashCode.Generate(_state);
                        if (recentatmState != null)
                        {
                            bool equalhashcodes = MD5Hash.VerifyMd5Hash(CurrrentHash, recentatmState.md5Hash);
                            if (equalhashcodes)
                            {
                                //#if !DEBUG
#if true//lastseen_lasttrx
                                recentatmState.LastSeen = _statedate;
                                recentatmState.LastTransaction = _statedate;
                                _RecentAtmStateService.Update(recentatmState);
#endif//lastseen_lasttrx
                                result.logmsg += Environment.NewLine + "Equal Hashcodes: skip insertion!";
                                result.excepocurred = false;
                                result.statecreated = false;
                                ExceptionLogger.Error("VerifyMd5Hash");
                                    
                                return result;
                                //#endif//!DEBUGd
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.Error(ex.Message);
                        ExceptionLogger.Error(ex.StackTrace);
                    }
#endif//state-md5
                    int stateId = _StateService.Create(_state);
                    ExceptionLogger.Error("(cashstock-alert.........)");
#if true//cashstock-alert
#if !DEBUG
                    if ((_state.StateType_Id == (int)StateEnum.inservice) || (_state.StateType_Id == (int)StateEnum.warning))
#endif//!DEBUG
                    {
                        try
                        {
                            var cashstock = CashStocksList.FirstOrDefault();
                            //var devicename = atm.Id + "--" + atm.Name;
                            ExceptionLogger.Error("(cashstock != null)");
                            if ((cashstock != null))
                            {
                                int thresholdWarningValue = 3000000;
                                try
                                {
                                    string thresholdWarning = _ParametersService.FindBy(x => x.name == "ThresholdWarning").FirstOrDefault().value;
                                    int.TryParse(thresholdWarning, out thresholdWarningValue);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionLogger.Error(ex.Message);
                                    ExceptionLogger.Error(ex.StackTrace);
                                }
                                int thresholdExaustionValue = 500000;
                                try
                                {
                                    string thresholdExaustion = _ParametersService.FindBy(x => x.name == "ThresholdExaustion").FirstOrDefault().value;
                                    int.TryParse(thresholdExaustion, out thresholdExaustionValue);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionLogger.Error(ex.Message);
                                    ExceptionLogger.Error(ex.StackTrace);
                                }
                                if (cashstock.Total < thresholdWarningValue)
                                {
                                    bool cashexausion = (cashstock.Total < thresholdWarningValue);
                                    //var cashalerts = db2.AtmCashAlerts.Where(a => a.Atm_Id == _state.Atm_Id && a.EndDate == null && (a.Exaustion == cashexausion)).ToList();
                                    var cashalerts = db2.AtmCashAlerts.Where(a => a.Atm_Id == _state.Atm_Id && a.EndDate == null).ToList();
                                    if (cashalerts.ToList().Count == 0)
                                    {

                                        var cashalert = new AtmView.Entities.AtmCashAlert
                                        {
                                            Atm_Id = _state.Atm_Id,
                                            StartDate = _state.StateDate,
                                            EndDate = null,
                                            State_Id = _state.Id,
                                            Exaustion = false,
                                        };
                                        db2.AtmCashAlerts.Add(cashalert);
                                        db2.SaveChanges();
                                        // modif Ayoub
                                        var raiseTest = new RaiseAlert();
                                        List<object> list = new List<object>();

                                        Alert alert = db2.Alerts.Find("WarningCash");

                                        list.Add(alert);
                                        list.Add(atm);
                                        list.Add(cashstock);
                                        list.Add(User.Identity.GetUserId());
                                        ExceptionLogger.Error("alert 1 start");
                                        raiseTest.RaiseAlerts(1, list);

                                        // SendCashAlert(atm, cashstock, false);
                                        recentatmState.cashalert = true;
                                    }
                                    else if (cashalerts.Where(m => m.Exaustion == false && m.EndDate == null).ToList().Count != 0 && cashstock.Total < thresholdExaustionValue)
                                    {
                                        ExceptionLogger.Error("Cash Alert" + cashalerts.Where(m => m.Exaustion == false).ToList().Count);
                                        cashalerts = cashalerts.Where(m => m.Exaustion == false).ToList();
                                        cashalerts.ForEach(x => x.Exaustion = true);
                                        cashalerts.ForEach(x => x.EndDate = null);
                                        foreach (var item in cashalerts)
                                        {
                                            db2.Entry(item).State = EntityState.Modified;
                                        }

                                        db2.SaveChanges();
                                        var raiseTest = new RaiseAlert();
                                        List<object> list = new List<object>();

                                        Alert alert = db2.Alerts.Find("ExhaustionCash");

                                        list.Add(alert);
                                        list.Add(atm);
                                        list.Add(cashstock);
                                        list.Add(User.Identity.GetUserId());
                                        ExceptionLogger.Error("alert 7 start");
                                        raiseTest.RaiseAlerts(7, list);

                                        // SendCashAlert(atm, cashstock, false);
                                        recentatmState.cashalert = true;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        var cashalerts = db2.AtmCashAlerts.Where(a => a.Atm_Id == _state.Atm_Id && a.EndDate == null).ToList();
                                        if (cashalerts.Any())
                                        {
                                            cashalerts.ForEach(x => { x.EndDate = _state.StateDate; });
                                            foreach (var item in cashalerts)
                                            {
                                                db2.Entry(item).State = EntityState.Modified;
                                            }
                                            db2.SaveChanges();

                                            var raiseTest = new RaiseAlert();
                                            List<object> list = new List<object>();

                                            Alert alert = db2.Alerts.Find("AlimentationCash");

                                            list.Add(alert);
                                            list.Add(atm);
                                            list.Add(cashstock);
                                            ExceptionLogger.Error("alert 2 start");
                                            raiseTest.RaiseAlerts(2, list);

                                            // SendCashAlert(atm, cashstock, true);
                                            recentatmState.cashalert = false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ExceptionLogger.Error(ExceptionUtil.ExceptionMessage(ex));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionLogger.Error(ex.Message);
                            ExceptionLogger.Error(ex.StackTrace);
                        }
                    }
#endif//cashstock-alert
                    uint state_bit = ((_state.StateType_Id == (int)StateEnum.maintenance) 
                        || (_state.StateType_Id == (int)StateEnum.inservice)
                        || (_state.StateType_Id == (int)StateEnum.warning)
                        || (_state.StateType_Id == (int)StateEnum.inactive)
                        || (_state.StateType_Id == (int)StateEnum.lowcash)
                        ) ? (uint)1 : (uint)0;
                    if (recentatmState != null)
                    {
                        recentatmState.State_Id = stateId;
                        recentatmState.LastStateType = _state.StateType_Id;
                        recentatmState.Connected = true;
#if true//state-md5
                        recentatmState.md5Hash = CurrrentHash;
#endif//state-md5
#if true//lastseen_lasttrx
                        recentatmState.LastSeen = _statedate;
#endif//lastseen_lasttrx
#if true//monitoring
                        recentatmState.statehistory <<= 1;
                        recentatmState.statehistory |= state_bit;
                        recentatmState.checkflag = (0==state_bit);
#endif//monitoring
                        _RecentAtmStateService.Update(recentatmState);
                    }
                    else
                    {
                        recentatmState = new RecentAtmState
                        {
                            Atm_Id = _state.Atm_Id
                            ,
                            State_Id = stateId
                            ,//LastStateType pour obtenir l'info rapidement si on a besoin que de statetype
                            LastStateType = _state.StateType_Id
                            ,//connected à true puisqu'on vient de recevoir le state du gab apres ca peut changer selon l'etat de signalR
                            Connected = true,
#if true//state-md5
                            md5Hash = CurrrentHash,
#endif//state-md5
#if true//lastseen_lasttrx
                            LastSeen = _statedate,

                            LastTransaction=_statedate,
#endif//lastseen_lasttrx
#if true//monitoring
                            statehistory = state_bit,
#endif//monitoring
                        };
                        _RecentAtmStateService.Create(recentatmState);
                    }
#if true//error_declaration
                    //Après chaque réception d'un state en erreur d'un Gab ( id=4), insérer une nouvelle entrée dans la nouvelle table AtmError
                    if ((_state.StateType_Id == (int)StateEnum.maintenance) || (_state.StateType_Id == (int)StateEnum.outofserv))
                    {
                        int atmerrid;
                        //var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                        //int days = (int)cal.GetDayOfWeek(DateTime.Today);DayOfWeek
                        // si il n a pas deja une entree en erreur dans la meme journee pour le meme atm
                        var errorList = _AtmErrorService.FindBy(e => e.Atm_Id == _state.Atm_Id && e.EndDate == null /*&& e.StartDate >= DateTime.Today*/).ToList();
                        //#if DEBUG
                        //                        bool createnewerr = true;
                        //                        foreach (var er in errorList)
                        //                        {
                        //                            if (DatesAreInTheSameWeek(er.StartDate, _state.StateDate))
                        //                            {
                        //                                createnewerr = false;
                        //                            }
                        //                        }
                        //                        if (createnewerr)//
                        //#else
                        if (errorList.Count == 0)
                        //#endif
                        {
                            var ActionCorrectiveGeneric = _ActionCorrectiveService.GetAll().ToList().FirstOrDefault();
                            var errorAtm = new AtmView.Entities.AtmError
                            {
                                Atm_Id = _state.Atm_Id,
                                StartDate = _state.StateDate,
                                State_Id = _state.Id,
                                ActionCorrective_Id = ActionCorrectiveGeneric.Id, //first action
                            };
                            _AtmErrorService.Create(errorAtm);
                            atmerrid = errorAtm.Id;
                        }
                        else
                        {
                            var errorAtm = errorList.LastOrDefault();
                            errorAtm.State_Id = _state.Id;
                            //errorAtm.ErrTypeIds = errsList;
                            _AtmErrorService.Update(errorAtm);
                            atmerrid = errorAtm.Id;
                        }
#if true
                        if ((_state.StateType_Id == (int)StateEnum.outofserv))
                        {
                            var handleStateTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                            {
                                var result2 = HandleAtmBugs(_state.Atm_Id, _state.Id, atmerrid, null);
                            });
                            handleStateTask.Wait(15);
                        }
                        result.haserror = false;
#else
                        result.haserror = (_state.StateType_Id == (int)StateEnum.outofserv);
#endif
                        result.atmerrid = atmerrid;
                    }
                    else if ((_state.StateType_Id == (int)StateEnum.inservice) || (_state.StateType_Id == (int)StateEnum.warning))
                    {//si une entre dans la table atmerror est ouvert pour l atm alors il faut la cloturer

                        var errorList = _AtmErrorService.FindBy(e => e.Atm_Id == _state.Atm_Id && e.EndDate == null /*&& e.StartDate >= DateTime.Today*/).ToList();
                        if (errorList.Count() >= 1)
                        {
                            var errorAtm = errorList.FirstOrDefault();
                            errorAtm.EndDate = _state.StateDate;
                            _AtmErrorService.Update(errorAtm);
                            var handleStateTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                            {
                                CloseAtmBugs(_state.Atm_Id, errorAtm.Id);

                                var raiseTest = new RaiseAlert();
                                Alert alert = db2.Alerts.Find("InService");
                                List<object> list = new List<object>();
                                list.Add(alert);
                                list.Add(atm);
                                raiseTest.RaiseAlerts(4, list);

                                // SendInServiceEmail(atm);
                            });
                        }

                    }
#endif//error_declaration
                    int doneOrderId = 0;
                    try
                    {
                        doneOrderId = InsertDoneOrder(_state);
                    }
                    catch (Exception ex)
                    {
                        //loger l erreur
                        doneOrderId = 0;
                        Console_Writeline(ex.Message);
                        ExceptionLogger.Error(ex.Message);
                        ExceptionLogger.Error(ex.StackTrace);
                    }
                    if (doneOrderId != 0)
                    {
                        //mettre a jour doneorder stateafterId:
                        State recentSate = _StateService.GetAll(elt => elt.Atm_Id == _state.Atm_Id).OrderByDescending(e => e.Id).FirstOrDefault();
                        var doneOrder = _DoneOrderService.GetById(doneOrderId);
                        doneOrder.StateAfter_Id = recentSate.Id;
                        _DoneOrderService.Update(doneOrder);
                    }
                    //-----------------------------------------------------------
                    result.logmsg += Environment.NewLine + "OK";
                    result.statecreated = true;
                    result.stateid = _state.Id;
                }
                catch (Exception ex)
                {
                    result.excepocurred = true;
                    Console_Writeline("Insertion failed: ");
                    result.exmessage = " StackTrace: " + ex.StackTrace.ToString() + ex.Message + " innerex: ";
                    var innerex = ex.InnerException;
                    while (innerex != null)
                    {
                        result.exmessage += Environment.NewLine + innerex.Message;
                        innerex = innerex.InnerException;
                    }
                    Console.WriteLine(result);
                    ExceptionLogger.Error(result.exmessage);
                    ExceptionLogger.Error(ex.StackTrace);
                }
            }
            return result;
            void Console_Writeline(string _msg)
            {
                result.logmsg += Environment.NewLine + _msg;
                Console.WriteLine(_msg);
            }
        }

        [WebMethod]
        public StateResult2 HandleStateBugs(string _atmid, int _stateid, int _atmerrid, bool _attachmentuploaded, string _attachmentfile)
        {
            StateResult2 result = null;
            var handleStateTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                result = HandleAtmBugs(_atmid, _stateid, _atmerrid, _attachmentfile);
            });
            handleStateTask.Wait();
            return result;
        }

        private void CloseAtmBugs(string atm_Id, int _atmerr_id)
        {
            //AtmView.Entities.AtmViewContext db2 = new AtmView.Entities.AtmViewContext();
            var buglist = db2.Bugs.AsNoTracking().Where(b => ((b.AtmError_Id == _atmerr_id) && (b.BugStatut_Id != 4))).ToList();
            buglist.ForEach(b =>
            {
                b.BugStatut_Id = 4;
                b.LastUpdateDate = DateTime.Now;
                db2.Entry(b).State = System.Data.Entity.EntityState.Modified;
            });
            db2.SaveChanges();
        }

        private List<ErrTypeId> HandleErrors(WFSATM _wfsstate, State _state)
        {
            var stateFields = new List<StateErrorInfo>();
            if ((int)StateEnum.outofserv == _state.StateType_Id)
            {
                StateErrorInfo errinfo = new StateErrorInfo();
                errinfo.errcode = (int)_state.StateType_Id;
                errinfo.status = new StateDescript("Out of Service", FState.error, "Gab Hors Service!");
                stateFields.Add(errinfo);
            }
            {
                //CDM: 100000
                if (_wfsstate.cdm.status == iopcts.XFSAPIh.WFS_SUCCESS)
                {
                    //if (_wfsstate.cdm.fwDevice != iopcts.XFSCDMh.WFS_CDM_DEVONLINE)
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        //errinfo.errcode = ((1 << 16) | (1 << 8) | (_wfsstate.cdm.fwDevice));
                        errinfo.errcode = ((100100) | (_wfsstate.cdm.fwDevice));
                        //errinfo.label = CDMLabels.Device[_wfsstate.cdm.fwDevice].label;
                        errinfo.status = CDMLabels.Device[_wfsstate.cdm.fwDevice];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.cdm.fwSafeDoor != iopcts.XFSCDMh.WFS_CDM_DOORNOTSUPPORTED) && (_wfsstate.cdm.fwSafeDoor != iopcts.XFSCDMh.WFS_CDM_DOORCLOSED))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        //errinfo.errcode = ((1 << 16) | (2 << 8) | (_wfsstate.cdm.fwSafeDoor));
                        errinfo.errcode = ((100200) | (_wfsstate.cdm.fwSafeDoor));
                        //errinfo.label = CDMLabels.SafeDoor[_wfsstate.cdm.fwSafeDoor].label;
                        errinfo.status = CDMLabels.SafeDoor[_wfsstate.cdm.fwSafeDoor];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if (_wfsstate.cdm.fwDispenserstate != iopcts.XFSCDMh.WFS_CDM_DISPOK)
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        //errinfo.errcode = ((1 << 16) | (3 << 8) | (_wfsstate.cdm.fwDispenserstate));
                        errinfo.errcode = ((100300) | (_wfsstate.cdm.fwDispenserstate));
                        //errinfo.label = CDMLabels.Dispenser[_wfsstate.cdm.fwDispenserstate].label;
                        errinfo.status = CDMLabels.Dispenser[_wfsstate.cdm.fwDispenserstate];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.cdm.fwintermediateStacker != iopcts.XFSCDMh.WFS_CDM_ISNOTSUPPORTED) && (_wfsstate.cdm.fwintermediateStacker != iopcts.XFSCDMh.WFS_CDM_ISEMPTY))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((100400) | (_wfsstate.cdm.fwintermediateStacker));
                        //errinfo.label = CDMLabels.IntermediateStacker[_wfsstate.cdm.fwintermediateStacker].label;
                        errinfo.status = CDMLabels.IntermediateStacker[_wfsstate.cdm.fwintermediateStacker];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.cdm.wDevicePosition != iopcts.XFSCDMh.WFS_CDM_DEVICEINPOSITION) && (_wfsstate.cdm.wDevicePosition != iopcts.XFSCDMh.WFS_CDM_DEVICEPOSNOTSUPP))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((100500) | ((Int32)_wfsstate.cdm.wDevicePosition));
                        //errinfo.label = CDMLabels.DevicePosition[_wfsstate.cdm.wDevicePosition].label;
                        errinfo.status = CDMLabels.DevicePosition[_wfsstate.cdm.wDevicePosition];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.cdm.wAntiFraudModule != iopcts.XFSCDMh.WFS_CDM_AFMNOTSUPP) && (_wfsstate.cdm.wAntiFraudModule != iopcts.XFSCDMh.WFS_CDM_AFMOK))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((100600) | ((Int32)_wfsstate.cdm.wAntiFraudModule));
                        //errinfo.label = CDMLabels.AntiFraudModule[_wfsstate.cdm.wAntiFraudModule].label;
                        errinfo.status = CDMLabels.AntiFraudModule[_wfsstate.cdm.wAntiFraudModule];//.descri
                        stateFields.Add(errinfo);
                    }
                    ////if ((_wfsstate.cdm != iopcts.XFSCDMh) && (_wfsstate.cdm != iopcts.XFSCDMh))
                    //{ }
                    //CUs: 800000
                    {
                    }
                }
                else
                {
                    StateErrorInfo errinfo = new StateErrorInfo();
                    errinfo.errcode = ((100000) | ((Int32)(-_wfsstate.cdm.status)));
                    errinfo.status = CDMLabels.WfsGetInfo;
                    stateFields.Add(errinfo);
                }
                //IDC: 200000
                if (_wfsstate.idc.status == iopcts.XFSAPIh.WFS_SUCCESS)
                {
                    var idc_device = IDCLabels.Device[_wfsstate.idc.fwDevice];
                    var idc_media = IDCLabels.Media[_wfsstate.idc.fwMedia];
                    //var idc_retain = IDCLabels.RetainBin[_wfsstate.idc.fwRetainBin];
                    //if (idc_device.type!= FState.ok)
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200100) | ((Int32)_wfsstate.idc.fwDevice));
                        //errinfo.label = idc_device.label;
                        errinfo.status = idc_device;//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.idc.fwMedia == iopcts.XFSIDCh.WFS_IDC_MEDIAJAMMED) && (_wfsstate.idc.fwMedia != iopcts.XFSIDCh.WFS_IDC_MEDIANOTSUPP))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200200) | ((Int32)_wfsstate.idc.fwMedia));
                        //errinfo.label = idc_media.label;
                        errinfo.status = idc_media;//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.idc.fwRetainBin != iopcts.XFSIDCh.WFS_IDC_RETAINBINOK) && (_wfsstate.idc.fwRetainBin != iopcts.XFSIDCh.WFS_IDC_RETAINNOTSUPP))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200300) | ((Int32)_wfsstate.idc.fwRetainBin));
                        //errinfo.label = IDCLabels.RetainBin[_wfsstate.idc.fwRetainBin].label;
                        errinfo.status = IDCLabels.RetainBin[_wfsstate.idc.fwRetainBin];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.idc.fwSecurity == iopcts.XFSIDCh.WFS_IDC_SECNOTREADY))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200400) | ((Int32)_wfsstate.idc.fwSecurity));
                        //errinfo.label = IDCLabels.Security[_wfsstate.idc.fwSecurity].label;
                        errinfo.status = IDCLabels.Security[_wfsstate.idc.fwSecurity];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.idc.fwChipModule != iopcts.XFSIDCh.WFS_IDC_CHIPMODOK) && (_wfsstate.idc.fwChipModule != iopcts.XFSIDCh.WFS_IDC_CHIPMODNOTSUPP))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200500) | ((Int32)_wfsstate.idc.fwChipModule));
                        //errinfo.label = IDCLabels.ChipModule[_wfsstate.idc.fwChipModule].label;
                        errinfo.status = IDCLabels.ChipModule[_wfsstate.idc.fwChipModule];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.idc.fwMagReadModule != iopcts.XFSIDCh.WFS_IDC_MAGMODOK) && (_wfsstate.idc.fwMagReadModule != iopcts.XFSIDCh.WFS_IDC_MAGMODNOTSUPP))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200600) | ((Int32)_wfsstate.idc.fwMagReadModule));
                        //errinfo.label = IDCLabels.MagReadModule[_wfsstate.idc.fwMagReadModule].label;
                        errinfo.status = IDCLabels.MagReadModule[_wfsstate.idc.fwMagReadModule];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.idc.wDevicePosition != iopcts.XFSIDCh.WFS_IDC_DEVICEINPOSITION) && (_wfsstate.idc.wDevicePosition != iopcts.XFSIDCh.WFS_IDC_DEVICEPOSNOTSUPP))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200700) | ((Int32)_wfsstate.idc.wDevicePosition));
                        //errinfo.label = IDCLabels.DevicePosition[_wfsstate.idc.wDevicePosition].label;
                        errinfo.status = IDCLabels.DevicePosition[_wfsstate.idc.wDevicePosition];//.descri
                        stateFields.Add(errinfo);
                    }
                    //if ((_wfsstate.idc.wAntiFraudModule != iopcts.XFSIDCh.WFS_IDC_AFMOK) && (_wfsstate.idc.wAntiFraudModule != iopcts.XFSIDCh.WFS_IDC_AFMNOTSUPP))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((200800) | ((Int32)_wfsstate.idc.wAntiFraudModule));
                        //errinfo.label = IDCLabels.AntiFraudModule[_wfsstate.idc.wAntiFraudModule].label;
                        errinfo.status = IDCLabels.AntiFraudModule[_wfsstate.idc.wAntiFraudModule];//.descri
                        stateFields.Add(errinfo);
                    }
                }
                else
                {
                    StateErrorInfo errinfo = new StateErrorInfo();
                    errinfo.errcode = ((200000) | ((Int32)(-_wfsstate.idc.status)));
                    errinfo.status = IDCLabels.WfsGetInfo;
                    stateFields.Add(errinfo);
                }
                //JRN: 300000
                if (_wfsstate.jrn.status == iopcts.XFSAPIh.WFS_SUCCESS)
                {
                    //if ((_wfsstate.jrn.fwDevice != iopcts.XFSPTRh.WFS_PTR_DEVONLINE))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((300100) | ((Int32)_wfsstate.jrn.fwDevice));
                        //errinfo.label = PTRLabels.Device[_wfsstate.jrn.fwDevice].label;
                        errinfo.status = PTRLabels.Device[_wfsstate.jrn.fwDevice];//.descri
                        stateFields.Add(errinfo);
                    }
                    int upperidx = iopcts.XFSPTRh.WFS_PTR_SUPPLYUPPER;
                    int uppervalue = _wfsstate.jrn.fwPaper[upperidx];
                    //if ((uppervalue != iopcts.XFSPTRh.WFS_PTR_PAPERFULL))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((300300) | ((Int32)_wfsstate.jrn.fwDevice));
                        //errinfo.label = PTRLabels.Paper[uppervalue].label;
                        errinfo.status = PTRLabels.Paper[uppervalue];//.descri
                        stateFields.Add(errinfo);
                    }
                }
                else
                {
                    StateErrorInfo errinfo = new StateErrorInfo();
                    errinfo.errcode = ((300000) | ((Int32)(-_wfsstate.jrn.status)));
                    errinfo.status = PTRLabels.WfsGetInfo;
                    errinfo.status.label.Replace("PTR", "JRN");
                    stateFields.Add(errinfo);
                }
                //PIN: 400000
                if (_wfsstate.pin.status == iopcts.XFSAPIh.WFS_SUCCESS)
                {
                    //if ((_wfsstate.pin.fwDevice != iopcts.XFSPINh.WFS_PIN_DEVONLINE))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((400100) | ((Int32)_wfsstate.pin.fwDevice));
                        //errinfo.label = PINLabels.Device[_wfsstate.pin.fwDevice].label;
                        errinfo.status = PINLabels.Device[_wfsstate.pin.fwDevice];//.descri
                        stateFields.Add(errinfo);
                    }
                }
                else
                {
                    StateErrorInfo errinfo = new StateErrorInfo();
                    errinfo.errcode = ((400000) | ((Int32)(-_wfsstate.pin.status)));
                    errinfo.status = PINLabels.WfsGetInfo;
                    stateFields.Add(errinfo);
                }

                //RCP: 500000
                if (_wfsstate.rcp.status == iopcts.XFSAPIh.WFS_SUCCESS)
                {
                    //if ((_wfsstate.rcp.fwDevice != iopcts.XFSPTRh.WFS_PTR_DEVONLINE))
                    {
                        StateErrorInfo errinfo = new StateErrorInfo();
                        errinfo.errcode = ((500100) | ((Int32)_wfsstate.rcp.fwDevice));
                        //errinfo.label = PTRLabels.Device[_wfsstate.rcp.fwDevice].label;
                        errinfo.status = PTRLabels.Device[_wfsstate.rcp.fwDevice];//.descri
                        stateFields.Add(errinfo);
                    }
                }
                else
                {
                    StateErrorInfo errinfo = new StateErrorInfo();
                    errinfo.errcode = ((500000) | ((Int32)(-_wfsstate.rcp.status)));
                    errinfo.status = PTRLabels.WfsGetInfo;
                    errinfo.status.label.Replace("PTR", "RCP");
                    stateFields.Add(errinfo);
                }
                //SIU: 600000
                if (_wfsstate.siu.status == iopcts.XFSAPIh.WFS_SUCCESS)
                {
                    ////if ((_wfsstate.cdm != iopcts.XFSCDMh) && (_wfsstate.cdm != iopcts.XFSCDMh))
                    //{ }
                }
                else
                {
                    StateErrorInfo errinfo = new StateErrorInfo();
                    errinfo.errcode = ((600000) | ((Int32)(-_wfsstate.siu.status)));
                    errinfo.status = SIULabels.WfsGetInfo;
                    stateFields.Add(errinfo);
                }
                //TTU: 700000
                if (_wfsstate.ttu.status == iopcts.XFSAPIh.WFS_SUCCESS)
                {
                }
                else
                {
                    StateErrorInfo errinfo = new StateErrorInfo();
                    errinfo.errcode = ((700000) | ((Int32)(-_wfsstate.ttu.status)));
                    errinfo.status = TTULabels.WfsGetInfo;
                    stateFields.Add(errinfo);
                }
            }
            var stateErrors = stateFields.Where(sf => HasError(sf.status.type)).ToList();
            var errsList = new List<ErrTypeId>();
            if (stateErrors.Any())
            {
                //AtmView.Entities.AtmViewContext db2 = new AtmView.Entities.AtmViewContext();

                var ActionCorrectiveList = _ActionCorrectiveService.GetAll().ToList();
                var ActionCorrectiveGeneric = ActionCorrectiveList.FirstOrDefault();
#if false//multiple AC
                var ActionCorrectiveError = ActionCorrectiveList.Where(a => a.Name.ToLower().Contains("hard")).DefaultIfEmpty(ActionCorrectiveGeneric).First();
                var ActionCorrectiveAlert = ActionCorrectiveList.Where(a => a.Name.ToLower().Contains("consom")).DefaultIfEmpty(ActionCorrectiveGeneric).First();
                var ActionCorrectiveSecur = ActionCorrectiveList.Where(a => a.Name.ToLower().Contains("secur")).DefaultIfEmpty(ActionCorrectiveGeneric).First();
                var ActionCorrectiveXfs = ActionCorrectiveList.Where(a => a.Name.ToLower().Contains("xfs")).DefaultIfEmpty(ActionCorrectiveGeneric).First();
#endif//multiple AC
                foreach (var errinfo in stateErrors)
                {
                    var errCategList = db2.AtmErrTypes.Where(e => e.errcode == errinfo.errcode).ToList();
                    int errorkindid;
                    if (errCategList.Count() < 1)
                    {
                        //create entry:
                        int compidx = (errinfo.errcode / 100000);
                        string complabel = AtmLabels.COMPONENT[compidx];
                        var newErrCateg = new AtmView.Entities.ErrorType
                        {
                            errcode = errinfo.errcode
                            ,
                            label = (complabel + "-" + errinfo.status.label)//((compidx > 0) ?  : errinfo.status.label)
                            ,
                            descr = errinfo.status.descri
                            ,
                            actioncorrectivid = ActionCorrectiveGeneric.Id
                        };
#if false//multiple AC
                                if (compidx > 0)
                                {
                                    switch (errinfo.status.type)
                                    {
                                        case FState.error:
                                            newErrCateg.actioncorrectivid = ActionCorrectiveError.Id;
                                            break;
                                        case FState.warning:
                                            newErrCateg.actioncorrectivid = ActionCorrectiveAlert.Id;
                                            break;
                                        case FState.danger:
                                            newErrCateg.actioncorrectivid = ActionCorrectiveSecur.Id;
                                            break;
                                        case FState.softerr:
                                            newErrCateg.actioncorrectivid = ActionCorrectiveXfs.Id;
                                            break;
                                    }
                                }
#endif//multiple AC
                        db2.AtmErrTypes.Add(newErrCateg);
                        db2.SaveChanges();
                        errorkindid = newErrCateg.Id;
                    }
                    else
                    {
                        var foundErrCateg = errCategList.First();
                        errorkindid = foundErrCateg.Id;
#if DEBUG
                        if (true)
                        {
                            int compidx = (errinfo.errcode / 100000);
                            string complabel = AtmLabels.COMPONENT[compidx];
                            foundErrCateg.label = (complabel + "-" + errinfo.status.label)/*((compidx > 0) ?  : errinfo.status.label)*/;
                            db2.Entry(foundErrCateg).State = System.Data.Entity.EntityState.Modified;
                            db2.SaveChanges();
                        }
#endif//DEBUG
                    }
                    errsList.Add(new ErrTypeId(errorkindid));
                }
            }
            return errsList;
            bool HasError(FState type)
            {
                return ((type == FState.error) || (type == FState.danger) || (type == FState.softerr) || (type == FState.warning) /*|| (type == FState.unknown)*/);
            }
        }


        public StateResult2 HandleAtmBugs(string _atmId, int _stateid, int _atmerr_id, string _attachmentfile)
        {
            var result = new StateResult2();
            //AtmView.Entities.AtmViewContext db2 = new AtmView.Entities.AtmViewContext();
            var errstate = _StateService.FindBy(s => s.Id == _stateid).FirstOrDefault();
            if (true)
            {
                Entities.Atm atmEntity = db2.Atms.Where(a => (a.Id == _atmId)).FirstOrDefault();
                AtmError err = db2.AtmErrors.Find(_atmerr_id);
                if ((err == null) || (errstate == null) || (atmEntity == null))
                {
                    ExceptionLogger.Debug("HandleAtmBugs: prematurely  exit: " + _atmId + ", " + _stateid + ", " + _atmerr_id);
                    return result;
                }
            }
            var idslist = db2.ErrTypeIds.AsNoTracking()
                .Where(i => i.State_Id == _stateid)
                .Select(x => x.ErrorType_Id).ToList();
            var errtypes = db2.AtmErrTypes.AsNoTracking().Where(e => idslist.Contains(e.Id)).ToList();

            db2.ActionCorrectives.ToList().ForEach(ac => handleErrTypesAsync(ac));
            result.bugids = db2.Bugs.Where(b => ((b.AtmError_Id == _atmerr_id))).Select(x => x.Id).ToArray();
            if (result.bugids.Any())
            {
                ExceptionLogger.Error("HandleAtmBugs (" + _atmId + ", " + _stateid + ", " + _atmerr_id + ")");
                result.bugids.ToList().ForEach(b => ExceptionLogger.Info(b.ToString()));
            }
            else
            {
                ExceptionLogger.Error("No bugs created for " + _atmId + ", " + _stateid + ", " + _atmerr_id);
            }
            return result;

            /*async*/
            void handleErrTypesAsync(ActionCorrective ac)
            {
                var ErrTypesByAction = errtypes.Where(e => (e.actioncorrectivid == ac.Id)).ToList();
                if (ErrTypesByAction.Any())
                {
                    try
                    {
                        var acbugs = db2.Bugs.Where(b => ((b.AtmError_Id == _atmerr_id) && (b.ActionCorrective_Id == ac.Id))).ToList();
                        var acbug = acbugs.LastOrDefault();
                        if (acbugs.Any())
                        {
                            ExceptionLogger.Info("handleErrTypesAsync(" + ac.Name + ") ac bugs count: " + acbugs.Count);
                        }
                        else
                        {
                            ExceptionLogger.Info("handleErrTypesAsync(" + ac.Name + ") NewBug()");
                        }
                        //if (buglist.Count() > 1)
                        //{
                        //    throw new Exception("Duplication in (state) Bug Table !");
                        //}
                        //else
                        {
                            if (acbug == null)
                            {
                                acbug = NewBug();
                                db2.Bugs.Add(acbug);
                                db2.SaveChanges();
                                UpdateStateErrTypes(acbug.Id);
                                //send bug to ac.User_Id ?
                                ExceptionLogger.Info("NewBug() " + acbug.Id + ": ");
                                acbug.ErrTypeIds__.ToList().ForEach(i => ExceptionLogger.Info(i.ErrorType_Id + ", "));

                                //var raiseTest = new RaiseAlert();
                                //Alert alert = db2.Alerts.Find("IncidentGab");

                                //List<object> list = new List<object>();
                                //list.Add(alert);
                                //list.Add(acbug.Id);
                                //raiseTest.RaiseAlerts(3, list);
                                ExceptionLogger.Error("sendMail (" + ac.Name + ", " + acbug.Id+")");
                                SendIncidentEmail(ac, acbug.Id);
                            }
                            else//Update errortypes
                            {
                                acbug.LastUpdateDate = errstate.StateDate;
                                db2.Entry(acbug).State = System.Data.Entity.EntityState.Modified;
                                db2.SaveChanges();
                                UpdateStateErrTypes(acbug.Id);
#if DEBUG

                                //var raiseTest = new RaiseAlert();
                                //Alert alert = db2.Alerts.Find("IncidentGab");

                                //List<object> list = new List<object>();
                                //list.Add(alert);
                                //list.Add(acbug.Id);
                                //raiseTest.RaiseAlerts(3, list);
                                ExceptionLogger.Error("sendMail (" + ac.Name + ", " + acbug.Id + ")");

                                SendIncidentEmail(ac, acbug.Id);
#endif//
                            }
                            Bug NewBug()
                            {
                                var bug = new Bug();
                                //----------------------------------------------------------
                                var bugtitle = "";
                                ErrTypesByAction.ForEach(e => bugtitle += e.label + ", ");
                                bugtitle = bugtitle.Substring(0, bugtitle.Length - 2);
#if false//multiple AC
                                bug.Title = bugtitle + " (" + ac.Name + ")";
#else
                                bug.Title = bugtitle;
#endif//multiple AC
                                //----------------------------------------------------------
                                bug.AssignedUser = ac.User_Id;
                                //----------------------------------------------------------
                                bug.AtmError_Id = _atmerr_id;
                                bug.ActionCorrective_Id = ac.Id;
                                bug.BugAtms = new List<BugAtm>();
                                bug.BugAtms.Add(new BugAtm { Atm_Id = _atmId });
                                bug.BugComponents = new List<BugComponent>();
                                foreach (var item in errstate.ComponentStates.Where(c => (c.StateComponent_Id != 1)))
                                {
                                    bug.BugComponents.Add(new BugComponent { Component_Id = item.Component_Id });
                                }
                                bug.BugCategory_Id = 2;//Anomalie
                                bug.BugPriority_Id = 1;//Major
                                bug.BugStatut_Id = 1;//Nouveau
                                bug.Creator = null; //ac.User_Id;
                                bug.CreationDate = errstate.StateDate;
                                bug.LastUpdateDate = errstate.StateDate;
                                bug.BugComments = new List<BugComment>();
                                bug.BugHistories = new List<BugHistory>();
                                bug.BugAttachments = new List<BugAttachment>();
                                //
                                if (_attachmentfile != null)
                                {
                                    string filePath = Path.Combine(Server.MapPath("~/App_Data/uploads/" + _atmId + "/"), Path.GetFileName(_attachmentfile));
                                    var attchfileInfo = new FileInfo(filePath);
                                    if (attchfileInfo.Exists)
                                    {
                                        string contenttype = MediaTypeNames.Text.Plain;
                                        var attachment = new BugAttachment();
                                        //attachment.Bug_Id = bug.Id;
                                        //attachment.UserId = User.Identity.GetUserId();
                                        attachment.FileName = attchfileInfo.FullName;
                                        attachment.Attachment = ReadFileData(attchfileInfo.FullName, out contenttype);
                                        attachment.ContentType = contenttype;
                                        bug.BugAttachments.Add(attachment);
                                    }
                                }
                                return bug;
                            }
                            void UpdateStateErrTypes(int _bugid)
                            {
                                var erridsByAction = ErrTypesByAction.Select(x => x.Id).ToList();
                                var bugErrTypeIds = db2.ErrTypeIds.Where(i => (i.State_Id == _stateid) && erridsByAction.Contains(i.ErrorType_Id)).ToList();
                                bugErrTypeIds.ForEach(e => modifyentry(e));
                                db2.SaveChanges();
                                void modifyentry(ErrTypeId e)
                                {
                                    e.Bug_Id = _bugid;
                                    db2.Entry(e).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.Error(ex.Message);
                        ExceptionLogger.Error(ex.StackTrace);
                    }
                }
                //else
                //{
                //    if (ac == _ActionCorrectiveService.GetAll().FirstOrDefault())
                //    {
                //        ExceptionLogger.Error("No bugs created for " + _atmId + ", " + _stateid + ", " + _atmerr_id);
                //    }
                //}
            }
        }

        private byte[] ReadFileData(string attchfile, out string contenttype)
        {
            contenttype = MediaTypeNames.Text.Plain;
            FileInfo fileInfo = new FileInfo(attchfile);
            if (fileInfo.Exists)
            {
                FileStream fstream = new FileStream(attchfile, FileMode.Open, FileAccess.Read);
                int fileLength = (int)fstream.Length;
                byte[] fileData = new byte[fileLength];
                fstream.Read(fileData, 0, fileLength);
                fstream.Close();
                return fileData;
            }
            else
            {
                return null;
            }
        }

        private int InsertDoneOrder(State state)
        {
            //recuperer le  plus recent state de l atm , comparer le total de l objet CashStocks  (un state a normalment un seul cachstock)
            //si le total passé en param  est superieur a celui deja en base alors on alimente la table doneordder : 
            //  DoneOrder_Date  date alimlentation 
            //DoneOrder_Amount diference des totaux (param - dernier state)
            //DoneOrderState : selectionner le dernier order valid duatm => amount , si amount = DoneOrder_Amount alors state=OK, puis cloturer de la table order le dernier order de l atm qui est a l etat validé
            // si DoneOrder_Amount < amount alors state dificit puis mettre error dans l etat de la table order le dernier order de l atm qui est a l etat validé
            // si DoneOrder_Amount > amount alors state execdent puis mettre error dans l etat de la table order le dernier order de l atm qui est a l etat validé
            //CashProvider_Id reccuprer cette valeur depuis la table order 
            //AddedAstatemount  --> changer le nom vers initial amount : reccuperer le total depiis cashstock de state  
            //statebefioreid et after

            State recentSate = _StateService.GetAll(elt => elt.Atm_Id == state.Atm_Id).OrderByDescending(e => e.Id).FirstOrDefault();
            int recentTotal = 0;
            int paramTotal = 0;
            if (recentSate.CashStocks.FirstOrDefault().Total.HasValue)
                recentTotal = recentSate.CashStocks.FirstOrDefault().Total.Value;

            if (state.CashStocks.FirstOrDefault().Total.HasValue)
                paramTotal = state.CashStocks.FirstOrDefault().Total.Value;
            if (paramTotal > recentTotal)
            {
                //alimenter la table doneOrder
                DateTime doneOrderDate = DateTime.Now;
                int DoneOrderAmount = paramTotal - recentTotal;
                //string atm = state.Atm_Id;

                OrderState doneOrderState = OrderState.OK;
                // OrderState DoneOrderState { get; set; } // 0:OK ; 1: ERROR ; 2: ERROR WITH DEFICIT ; 3: ERROR WITH EXCEDENT ; 4: 
                var recentValidOrder = _OrderService.GetAll(elt => /*elt.Atm_Id == state.Atm_Id &&*/ elt.Orderstatut == statut.Validated).OrderByDescending(e => e.Id).FirstOrDefault();
                if (recentValidOrder != null)
                {
                    if (recentValidOrder.AtmAmount == DoneOrderAmount)
                    {
                        doneOrderState = OrderState.OK;
                        //cloturer de la table order le dernier order de l atm qui est a l etat validé
                        recentValidOrder.Orderstatut = statut.Closed;
                        _OrderService.Update(recentValidOrder);
                    }
                    else if (DoneOrderAmount < recentValidOrder.AtmAmount)
                    {
                        doneOrderState = OrderState.ERRORWITHDEFICIT;
                        //mettre error dans l etat de la table order le dernier order de l atm qui est a l etat validé
                        //todo ou mettre l etat erreur
                        //recentValidOrder.Orderstatut = statut.;


                    }
                    else
                    {
                        doneOrderState = OrderState.ERRORWITHEXCEDENT;
                        //mettre error dans l etat de la table order le dernier order de l atm qui est a l etat validé
                        //todo

                    }
                    //CashProvider_Id reccuprer cette valeur depuis la table order 
                    int cashProvider_Id = recentValidOrder.CashProvider_Id;
                    int AddedAmount = state.CashStocks.FirstOrDefault().Total.Value;
                    int stateBefore_Id = recentSate.Id;
                    //int stateAfter_Id = 0;

                    DoneOrder doneOrder = new DoneOrder
                    {
                        DoneOrder_Date = doneOrderDate,
                        DoneOrder_Amount = DoneOrderAmount,
                        DoneOrderState = doneOrderState,
                        Atm_Id = state.Atm_Id,
                        CashProvider_Id = cashProvider_Id,
                        AddedAmount = AddedAmount,
                        StateBefore_Id = stateBefore_Id,
                        StateAfter_Id = null,
                    };
                    _DoneOrderService.Create(doneOrder);
                    //reccuperer le dernier id de doneorder de l atm 
                    int doneOrderId = _DoneOrderService.GetAll(elt => elt.Atm_Id == state.Atm_Id).OrderByDescending(e => e.Id).FirstOrDefault().Id;

                    return doneOrderId;
                }
                else
                {
                    return 0;
                }

            }
            return 0;
        }

        private bool SendCashAlert(Atm atm, CashStock cashstock, bool _cashok)
        {
            try
            {
                var devicename = atm.Id + "--" + atm.Name;
                string Body = string.Empty;
                try
                {
                    var tplpath = Server.MapPath("~/Views/Templates/CashEmailTpl.html");
                    if (_cashok)
                    {
                        tplpath = Server.MapPath("~/Views/Templates/CashEmailTplOk.html");
                    }
                    using (StreamReader reader = new StreamReader(tplpath))
                    {
                        Body = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Error(ex.Message);
                    ExceptionLogger.Error(ex.StackTrace);
                }

                string mail_subject = devicename + (_cashok ? " Cashstock Ok !" : " Cashstock alert !");
                Body = Body.Replace("{Atm_Id}", devicename);
                Body = Body.Replace("{CashStockTotal}", cashstock.Total.ToString());
                var emails = (atm.AtmContacts != null) ? atm.AtmContacts.Select(ac => ac.Contact.Email).ToList() : null;
                return SendEmail(mail_subject, Body, true, null, emails);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Error(ex.Message);
                ExceptionLogger.Error(ex.StackTrace);
                return false;
            }
        }


        private bool SendInServiceEmail(Atm _atm)
        {
            try
            {
                string Body = string.Empty;
                try
                {
                    var tplpath = Server.MapPath("~/Views/Templates/InServiceEmailTpl.html");
                    using (StreamReader reader = new StreamReader(tplpath))
                    {
                        Body = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Error(ex.Message);
                    ExceptionLogger.Error(ex.StackTrace);
                }
                var atm_name = _atm.Id + "--" + _atm.Name;
                string mail_subject = atm_name + " In Service !";
                Body = Body.Replace("{Atm_Id}", atm_name);
                var emails = (_atm.AtmContacts != null) ? _atm.AtmContacts.Select(ac => ac.Contact.Email).ToList() : null;
                return SendEmail(mail_subject, Body, true, null, emails);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Error(ex.Message);
                ExceptionLogger.Error(ex.StackTrace);
                return false;
            }
        }
        private void SendIncidentEmail(AtmView.Entities.ActionCorrective _ac, int _bugid)
        {
            try
            {
                AtmView.Entities.Bug _bug = db2.Bugs.AsNoTracking().Where(b => b.Id == _bugid).FirstOrDefault();
                string Body = string.Empty;
                try
                {
#if false
            string Body = "<html><div> < h3 > Automatique Incident Alert : </h3>       < span > bonjour</span>            < br />< br />< strong > CE GAB est EN ERREUR " + _bug.BugAtms.FirstOrDefault().Atm_Id;
            Body = Body + "</strong>< br /> < br />< br />< b > the following error is detected : #ErrorDesc#</b>                         < br />< br /> < b > Please execute this action to solve this error: #Action#</b> < span > Merci de ne pas réponde à l'adresse ci-dessus.</span>< br />< strong > Email envoyé automatique par l'équipe Back Office.</strong>< br />< br /></ div ></html> ";
            String errordes = _bug.ActionCorrective.Description;
            Body = Body.Replace("#Action#", errordes).Replace("#ErrorDesc#",_ac.Name+_ac.Description);
//#else
//                    var tplpath = Server.MapPath("~/Views/Templates/IncidentEmailTpl.html");
//                    using (StreamReader reader = new StreamReader(tplpath))
//                    {
//                        Body = reader.ReadToEnd();
//                    }
#endif
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Error(ex.Message);
                    ExceptionLogger.Error(ex.StackTrace);
                }
                var bgatm = _bug.BugAtms.FirstOrDefault();
                //foreach (var bgatm in _bug.BugAtms)
                {
                    var atm = bgatm.Atm;
                    string mail_subject = atm.Id + "--" + atm.Name + " Hors Service !";

                    var errtypestable = "";
                    try
                    {
                        var ids = _bug.ErrTypeIds__.Where(y => y.State_Id == _bug.AtmError.State_Id).Select(i => i.ErrorType_Id).ToList();
                        var errtypes = db2.AtmErrTypes.Where(e => ids.Contains(e.Id)).ToList();
                        errtypes.ForEach(e => errtypestable += "<tr class='danger'> <td>" + e.label + "</td> <td>" + e.descr + "</td> </tr>");
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.Error(ex.Message);
                        ExceptionLogger.Error(ex.StackTrace);
                    }
                    var ComponentsWithError = "";
                    try
                    {
                        _bug.BugComponents.ToList().ForEach(c => ComponentsWithError += c.Component.Label + ", ");
                        if (!string.IsNullOrWhiteSpace(ComponentsWithError))
                        {//suppress last ', '
                            ComponentsWithError = ComponentsWithError.Substring(0, ComponentsWithError.Length - 2);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.Error(ex.Message);
                        ExceptionLogger.Error(ex.StackTrace);
                    }


                    //Body = Body.Replace("{Atm_Id}", atm.Id + "--" + atm.Name);
                    //Body = Body.Replace("{ComponentsWithError}", ComponentsWithError);
                    //Body = Body.Replace("{ErrorsTypes}", errtypestable);
                    //Body = Body.Replace("{Atm_State_Link}", BaseUrl + "/AtmMgt/Detail?atmId=" + atm.Id);
                    //Body = Body.Replace("{bug_id}", _bug.Id.ToString());
                    //Body = Body.Replace("{bug_link}", BaseUrl + "/AVAtmBugTracker/Bug/UpdateBug/" + _bug.Id);
#if true
                    System.Net.Mail.Attachment attachment = null;
                    var battachment = _bug.BugAttachments.FirstOrDefault();
                    if (battachment != null)
                    {
                        var attchfileInfo = new FileInfo(battachment.FileName);
                        if (attchfileInfo.Exists)
                        {
                            attachment = new System.Net.Mail.Attachment(battachment.FileName);
                        }
                    }
#endif
                    var emails = (atm.AtmContacts != null) ? atm.AtmContacts.Select(ac => ac.Contact.Email).ToList() : null;
                    ExceptionLogger.Error("start get alert");

                    var raisAlert = new RaiseAlert(db2);
                    var alert = db2.Alerts.Find("IncidentGab");
                    alert.Name = "GAB: " + atm.Name + " " + alert.Name;
                    alert.Template = db2.Templates.Find(3);
                    ExceptionLogger.Error("get alert");

                    var list = new List<object>();
                    var path = System.AppDomain.CurrentDomain.BaseDirectory + alert.Template.Path;
                    var msg = System.IO.File.ReadAllText(path);
                    ExceptionLogger.Error("get template");

                    msg = msg.Replace("{Atm_Id}", atm.Id + "--" + atm.Name);
                    msg = msg.Replace("{ComponentsWithError}", ComponentsWithError);
                    msg = msg.Replace("{ErrorsTypes}", errtypestable);
                    msg = msg.Replace("{Atm_State_Link}", BaseUrl + "/AtmMgt/Detail?atmId=" + atm.Id);
                    msg = msg.Replace("{bug_id}", _bug.Id.ToString());
                    list.Add(alert);
                    list.Add(msg);
                    list.Add(emails);
                    ExceptionLogger.Error("befor raise alert");
                    raisAlert.RaiseAlerts(3, list);
                    //raisAlert.SendMail(mail_subject, Body, true, emails);

                    //SendEmail(mail_subject, Body, true, attachment, emails);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Error(ex.Message);
                ExceptionLogger.Error(ex.StackTrace);
            }
        }
        private bool SendEmail(string mail_subject, string body, bool _IsBodyHtml, System.Net.Mail.Attachment _attachment, List<string> emails)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = null;
            var bankname = WebConfigurationManager.AppSettings["BANKNAME"];
            if (string.IsNullOrWhiteSpace(bankname))
            {
#if DEBUG
                bankname = "atmview";
#else//DEBUG
                bankname = "BURKINACORIS";
#endif//DEBUG
            }
            switch (bankname.ToUpper())
            {
                case "BURKINACORIS":
                    SmtpServer = new SmtpClient("131.107.0.180", 587);
                    SmtpServer.Credentials = new System.Net.NetworkCredential("cbibf1@coris-bank.com", "Ouaga123");
                    SmtpServer.Port = 587;
                    //SmtpServer.EnableSsl = false;
                    mail.From = new MailAddress("cbibf1@coris-bank.com");
                    //mail.CC.Add("mba@coris-bank.com");
                    //mail.CC.Add("msavadogo@coris-bank.com");
                    //mail.CC.Add("ecoulibaly@coris-bank.com");
                    //mail.CC.Add("altoure@coris-bank.com");
                    break;
                case "BPMMAURITANIE":
                    SmtpServer = new SmtpClient("30.30.1.42", 25);
                    SmtpServer.Credentials = new System.Net.NetworkCredential("atm-view@bpm.mr", "@tmView!2020");
                    SmtpServer.Port = 25;
                    //SmtpServer.EnableSsl = false;
                    mail.From = new MailAddress("atm-view@bpm.mr");
                    //mail.CC.Add("mba@coris-bank.com");
                    //mail.CC.Add("msavadogo@coris-bank.com");
                    //mail.CC.Add("ecoulibaly@coris-bank.com");
                   // mail.Bcc.Add("support@atm-view.com");
                    break;
                case "ATMVIEW":
                default:
                    SmtpServer = new SmtpClient("smtpout.europe.secureserver.net");//
                    //SmtpServer.Port = 80;
                    SmtpServer.Port = 587;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("mohamed.douayou@atm-view.com", "WincorMDO.01");
                    SmtpServer.Credentials = new System.Net.NetworkCredential("info@atm-view.com", "Aa123456.");
                    SmtpServer.EnableSsl = false;
                    //mail.From = new MailAddress("mohamed.douayou@atm-view.com");
                    mail.From = new MailAddress("info@atm-view.com");
                    //mail.To.Add("mohamed.douayou@atm-view.com");
                    //mail.To.Add("houcine.khabir@atm-view.com");
                    mail.To.Add("ayoub.httater@atm-view.com");
                    break;
            }//switch
            if (SmtpServer != null)
            {
#if !DEBUG
                mail.Bcc.Add("houcine.khabir@atm-view.com");
                mail.Bcc.Add("mohamed.douayou@atm-view.com");
#endif//DEBUG
                if (emails != null)
                {
                    emails.ForEach(em => mail.To.Add(em));
                }
                mail.Subject = mail_subject;
                mail.Body = body;
                mail.IsBodyHtml = _IsBodyHtml;
                if (_attachment != null)
                {
                    mail.Attachments.Add(_attachment);
                }
                try
                {
                    SmtpServer.Send(mail);
                    ExceptionLogger.Info("Email (" + mail.Subject + ") sent ! to:");
                    mail.To.ToList().ForEach(to => ExceptionLogger.Info(to.Address));
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Error(ex.Message);
                    ExceptionLogger.Error(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                ExceptionLogger.Error("smtpsernver null");
                return false;
            }
        }
        //private bool DatesAreInTheSameWeek(DateTime date1, DateTime date2)
        //{
        //    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
        //    var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
        //    var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));
        //    return d1 == d2;
        //}
        //internal static readonly uint CDM = (0x1 << 0);
        //internal static readonly uint IDC = (0x1 << 1);
        //internal static readonly uint JRN = (0x1 << 2);
        //internal static readonly uint PIN = (0x1 << 3);
        //internal static readonly uint RCP = (0x1 << 4);
        //internal static readonly uint SIU = (0x1 << 5);
        //internal static readonly uint TTU = (0x1 << 6);
        //internal static readonly uint CASH = (0x1 << 7);
        private static readonly string BaseUrl = WebConfigurationManager.AppSettings["BaseUrl"];

        //private ApplicationUserManager _userManager;
        //public ApplicationUserManager UserManager
        //{
        //    get => _userManager ?? System.Web.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
    }

    internal class StateErrorInfo
    {
        internal int errcode;
        internal AtmStateLib.StateDescript status;
    }
}
