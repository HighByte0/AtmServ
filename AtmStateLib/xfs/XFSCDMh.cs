/******************************************************************************
*                                                                             *
* xfscdm.h      XFS - Cash Dispenser = (CDM); definitions                        *
*                                                                             *
*               Version 3.20  = (March 02 2011);                                 *
*                                                                             *
******************************************************************************/

//#ifndef __INC_XFSCDM__H
//public const int __INC_XFSCDM__H

//#ifdef __cplusplus
//extern "C" {
//#endif

//#include "XFSAPI.H"

///* be aware of alignment */
//#pragma pack = (push, 1);

namespace iopcts
{
    public class XFSCDMh
    {


        /* values of WFSCDMCAPS.wClass */

        public const int WFS_SERVICE_CLASS_CDM = (3);
        public const int WFS_SERVICE_CLASS_VERSION_CDM = 0x1403; /* Version 3.20 */
        public const string WFS_SERVICE_CLASS_NAME_CDM = "CDM";

        public const int CDM_SERVICE_OFFSET = (WFS_SERVICE_CLASS_CDM * 100);

        /* CDM Info Commands */

        public const int WFS_INF_CDM_STATUS = (CDM_SERVICE_OFFSET + 1);
        public const int WFS_INF_CDM_CAPABILITIES = (CDM_SERVICE_OFFSET + 2);
        public const int WFS_INF_CDM_CASH_UNIT_INFO = (CDM_SERVICE_OFFSET + 3);
        public const int WFS_INF_CDM_TELLER_INFO = (CDM_SERVICE_OFFSET + 4);
        public const int WFS_INF_CDM_CURRENCY_EXP = (CDM_SERVICE_OFFSET + 6);
        public const int WFS_INF_CDM_MIX_TYPES = (CDM_SERVICE_OFFSET + 7);
        public const int WFS_INF_CDM_MIX_TABLE = (CDM_SERVICE_OFFSET + 8);
        public const int WFS_INF_CDM_PRESENT_STATUS = (CDM_SERVICE_OFFSET + 9);

        /* CDM Execute Commands */

        public const int WFS_CMD_CDM_DENOMINATE = (CDM_SERVICE_OFFSET + 1);
        public const int WFS_CMD_CDM_DISPENSE = (CDM_SERVICE_OFFSET + 2);
        public const int WFS_CMD_CDM_PRESENT = (CDM_SERVICE_OFFSET + 3);
        public const int WFS_CMD_CDM_REJECT = (CDM_SERVICE_OFFSET + 4);
        public const int WFS_CMD_CDM_RETRACT = (CDM_SERVICE_OFFSET + 5);
        public const int WFS_CMD_CDM_OPEN_SHUTTER = (CDM_SERVICE_OFFSET + 7);
        public const int WFS_CMD_CDM_CLOSE_SHUTTER = (CDM_SERVICE_OFFSET + 8);
        public const int WFS_CMD_CDM_SET_TELLER_INFO = (CDM_SERVICE_OFFSET + 9);
        public const int WFS_CMD_CDM_SET_CASH_UNIT_INFO = (CDM_SERVICE_OFFSET + 10);
        public const int WFS_CMD_CDM_START_EXCHANGE = (CDM_SERVICE_OFFSET + 11);
        public const int WFS_CMD_CDM_END_EXCHANGE = (CDM_SERVICE_OFFSET + 12);
        public const int WFS_CMD_CDM_OPEN_SAFE_DOOR = (CDM_SERVICE_OFFSET + 13);
        public const int WFS_CMD_CDM_CALIBRATE_CASH_UNIT = (CDM_SERVICE_OFFSET + 15);
        public const int WFS_CMD_CDM_SET_MIX_TABLE = (CDM_SERVICE_OFFSET + 20);
        public const int WFS_CMD_CDM_RESET = (CDM_SERVICE_OFFSET + 21);
        public const int WFS_CMD_CDM_TEST_CASH_UNITS = (CDM_SERVICE_OFFSET + 22);
        public const int WFS_CMD_CDM_COUNT = (CDM_SERVICE_OFFSET + 23);
        public const int WFS_CMD_CDM_SET_GUIDANCE_LIGHT = (CDM_SERVICE_OFFSET + 24);
        public const int WFS_CMD_CDM_POWER_SAVE_CONTROL = (CDM_SERVICE_OFFSET + 25);
        public const int WFS_CMD_CDM_PREPARE_DISPENSE = (CDM_SERVICE_OFFSET + 26);


        /* CDM Messages */
        //printf("public const int WFS_SRVE_CDM_SAFEDOOROPEN = %d", WFS_SRVE_CDM_SAFEDOOROPEN);// (CDM_SERVICE_OFFSET + 1);

        public const int WFS_SRVE_CDM_SAFEDOOROPEN = (CDM_SERVICE_OFFSET + 1);
        public const int WFS_SRVE_CDM_SAFEDOORCLOSED = (CDM_SERVICE_OFFSET + 2);
        public const int WFS_USRE_CDM_CASHUNITTHRESHOLD = (CDM_SERVICE_OFFSET + 3);
        public const int WFS_SRVE_CDM_CASHUNITINFOCHANGED = (CDM_SERVICE_OFFSET + 4);
        public const int WFS_SRVE_CDM_TELLERINFOCHANGED = (CDM_SERVICE_OFFSET + 5);
        public const int WFS_EXEE_CDM_DELAYEDDISPENSE = (CDM_SERVICE_OFFSET + 6);
        public const int WFS_EXEE_CDM_STARTDISPENSE = (CDM_SERVICE_OFFSET + 7);
        public const int WFS_EXEE_CDM_CASHUNITERROR = (CDM_SERVICE_OFFSET + 8);
        public const int WFS_SRVE_CDM_ITEMSTAKEN = (CDM_SERVICE_OFFSET + 9);
        public const int WFS_EXEE_CDM_PARTIALDISPENSE = (CDM_SERVICE_OFFSET + 10);
        public const int WFS_EXEE_CDM_SUBDISPENSEOK = (CDM_SERVICE_OFFSET + 11);
        public const int WFS_SRVE_CDM_ITEMSPRESENTED = (CDM_SERVICE_OFFSET + 13);
        public const int WFS_SRVE_CDM_COUNTS_CHANGED = (CDM_SERVICE_OFFSET + 14);
        public const int WFS_EXEE_CDM_INCOMPLETEDISPENSE = (CDM_SERVICE_OFFSET + 15);
        public const int WFS_EXEE_CDM_NOTEERROR = (CDM_SERVICE_OFFSET + 16);
        public const int WFS_SRVE_CDM_MEDIADETECTED = (CDM_SERVICE_OFFSET + 17);
        public const int WFS_EXEE_CDM_INPUT_P6 = (CDM_SERVICE_OFFSET + 18);
        public const int WFS_SRVE_CDM_DEVICEPOSITION = (CDM_SERVICE_OFFSET + 19);
        public const int WFS_SRVE_CDM_POWER_SAVE_CHANGE = (CDM_SERVICE_OFFSET + 20);


        /* values of WFSCDMSTATUS.fwDevice */

        public const int WFS_CDM_DEVONLINE = XFSAPIh.WFS_STAT_DEVONLINE;
        public const int WFS_CDM_DEVOFFLINE = XFSAPIh.WFS_STAT_DEVOFFLINE;
        public const int WFS_CDM_DEVPOWEROFF = XFSAPIh.WFS_STAT_DEVPOWEROFF;
        public const int WFS_CDM_DEVNODEVICE = XFSAPIh.WFS_STAT_DEVNODEVICE;
        public const int WFS_CDM_DEVHWERROR = XFSAPIh.WFS_STAT_DEVHWERROR;
        public const int WFS_CDM_DEVUSERERROR = XFSAPIh.WFS_STAT_DEVUSERERROR;
        public const int WFS_CDM_DEVBUSY = XFSAPIh.WFS_STAT_DEVBUSY;
        public const int WFS_CDM_DEVFRAUDATTEMPT = XFSAPIh.WFS_STAT_DEVFRAUDATTEMPT;
        public const int WFS_CDM_DEVPOTENTIALFRAUD = XFSAPIh.WFS_STAT_DEVPOTENTIALFRAUD;

        /* values of WFSCDMSTATUS.fwSafeDoor */

        public const int WFS_CDM_DOORNOTSUPPORTED = (1);
        public const int WFS_CDM_DOOROPEN = (2);
        public const int WFS_CDM_DOORCLOSED = (3);
        public const int WFS_CDM_DOORUNKNOWN = (5);

        /* values of WFSCDMSTATUS.fwDispenser */

        public const int WFS_CDM_DISPOK = (0);
        public const int WFS_CDM_DISPCUSTATE = (1);
        public const int WFS_CDM_DISPCUSTOP = (2);
        public const int WFS_CDM_DISPCUUNKNOWN = (3);

        /* values of WFSCDMSTATUS.fwIntermediateStacker */

        public const int WFS_CDM_ISEMPTY = (0);
        public const int WFS_CDM_ISNOTEMPTY = (1);
        public const int WFS_CDM_ISNOTEMPTYCUST = (2);
        public const int WFS_CDM_ISNOTEMPTYUNK = (3);
        public const int WFS_CDM_ISUNKNOWN = (4);
        public const int WFS_CDM_ISNOTSUPPORTED = (5);

        /* Size and max index of dwGuidLights array */

        public const int WFS_CDM_GUIDLIGHTS_SIZE = (32);
        public const int WFS_CDM_GUIDLIGHTS_MAX = (WFS_CDM_GUIDLIGHTS_SIZE - 1);

        /* Indices of WFSCDMSTATUS.dwGuidLights [...]
                      WFSCDMCAPS.dwGuidLights [...]
        */

        public const int WFS_CDM_GUIDANCE_POSOUTNULL = (0);
        public const int WFS_CDM_GUIDANCE_POSOUTLEFT = (1);
        public const int WFS_CDM_GUIDANCE_POSOUTRIGHT = (2);
        public const int WFS_CDM_GUIDANCE_POSOUTCENTER = (3);
        public const int WFS_CDM_GUIDANCE_POSOUTTOP = (4);
        public const int WFS_CDM_GUIDANCE_POSOUTBOTTOM = (5);
        public const int WFS_CDM_GUIDANCE_POSOUTFRONT = (6);
        public const int WFS_CDM_GUIDANCE_POSOUTREAR = (7);

        /* Values of WFSCDMSTATUS.dwGuidLights [...]
                     WFSCDMCAPS.dwGuidLights [...]
        */
        public const int WFS_CDM_GUIDANCE_OFF = (0x00000001);
        public const int WFS_CDM_GUIDANCE_SLOW_FLASH = (0x00000004);
        public const int WFS_CDM_GUIDANCE_MEDIUM_FLASH = (0x00000008);
        public const int WFS_CDM_GUIDANCE_QUICK_FLASH = (0x00000010);
        public const int WFS_CDM_GUIDANCE_CONTINUOUS = (0x00000080);
        public const int WFS_CDM_GUIDANCE_RED = (0x00000100);
        public const int WFS_CDM_GUIDANCE_GREEN = (0x00000200);
        public const int WFS_CDM_GUIDANCE_YELLOW = (0x00000400);
        public const int WFS_CDM_GUIDANCE_BLUE = (0x00000800);
        public const int WFS_CDM_GUIDANCE_CYAN = (0x00001000);
        public const int WFS_CDM_GUIDANCE_MAGENTA = (0x00002000);
        public const int WFS_CDM_GUIDANCE_WHITE = (0x00004000);

        /* Values of WFSCDMSTATUS.dwGuidLights [...]
                     WFSCDMCAPS.dwGuidLights [...]
        */
        public const int WFS_CDM_GUIDANCE_NOT_AVAILABLE = (0x0000);

        /* values of WFSCDMSTATUS.wDevicePosition
                     WFSCDMDEVICEPOSITION.wPosition */

        public const int WFS_CDM_DEVICEINPOSITION = (0);
        public const int WFS_CDM_DEVICENOTINPOSITION = (1);
        public const int WFS_CDM_DEVICEPOSUNKNOWN = (2);
        public const int WFS_CDM_DEVICEPOSNOTSUPP = (3);


        /* values of WFSCDMOUTPOS.fwShutter */

        public const int WFS_CDM_SHTCLOSED = (0);
        public const int WFS_CDM_SHTOPEN = (1);
        public const int WFS_CDM_SHTJAMMED = (2);
        public const int WFS_CDM_SHTUNKNOWN = (3);
        public const int WFS_CDM_SHTNOTSUPPORTED = (4);

        /* values of WFSCDMOUTPOS.fwPositionStatus */

        public const int WFS_CDM_PSEMPTY = (0);
        public const int WFS_CDM_PSNOTEMPTY = (1);
        public const int WFS_CDM_PSUNKNOWN = (2);
        public const int WFS_CDM_PSNOTSUPPORTED = (3);

        /* values of WFSCDMOUTPOS.fwTransport */

        public const int WFS_CDM_TPOK = (0);
        public const int WFS_CDM_TPINOP = (1);
        public const int WFS_CDM_TPUNKNOWN = (2);
        public const int WFS_CDM_TPNOTSUPPORTED = (3);

        /* values of WFSCDMOUTPOS.fwTransportStatus */

        public const int WFS_CDM_TPSTATEMPTY = (0);
        public const int WFS_CDM_TPSTATNOTEMPTY = (1);
        public const int WFS_CDM_TPSTATNOTEMPTYCUST = (2);
        public const int WFS_CDM_TPSTATNOTEMPTY_UNK = (3);
        public const int WFS_CDM_TPSTATNOTSUPPORTED = (4);


        /* values of WFSCDMCAPS.fwType */

        public const int WFS_CDM_TELLERBILL = (0);
        public const int WFS_CDM_SELFSERVICEBILL = (1);
        public const int WFS_CDM_TELLERCOIN = (2);
        public const int WFS_CDM_SELFSERVICECOIN = (3);

        /* values of WFSCDMCAPS.fwRetractAreas */
        /* values of WFSCDMRETRACT.usRetractArea */

        public const int WFS_CDM_RA_RETRACT = (0x0001);
        public const int WFS_CDM_RA_TRANSPORT = (0x0002);
        public const int WFS_CDM_RA_STACKER = (0x0004);
        public const int WFS_CDM_RA_REJECT = (0x0008);
        public const int WFS_CDM_RA_NOTSUPP = (0x0010);
        public const int WFS_CDM_RA_ITEMCASSETTE = (0x0020);

        /* values of WFSCDMCAPS.fwRetractTransportActions */
        /* values of WFSCDMCAPS.fwRetractStackerActions */

        public const int WFS_CDM_PRESENT = (0x0001);
        public const int WFS_CDM_RETRACT = (0x0002);
        public const int WFS_CDM_REJECT = (0x0004);
        public const int WFS_CDM_NOTSUPP = (0x0008);
        public const int WFS_CDM_ITEMCASSETTE = (0x0010);

        /* values of WFSCDMCAPS.fwMoveItems */

        public const int WFS_CDM_FROMCU = (0x0001);
        public const int WFS_CDM_TOCU = (0x0002);
        public const int WFS_CDM_TOTRANSPORT = (0x0004);

        /* values of WFSCDMCASHUNIT.usType */

        public const int WFS_CDM_TYPENA = (1);
        public const int WFS_CDM_TYPEREJECTCASSETTE = (2);
        public const int WFS_CDM_TYPEBILLCASSETTE = (3);
        public const int WFS_CDM_TYPECOINCYLINDER = (4);
        public const int WFS_CDM_TYPECOINDISPENSER = (5);
        public const int WFS_CDM_TYPERETRACTCASSETTE = (6);
        public const int WFS_CDM_TYPECOUPON = (7);
        public const int WFS_CDM_TYPEDOCUMENT = (8);
        public const int WFS_CDM_TYPEREPCONTAINER = (11);
        public const int WFS_CDM_TYPERECYCLING = (12);

        /* values of WFSCDMCASHUNIT.usStatus */

        public const int WFS_CDM_STATCUOK = (0);
        public const int WFS_CDM_STATCUFULL = (1);
        public const int WFS_CDM_STATCUHIGH = (2);
        public const int WFS_CDM_STATCULOW = (3);
        public const int WFS_CDM_STATCUEMPTY = (4);
        public const int WFS_CDM_STATCUINOP = (5);
        public const int WFS_CDM_STATCUMISSING = (6);
        public const int WFS_CDM_STATCUNOVAL = (7);
        public const int WFS_CDM_STATCUNOREF = (8);
        public const int WFS_CDM_STATCUMANIP = (9);

        /* values of WFSCDMMIXTYPE.usMixType */

        public const int WFS_CDM_MIXALGORITHM = (1);
        public const int WFS_CDM_MIXTABLE = (2);

        /* values of WFSCDMMIXTYPE.usMixNumber */

        public const int WFS_CDM_INDIVIDUAL = (0);

        /* values of WFSCDMMIXTYPE.usSubType = (predefined mix algorithms); */

        public const int WFS_CDM_MIX_MINIMUM_NUMBER_OF_BILLS = (1);
        public const int WFS_CDM_MIX_EQUAL_EMPTYING_OF_CASH_UNITS = (2);
        public const int WFS_CDM_MIX_MAXIMUM_NUMBER_OF_CASH_UNITS = (3);

        /* values of WFSCDMPRESENTSTATUS.wPresentState */

        public const int WFS_CDM_PRESENTED = (1);
        public const int WFS_CDM_NOTPRESENTED = (2);
        public const int WFS_CDM_UNKNOWN = (3);

        /* values of WFSCDMDISPENSE.fwPosition */
        /* values of WFSCDMCAPS.fwPositions */
        /* values of WFSCDMOUTPOS.fwPosition */
        /* values of WFSCDMTELLERDETAILS.fwOutputPosition */
        /* values of WFSCDMPHYSICALCU.fwPosition */

        public const int WFS_CDM_POSNULL = (0x0000);
        public const int WFS_CDM_POSLEFT = (0x0001);
        public const int WFS_CDM_POSRIGHT = (0x0002);
        public const int WFS_CDM_POSCENTER = (0x0004);
        public const int WFS_CDM_POSTOP = (0x0040);
        public const int WFS_CDM_POSBOTTOM = (0x0080);
        public const int WFS_CDM_POSFRONT = (0x0800);
        public const int WFS_CDM_POSREAR = (0x1000);

        /* additional values of WFSCDMPHYSICALCU.fwPosition */
        public const int WFS_CDM_POSREJECT = (0x0100);

        /* values of WFSCDMTELLERDETAILS.ulInputPosition */

        public const int WFS_CDM_POSINLEFT = (0x0001);
        public const int WFS_CDM_POSINRIGHT = (0x0002);
        public const int WFS_CDM_POSINCENTER = (0x0004);
        public const int WFS_CDM_POSINTOP = (0x0008);
        public const int WFS_CDM_POSINBOTTOM = (0x0010);
        public const int WFS_CDM_POSINFRONT = (0x0020);
        public const int WFS_CDM_POSINREAR = (0x0040);

        /* values of fwExchangeType */

        public const int WFS_CDM_EXBYHAND = (0x0001);
        public const int WFS_CDM_EXTOCASSETTES = (0x0002);

        /* values of WFSCDMTELLERUPDATE.usAction */

        public const int WFS_CDM_CREATE_TELLER = (1);
        public const int WFS_CDM_MODIFY_TELLER = (2);
        public const int WFS_CDM_DELETE_TELLER = (3);


        /* values of WFSCDMCUERROR.wFailure */

        public const int WFS_CDM_CASHUNITEMPTY = (1);
        public const int WFS_CDM_CASHUNITERROR = (2);
        public const int WFS_CDM_CASHUNITFULL = (4);
        public const int WFS_CDM_CASHUNITLOCKED = (5);
        public const int WFS_CDM_CASHUNITINVALID = (6);
        public const int WFS_CDM_CASHUNITCONFIG = (7);
        public const int WFS_CDM_CASHUNITNOTCONF = (8);


        /* values of lpusReason in WFS_EXEE_CDM_NOTEERROR */

        public const int WFS_CDM_DOUBLENOTEDETECTED = (1);
        public const int WFS_CDM_LONGNOTEDETECTED = (2);
        public const int WFS_CDM_SKEWEDNOTE = (3);
        public const int WFS_CDM_INCORRECTCOUNT = (4);
        public const int WFS_CDM_NOTESTOOCLOSE = (5);
        public const int WFS_CDM_OTHERNOTEERROR = (6);
        public const int WFS_CDM_SHORTNOTEDETECTED = (7);

        /* values of WFSCDMPREPAREDISPENSE.wAction */
        public const int WFS_CDM_START = (1);
        public const int WFS_CDM_STOP = (2);

        /* values of WFSCDMSTATUS.wAntiFraudModule */

        public const int WFS_CDM_AFMNOTSUPP = (0);
        public const int WFS_CDM_AFMOK = (1);
        public const int WFS_CDM_AFMINOP = (2);
        public const int WFS_CDM_AFMDEVICEDETECTED = (3);
        public const int WFS_CDM_AFMUNKNOWN = (4);

        /* XFS CDM Errors */

        public const int WFS_ERR_CDM_INVALIDCURRENCY = (-(CDM_SERVICE_OFFSET + 0));
        public const int WFS_ERR_CDM_INVALIDTELLERID = (-(CDM_SERVICE_OFFSET + 1));
        public const int WFS_ERR_CDM_CASHUNITERROR = (-(CDM_SERVICE_OFFSET + 2));
        public const int WFS_ERR_CDM_INVALIDDENOMINATION = (-(CDM_SERVICE_OFFSET + 3));
        public const int WFS_ERR_CDM_INVALIDMIXNUMBER = (-(CDM_SERVICE_OFFSET + 4));
        public const int WFS_ERR_CDM_NOCURRENCYMIX = (-(CDM_SERVICE_OFFSET + 5));
        public const int WFS_ERR_CDM_NOTDISPENSABLE = (-(CDM_SERVICE_OFFSET + 6));
        public const int WFS_ERR_CDM_TOOMANYITEMS = (-(CDM_SERVICE_OFFSET + 7));
        public const int WFS_ERR_CDM_UNSUPPOSITION = (-(CDM_SERVICE_OFFSET + 8));
        public const int WFS_ERR_CDM_SAFEDOOROPEN = (-(CDM_SERVICE_OFFSET + 10));
        public const int WFS_ERR_CDM_SHUTTERNOTOPEN = (-(CDM_SERVICE_OFFSET + 12));
        public const int WFS_ERR_CDM_SHUTTEROPEN = (-(CDM_SERVICE_OFFSET + 13));
        public const int WFS_ERR_CDM_SHUTTERCLOSED = (-(CDM_SERVICE_OFFSET + 14));
        public const int WFS_ERR_CDM_INVALIDCASHUNIT = (-(CDM_SERVICE_OFFSET + 15));
        public const int WFS_ERR_CDM_NOITEMS = (-(CDM_SERVICE_OFFSET + 16));
        public const int WFS_ERR_CDM_EXCHANGEACTIVE = (-(CDM_SERVICE_OFFSET + 17));
        public const int WFS_ERR_CDM_NOEXCHANGEACTIVE = (-(CDM_SERVICE_OFFSET + 18));
        public const int WFS_ERR_CDM_SHUTTERNOTCLOSED = (-(CDM_SERVICE_OFFSET + 19));
        public const int WFS_ERR_CDM_PRERRORNOITEMS = (-(CDM_SERVICE_OFFSET + 20));
        public const int WFS_ERR_CDM_PRERRORITEMS = (-(CDM_SERVICE_OFFSET + 21));
        public const int WFS_ERR_CDM_PRERRORUNKNOWN = (-(CDM_SERVICE_OFFSET + 22));
        public const int WFS_ERR_CDM_ITEMSTAKEN = (-(CDM_SERVICE_OFFSET + 23));
        public const int WFS_ERR_CDM_INVALIDMIXTABLE = (-(CDM_SERVICE_OFFSET + 27));
        public const int WFS_ERR_CDM_OUTPUTPOS_NOT_EMPTY = (-(CDM_SERVICE_OFFSET + 28));
        public const int WFS_ERR_CDM_INVALIDRETRACTPOSITION = (-(CDM_SERVICE_OFFSET + 29));
        public const int WFS_ERR_CDM_NOTRETRACTAREA = (-(CDM_SERVICE_OFFSET + 30));
        public const int WFS_ERR_CDM_NOCASHBOXPRESENT = (-(CDM_SERVICE_OFFSET + 33));
        public const int WFS_ERR_CDM_AMOUNTNOTINMIXTABLE = (-(CDM_SERVICE_OFFSET + 34));
        public const int WFS_ERR_CDM_ITEMSNOTTAKEN = (-(CDM_SERVICE_OFFSET + 35));
        public const int WFS_ERR_CDM_ITEMSLEFT = (-(CDM_SERVICE_OFFSET + 36));
        public const int WFS_ERR_CDM_INVALID_PORT = (-(CDM_SERVICE_OFFSET + 37));
        public const int WFS_ERR_CDM_POWERSAVETOOSHORT = (-(CDM_SERVICE_OFFSET + 38));
        public const int WFS_ERR_CDM_POWERSAVEMEDIAPRESENT = (-(CDM_SERVICE_OFFSET + 39));

        /*=================================================================*/
        /* CDM Info Command Structures */
        /*=================================================================*/

        //typedef struct _wfs_cdm_position
        //        {
        //            WORD fwPosition;
        //            WORD fwShutter;
        //            WORD fwPositionStatus;
        //            WORD fwTransport;
        //            WORD fwTransportStatus;
        //        } WFSCDMOUTPOS, * LPWFSCDMOUTPOS;

        //    typedef struct _wfs_cdm_status
        //        {
        //            WORD fwDevice;
        //            WORD fwSafeDoor;
        //            WORD fwDispenser;
        //            WORD fwIntermediateStacker;
        //            LPWFSCDMOUTPOS* lppPositions;
        //            LPSTR lpszExtra;
        //            DWORD dwGuidLights[WFS_CDM_GUIDLIGHTS_SIZE];
        //            WORD wDevicePosition;
        //            USHORT usPowerSaveRecoveryTime;
        //            WORD wAntiFraudModule;
        //        } WFSCDMSTATUS, * LPWFSCDMSTATUS;

        //    typedef struct _wfs_cdm_caps
        //        {
        //            WORD wClass;
        //            WORD fwType;
        //            WORD wMaxDispenseItems;
        //            BOOL bCompound;
        //            BOOL bShutter;
        //            BOOL bShutterControl;
        //            WORD fwRetractAreas;
        //            WORD fwRetractTransportActions;
        //            WORD fwRetractStackerActions;
        //            BOOL bSafeDoor;
        //            BOOL bCashBox;
        //            BOOL bIntermediateStacker;
        //            BOOL bItemsTakenSensor;
        //            WORD fwPositions;
        //            WORD fwMoveItems;
        //            WORD fwExchangeType;
        //            LPSTR lpszExtra;
        //            DWORD dwGuidLights[WFS_CDM_GUIDLIGHTS_SIZE];
        //            BOOL bPowerSaveControl;
        //            BOOL bPrepareDispense;
        //            BOOL bAntiFraudModule;
        //        } WFSCDMCAPS, * LPWFSCDMCAPS;

        //    typedef struct _wfs_cdm_physicalcu
        //        {
        //            LPSTR lpPhysicalPositionName;
        //            CHAR cUnitID[5];
        //            ULONG ulInitialCount;
        //            ULONG ulCount;
        //            ULONG ulRejectCount;
        //            ULONG ulMaximum;
        //            USHORT usPStatus;
        //            BOOL bHardwareSensor;
        //            ULONG ulDispensedCount;
        //            ULONG ulPresentedCount;
        //            ULONG ulRetractedCount;
        //        } WFSCDMPHCU, * LPWFSCDMPHCU;

        //    typedef struct _wfs_cdm_cashunit
        //        {
        //            USHORT usNumber;
        //            USHORT usType;
        //            LPSTR lpszCashUnitName;
        //            CHAR cUnitID[5];
        //            CHAR cCurrencyID[3];
        //            ULONG ulValues;
        //            ULONG ulInitialCount;
        //            ULONG ulCount;
        //            ULONG ulRejectCount;
        //            ULONG ulMinimum;
        //            ULONG ulMaximum;
        //            BOOL bAppLock;
        //            USHORT usStatus;
        //            USHORT usNumPhysicalCUs;
        //            LPWFSCDMPHCU* lppPhysical;
        //            ULONG ulDispensedCount;
        //            ULONG ulPresentedCount;
        //            ULONG ulRetractedCount;
        //        } WFSCDMCASHUNIT, * LPWFSCDMCASHUNIT;

        //    typedef struct _wfs_cdm_cu_info
        //        {
        //            USHORT usTellerID;
        //            USHORT usCount;
        //            LPWFSCDMCASHUNIT* lppList;
        //        } WFSCDMCUINFO, * LPWFSCDMCUINFO;

        //    typedef struct _wfs_cdm_teller_info
        //        {
        //            USHORT usTellerID;
        //            CHAR cCurrencyID[3];
        //        } WFSCDMTELLERINFO, * LPWFSCDMTELLERINFO;

        //    typedef struct _wfs_cdm_teller_totals
        //        {
        //            char cCurrencyID[3];
        //            ULONG ulItemsReceived;
        //            ULONG ulItemsDispensed;
        //            ULONG ulCoinsReceived;
        //            ULONG ulCoinsDispensed;
        //            ULONG ulCashBoxReceived;
        //            ULONG ulCashBoxDispensed;
        //        } WFSCDMTELLERTOTALS, * LPWFSCDMTELLERTOTALS;

        //    typedef struct _wfs_cdm_teller_details
        //        {
        //            USHORT usTellerID;
        //            ULONG ulInputPosition;
        //            WORD fwOutputPosition;
        //            LPWFSCDMTELLERTOTALS* lppTellerTotals;
        //        } WFSCDMTELLERDETAILS, * LPWFSCDMTELLERDETAILS;


        //    typedef struct _wfs_cdm_currency_exp
        //        {
        //            CHAR cCurrencyID[3];
        //            SHORT sExponent;
        //        } WFSCDMCURRENCYEXP, * LPWFSCDMCURRENCYEXP;

        //    typedef struct _wfs_cdm_mix_type
        //        {
        //            USHORT usMixNumber;
        //            USHORT usMixType;
        //            USHORT usSubType;
        //            LPSTR lpszName;
        //        } WFSCDMMIXTYPE, * LPWFSCDMMIXTYPE;

        //    typedef struct _wfs_cdm_mix_row
        //        {
        //            ULONG ulAmount;
        //            LPUSHORT lpusMixture;
        //        } WFSCDMMIXROW, * LPWFSCDMMIXROW;

        //    typedef struct _wfs_cdm_mix_table
        //        {
        //            USHORT usMixNumber;
        //            LPSTR lpszName;
        //            USHORT usRows;
        //            USHORT usCols;
        //            LPULONG lpulMixHeader;
        //            LPWFSCDMMIXROW* lppMixRows;
        //        } WFSCDMMIXTABLE, * LPWFSCDMMIXTABLE;

        //    typedef struct _wfs_cdm_denomination
        //        {
        //            CHAR cCurrencyID[3];
        //            ULONG ulAmount;
        //            USHORT usCount;
        //            LPULONG lpulValues;
        //            ULONG ulCashBox;
        //        } WFSCDMDENOMINATION, * LPWFSCDMDENOMINATION;

        //    typedef struct _wfs_cdm_present_status
        //        {
        //            LPWFSCDMDENOMINATION lpDenomination;
        //            WORD wPresentState;
        //            LPSTR lpszExtra;
        //        } WFSCDMPRESENTSTATUS, * LPWFSCDMPRESENTSTATUS;

        //    /*=================================================================*/
        //    /* CDM Execute Command Structures */
        //    /*=================================================================*/

        //    typedef struct _wfs_cdm_denominate
        //        {
        //            USHORT usTellerID;
        //            USHORT usMixNumber;
        //            LPWFSCDMDENOMINATION lpDenomination;
        //        } WFSCDMDENOMINATE, * LPWFSCDMDENOMINATE;

        //    typedef struct _wfs_cdm_dispense
        //        {
        //            USHORT usTellerID;
        //            USHORT usMixNumber;
        //            WORD fwPosition;
        //            BOOL bPresent;
        //            LPWFSCDMDENOMINATION lpDenomination;
        //        } WFSCDMDISPENSE, * LPWFSCDMDISPENSE;

        //    typedef struct _wfs_cdm_physical_cu
        //        {
        //            BOOL bEmptyAll;
        //            WORD fwPosition;
        //            LPSTR lpPhysicalPositionName;
        //        } WFSCDMPHYSICALCU, * LPWFSCDMPHYSICALCU;

        //    typedef struct _wfs_cdm_counted_phys_cu
        //        {
        //            LPSTR lpPhysicalPositionName;
        //            CHAR cUnitId[5];
        //            ULONG ulDispensed;
        //            ULONG ulCounted;
        //            USHORT usPStatus;
        //        } WFSCDMCOUNTEDPHYSCU, * LPWFSCDMCOUNTEDPHYSCU;

        //    typedef struct _wfs_cdm_count
        //        {
        //            USHORT usNumPhysicalCUs;
        //            LPWFSCDMCOUNTEDPHYSCU* lppCountedPhysCUs;
        //        } WFSCDMCOUNT, * LPWFSCDMCOUNT;


        //    typedef struct _wfs_cdm_retract
        //        {
        //            WORD fwOutputPosition;
        //            USHORT usRetractArea;
        //            USHORT usIndex;
        //        } WFSCDMRETRACT, * LPWFSCDMRETRACT;

        //    typedef struct _wfs_cdm_item_number
        //        {
        //            CHAR cCurrencyID[3];
        //            ULONG ulValues;
        //            USHORT usRelease;
        //            ULONG ulCount;
        //            USHORT usNumber;
        //        } WFSCDMITEMNUMBER, * LPWFSCDMITEMNUMBER;

        //    typedef struct _wfs_cdm_item_number_list
        //        {
        //            USHORT usNumOfItemNumbers;
        //            LPWFSCDMITEMNUMBER* lppItemNumber;
        //        } WFSCDMITEMNUMBERLIST, * LPWFSCDMITEMNUMBERLIST;

        //    typedef struct _wfs_cdm_teller_update
        //        {
        //            USHORT usAction;
        //            LPWFSCDMTELLERDETAILS lpTellerDetails;
        //        } WFSCDMTELLERUPDATE, * LPWFSCDMTELLERUPDATE;

        //    typedef struct _wfs_cdm_start_ex
        //        {
        //            WORD fwExchangeType;
        //            USHORT usTellerID;
        //            USHORT usCount;
        //            LPUSHORT lpusCUNumList;
        //        } WFSCDMSTARTEX, * LPWFSCDMSTARTEX;

        //    typedef struct _wfs_cdm_itemposition
        //        {
        //            USHORT usNumber;
        //            LPWFSCDMRETRACT lpRetractArea;
        //            WORD fwOutputPosition;
        //        } WFSCDMITEMPOSITION, * LPWFSCDMITEMPOSITION;

        //    typedef struct _wfs_cdm_calibrate
        //        {
        //            USHORT usNumber;
        //            USHORT usNumOfBills;
        //            LPWFSCDMITEMPOSITION* lpPosition;
        //        } WFSCDMCALIBRATE, * LPWFSCDMCALIBRATE;

        //    typedef struct _wfs_cdm_set_guidlight
        //        {
        //            WORD wGuidLight;
        //            DWORD dwCommand;
        //        } WFSCDMSETGUIDLIGHT, * LPWFSCDMSETGUIDLIGHT;

        //    typedef struct _wfs_cdm_power_save_control
        //        {
        //            USHORT usMaxPowerSaveRecoveryTime;
        //        } WFSCDMPOWERSAVECONTROL, * LPWFSCDMPOWERSAVECONTROL;


        //    typedef struct _wfs_cdm_prepare_dispense
        //        {
        //            WORD wAction;
        //        } WFSCDMPREPAREDISPENSE, * LPWFSCDMPREPAREDISPENSE;


        //    /*=================================================================*/
        //    /* CDM Message Structures */
        //    /*=================================================================*/

        //    typedef struct _wfs_cdm_cu_error
        //        {
        //            WORD wFailure;
        //            LPWFSCDMCASHUNIT lpCashUnit;
        //        } WFSCDMCUERROR, * LPWFSCDMCUERROR;

        //    typedef struct _wfs_cdm_counts_changed
        //        {
        //            USHORT usCount;
        //            LPUSHORT lpusCUNumList;
        //        } WFSCDMCOUNTSCHANGED, * LPWFSCDMCOUNTSCHANGED;

        //    typedef struct _wfs_cdm_device_position
        //        {
        //            WORD wPosition;
        //        } WFSCDMDEVICEPOSITION, * LPWFSCDMDEVICEPOSITION;

        //    typedef struct _wfs_cdm_power_save_change
        //        {
        //            USHORT usPowerSaveRecoveryTime;
        //        } WFSCDMPOWERSAVECHANGE, * LPWFSCDMPOWERSAVECHANGE;


    };//class
}//namespace



///* restore alignment */
//#pragma pack = (pop);

//#ifdef __cplusplus
//}       /*extern "C"*/
//#endif

//#endif  /* __INC_XFSCDM__H */
