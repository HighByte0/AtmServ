/******************************************************************************
*                                                                             *
* xfsttu.h      XFS - Text Terminal Unit (TTU) definitions                    *
*                                                                             *
*               Version 3.20  (March 02 2011)                                 *
*                                                                             *
******************************************************************************/

//#ifndef __INC_XFSTTU__H
//public const int __INC_XFSTTU__H

//#ifdef __cplusplus
//extern "C" {
//#endif

//#include "XFSAPI.H"

///*   be aware of alignment   */
//#pragma pack(push,1)

namespace iopcts
{
    public class XFSTTUh
    {

        /* values of WFSTTUCAPS.wClass */

        public const int WFS_SERVICE_CLASS_TTU = (7);
        public const string WFS_SERVICE_CLASS_NAME_TTU = "TTU";
        public const int WFS_SERVICE_CLASS_VERSION_TTU = (0x1403); /* Version 3.20 */

        public const int TTU_SERVICE_OFFSET = (WFS_SERVICE_CLASS_TTU * 100);

        /* TTU Info Commands */

        public const int WFS_INF_TTU_STATUS = (TTU_SERVICE_OFFSET + 1);
        public const int WFS_INF_TTU_CAPABILITIES = (TTU_SERVICE_OFFSET + 2);
        public const int WFS_INF_TTU_FORM_LIST = (TTU_SERVICE_OFFSET + 3);
        public const int WFS_INF_TTU_QUERY_FORM = (TTU_SERVICE_OFFSET + 4);
        public const int WFS_INF_TTU_QUERY_FIELD = (TTU_SERVICE_OFFSET + 5);
        public const int WFS_INF_TTU_KEY_DETAIL = (TTU_SERVICE_OFFSET + 6);

        /* TTU Command Verbs */

        public const int WFS_CMD_TTU_BEEP = (TTU_SERVICE_OFFSET + 1);
        public const int WFS_CMD_TTU_CLEARSCREEN = (TTU_SERVICE_OFFSET + 2);
        public const int WFS_CMD_TTU_DISPLIGHT = (TTU_SERVICE_OFFSET + 3);
        public const int WFS_CMD_TTU_SET_LED = (TTU_SERVICE_OFFSET + 4);
        public const int WFS_CMD_TTU_SET_RESOLUTION = (TTU_SERVICE_OFFSET + 5);
        public const int WFS_CMD_TTU_WRITE_FORM = (TTU_SERVICE_OFFSET + 6);
        public const int WFS_CMD_TTU_READ_FORM = (TTU_SERVICE_OFFSET + 7);
        public const int WFS_CMD_TTU_WRITE = (TTU_SERVICE_OFFSET + 8);
        public const int WFS_CMD_TTU_READ = (TTU_SERVICE_OFFSET + 9);
        public const int WFS_CMD_TTU_RESET = (TTU_SERVICE_OFFSET + 10);
        public const int WFS_CMD_TTU_DEFINE_KEYS = (TTU_SERVICE_OFFSET + 11);
        public const int WFS_CMD_TTU_POWER_SAVE_CONTROL = (TTU_SERVICE_OFFSET + 12);
        public const int WFS_CMD_TTU_SET_LED_EX = (TTU_SERVICE_OFFSET + 13);

        /* TTU Messages */

        public const int WFS_EXEE_TTU_FIELDERROR = (TTU_SERVICE_OFFSET + 1);
        public const int WFS_EXEE_TTU_FIELDWARNING = (TTU_SERVICE_OFFSET + 2);
        public const int WFS_EXEE_TTU_KEY = (TTU_SERVICE_OFFSET + 3);
        public const int WFS_SRVE_TTU_DEVICEPOSITION = (TTU_SERVICE_OFFSET + 4);
        public const int WFS_SRVE_TTU_POWER_SAVE_CHANGE = (TTU_SERVICE_OFFSET + 5);

        /* Values of WFSTTUSTATUS.fwDevice */

        public const int WFS_TTU_DEVONLINE = XFSAPIh.WFS_STAT_DEVONLINE;
        public const int WFS_TTU_DEVOFFLINE = XFSAPIh.WFS_STAT_DEVOFFLINE;
        public const int WFS_TTU_DEVPOWEROFF = XFSAPIh.WFS_STAT_DEVPOWEROFF;
        public const int WFS_TTU_DEVBUSY = XFSAPIh.WFS_STAT_DEVBUSY;
        public const int WFS_TTU_DEVNODEVICE = XFSAPIh.WFS_STAT_DEVNODEVICE;
        public const int WFS_TTU_DEVHWERROR = XFSAPIh.WFS_STAT_DEVHWERROR;
        public const int WFS_TTU_DEVUSERERROR = XFSAPIh.WFS_STAT_DEVUSERERROR;
        public const int WFS_TTU_DEVFRAUDATTEMPT = XFSAPIh.WFS_STAT_DEVFRAUDATTEMPT;
        public const int WFS_TTU_DEVPOTENTIALFRAUD = XFSAPIh.WFS_STAT_DEVPOTENTIALFRAUD;

        /* Values of WFSTTUSTATUS.wKeyboard */

        public const int WFS_TTU_KBDNA = (0);
        public const int WFS_TTU_KBDON = (1);
        public const int WFS_TTU_KBDOFF = (2);

        /* Values of WFSTTUSTATUS.wKeyLock */

        public const int WFS_TTU_KBDLOCKNA = (0);
        public const int WFS_TTU_KBDLOCKON = (1);
        public const int WFS_TTU_KBDLOCKOFF = (2);

        public const int WFS_TTU_LEDS_MAX = (8);

        /* Values of WFSTTUSTATUS.fwLEDs
                     WFSTTUSTATUS.lpLEDEx.lpdwLEDs
                     WFSTTUCAPS.lpLEDEx.lpdwLEDs
                     WFSTTUSETLEDS.fwCommand */

        public const int WFS_TTU_LEDNA = (0x0000);
        public const int WFS_TTU_LEDOFF = (0x0001);
        public const int WFS_TTU_LEDSLOWFLASH = (0x0002);
        public const int WFS_TTU_LEDMEDIUMFLASH = (0x0004);
        public const int WFS_TTU_LEDQUICKFLASH = (0x0008);
        public const int WFS_TTU_LEDCONTINUOUS = (0x0080);

        /* Values of WFSTTUSTATUS.lpLEDEx.lpdwLEDs
                     WFSTTUCAPS.lpLEDEx.lpdwLEDs
                     WFSTTUSETLEDSEX.dwCommand */

        public const int WFS_TTU_LEDRED = (0x00000100);
        public const int WFS_TTU_LEDGREEN = (0x00000200);
        public const int WFS_TTU_LEDYELLOW = (0x00000400);
        public const int WFS_TTU_LEDBLUE = (0x00000800);
        public const int WFS_TTU_LEDCYAN = (0x00001000);
        public const int WFS_TTU_LEDMAGENTA = (0x00002000);
        public const int WFS_TTU_LEDWHITE = (0x00004000);

        /* Values of WFSTTUSTATUS.wDevicePosition
                     WFSTTUDEVICEPOSITION.wPosition */

        public const int WFS_TTU_DEVICEINPOSITION = (0);
        public const int WFS_TTU_DEVICENOTINPOSITION = (1);
        public const int WFS_TTU_DEVICEPOSUNKNOWN = (2);
        public const int WFS_TTU_DEVICEPOSNOTSUPP = (3);

        /* values of WFSTTUSTATUS.wAntiFraudModule */

        public const int WFS_TTU_AFMNOTSUPP = (0);
        public const int WFS_TTU_AFMOK = (1);
        public const int WFS_TTU_AFMINOP = (2);
        public const int WFS_TTU_AFMDEVICEDETECTED = (3);
        public const int WFS_TTU_AFMUNKNOWN = (4);

        /* Values of WFSTTUCAPS.fwType */

        public const int WFS_TTU_FIXED = (0x0001);
        public const int WFS_TTU_REMOVABLE = (0x0002);

        /* Values of WFSTTUCAPS.fwCharSupport
                   WFSTTUWRITE.fwCharSupport */

        public const int WFS_TTU_ASCII = (0x0001);
        public const int WFS_TTU_UNICODE = (0x0002);

        /* Values of WFSTTUFRMFIELD.fwType */

        public const int WFS_TTU_FIELDTEXT = (0);
        public const int WFS_TTU_FIELDINVISIBLE = (1);
        public const int WFS_TTU_FIELDPASSWORD = (2);

        /* Values of WFSTTUFRMFIELD.fwClass */

        public const int WFS_TTU_CLASSOPTIONAL = (0);
        public const int WFS_TTU_CLASSSTATIC = (1);
        public const int WFS_TTU_CLASSREQUIRED = (2);

        /* Values of WFSTTUFRMFIELD.fwAccess */

        public const int WFS_TTU_ACCESSREAD = (0x0001);
        public const int WFS_TTU_ACCESSWRITE = (0x0002);

        /* Values of WFSTTUFRMFIELD.fwOverflow */

        public const int WFS_TTU_OVFTERMINATE = (0);
        public const int WFS_TTU_OVFTRUNCATE = (1);
        public const int WFS_TTU_OVFOVERWRITE = (2);

        /* Values of WFSTTUWRITE.fwMode */

        public const int WFS_TTU_POSRELATIVE = (0);
        public const int WFS_TTU_POSABSOLUTE = (1);

        /* Values of WFSTTUWRITE.fwTextAttr */

        public const int WFS_TTU_TEXTUNDERLINE = (0x0001);
        public const int WFS_TTU_TEXTINVERTED = (0x0002);
        public const int WFS_TTU_TEXTFLASH = (0x0004);

        /* Values of WFSTTUFRMREAD.fwEchoMode */

        public const int WFS_TTU_ECHOTEXT = (0);
        public const int WFS_TTU_ECHOINVISIBLE = (1);
        public const int WFS_TTU_ECHOPASSWORD = (2);

        public const int WFS_TTU_BEEPOFF = (0x0001);
        public const int WFS_TTU_BEEPKEYPRESS = (0x0002);
        public const int WFS_TTU_BEEPEXCLAMATION = (0x0004);
        public const int WFS_TTU_BEEPWARNING = (0x0008);
        public const int WFS_TTU_BEEPERROR = (0x0010);
        public const int WFS_TTU_BEEPCRITICAL = (0x0020);
        public const int WFS_TTU_BEEPCONTINUOUS = (0x0080);

        /* values of WFSTTUFIELDFAIL.wFailure */

        public const int WFS_TTU_FIELDREQUIRED = (0);
        public const int WFS_TTU_FIELDSTATICOVWR = (1);
        public const int WFS_TTU_FIELDOVERFLOW = (2);
        public const int WFS_TTU_FIELDNOTFOUND = (3);
        public const int WFS_TTU_FIELDNOTREAD = (4);
        public const int WFS_TTU_FIELDNOTWRITE = (5);
        public const int WFS_TTU_FIELDTYPENOTSUPPORTED = (6);
        public const int WFS_TTU_CHARSETFORM = (7);

        /* values of WFSTTUKEYDETAIL.lpwCommandKeys */

        public const int WFS_TTU_NOKEY = (0);
        public const int WFS_TTU_CK_ENTER = (1);
        public const int WFS_TTU_CK_CANCEL = (2);
        public const int WFS_TTU_CK_CLEAR = (3);
        public const int WFS_TTU_CK_BACKSPACE = (4);
        public const int WFS_TTU_CK_HELP = (5);
        public const int WFS_TTU_CK_00 = (6);
        public const int WFS_TTU_CK_000 = (7);
        public const int WFS_TTU_CK_ARROWUP = (8);
        public const int WFS_TTU_CK_ARROWDOWN = (9);
        public const int WFS_TTU_CK_ARROWLEFT = (10);
        public const int WFS_TTU_CK_ARROWRIGHT = (11);
        public const int WFS_TTU_CK_OEM1 = (12);
        public const int WFS_TTU_CK_OEM2 = (13);
        public const int WFS_TTU_CK_OEM3 = (14);
        public const int WFS_TTU_CK_OEM4 = (15);
        public const int WFS_TTU_CK_OEM5 = (16);
        public const int WFS_TTU_CK_OEM6 = (17);
        public const int WFS_TTU_CK_OEM7 = (18);
        public const int WFS_TTU_CK_OEM8 = (19);
        public const int WFS_TTU_CK_OEM9 = (20);
        public const int WFS_TTU_CK_OEM10 = (21);
        public const int WFS_TTU_CK_OEM11 = (22);
        public const int WFS_TTU_CK_OEM12 = (23);
        public const int WFS_TTU_CK_FDK01 = (24);
        public const int WFS_TTU_CK_FDK02 = (25);
        public const int WFS_TTU_CK_FDK03 = (26);
        public const int WFS_TTU_CK_FDK04 = (27);
        public const int WFS_TTU_CK_FDK05 = (28);
        public const int WFS_TTU_CK_FDK06 = (29);
        public const int WFS_TTU_CK_FDK07 = (30);
        public const int WFS_TTU_CK_FDK08 = (31);
        public const int WFS_TTU_CK_FDK09 = (32);
        public const int WFS_TTU_CK_FDK10 = (33);
        public const int WFS_TTU_CK_FDK11 = (34);
        public const int WFS_TTU_CK_FDK12 = (35);
        public const int WFS_TTU_CK_FDK13 = (36);
        public const int WFS_TTU_CK_FDK14 = (37);
        public const int WFS_TTU_CK_FDK15 = (38);
        public const int WFS_TTU_CK_FDK16 = (39);
        public const int WFS_TTU_CK_FDK17 = (40);
        public const int WFS_TTU_CK_FDK18 = (41);
        public const int WFS_TTU_CK_FDK19 = (42);
        public const int WFS_TTU_CK_FDK20 = (43);
        public const int WFS_TTU_CK_FDK21 = (44);
        public const int WFS_TTU_CK_FDK22 = (45);
        public const int WFS_TTU_CK_FDK23 = (46);
        public const int WFS_TTU_CK_FDK24 = (47);
        public const int WFS_TTU_CK_FDK25 = (48);
        public const int WFS_TTU_CK_FDK26 = (49);
        public const int WFS_TTU_CK_FDK27 = (50);
        public const int WFS_TTU_CK_FDK28 = (51);
        public const int WFS_TTU_CK_FDK29 = (52);
        public const int WFS_TTU_CK_FDK30 = (53);
        public const int WFS_TTU_CK_FDK31 = (54);
        public const int WFS_TTU_CK_FDK32 = (55);

        /* XFS TTU Errors */

        public const int WFS_ERR_TTU_FIELDERROR = (-(TTU_SERVICE_OFFSET + 1));
        public const int WFS_ERR_TTU_FIELDINVALID = (-(TTU_SERVICE_OFFSET + 2));
        public const int WFS_ERR_TTU_FIELDNOTFOUND = (-(TTU_SERVICE_OFFSET + 3));
        public const int WFS_ERR_TTU_FIELDSPECFAILURE = (-(TTU_SERVICE_OFFSET + 4));
        public const int WFS_ERR_TTU_FORMINVALID = (-(TTU_SERVICE_OFFSET + 5));
        public const int WFS_ERR_TTU_FORMNOTFOUND = (-(TTU_SERVICE_OFFSET + 6));
        public const int WFS_ERR_TTU_INVALIDLED = (-(TTU_SERVICE_OFFSET + 7));
        public const int WFS_ERR_TTU_KEYCANCELED = (-(TTU_SERVICE_OFFSET + 8));
        public const int WFS_ERR_TTU_MEDIAOVERFLOW = (-(TTU_SERVICE_OFFSET + 9));
        public const int WFS_ERR_TTU_RESNOTSUPP = (-(TTU_SERVICE_OFFSET + 10));
        public const int WFS_ERR_TTU_CHARSETDATA = (-(TTU_SERVICE_OFFSET + 11));
        public const int WFS_ERR_TTU_KEYINVALID = (-(TTU_SERVICE_OFFSET + 12));
        public const int WFS_ERR_TTU_KEYNOTSUPPORTED = (-(TTU_SERVICE_OFFSET + 13));
        public const int WFS_ERR_TTU_NOACTIVEKEYS = (-(TTU_SERVICE_OFFSET + 14));
        public const int WFS_ERR_TTU_POWERSAVETOOSHORT = (-(TTU_SERVICE_OFFSET + 15));

        ///*=================================================================*/
        ///* TTU Info Command Structures */
        ///*=================================================================*/

        //typedef struct _wfs_ttu_led_ex
        //{
        //    USHORT                usNumOfLEDs;
        //    LPDWORD               lpdwLEDs;
        //} WFSTTULEDEX, *LPWFSTTULEDEX;

        //typedef struct _wfs_ttu_status
        //{
        //    WORD                  fwDevice;
        //    WORD                  wKeyboard;
        //    WORD                  wKeylock;
        //    WORD                  wLEDs[WFS_TTU_LEDS_MAX];
        //    WORD                  wDisplaySizeX;
        //    WORD                  wDisplaySizeY;
        //    LPSTR                 lpszExtra;
        //    WORD                  wDevicePosition;
        //    USHORT                usPowerSaveRecoveryTime;
        //    LPWFSTTULEDEX         lpLEDEx;
        //    WORD                  wAntiFraudModule;
        //} WFSTTUSTATUS, *LPWFSTTUSTATUS;

        //typedef struct _wfs_ttu_resolution
        //{
        //    WORD                  wSizeX;
        //    WORD                  wSizeY;
        //} WFSTTURESOLUTION, *LPWFSTTURESOLUTION;

        //typedef struct _wfs_ttu_caps
        //{
        //    WORD                  wClass;
        //    WORD                  fwType;
        //    LPWFSTTURESOLUTION    *lppResolutions;
        //    WORD                  wNumOfLEDs;
        //    BOOL                  bKeyLock;
        //    BOOL                  bDisplayLight;
        //    BOOL                  bCursor;
        //    BOOL                  bForms;
        //    WORD                  fwCharSupport;
        //    LPSTR                 lpszExtra;
        //    BOOL                  bPowerSaveControl;
        //    LPWFSTTULEDEX         lpLEDEx;
        //    BOOL                  bAntiFraudModule;
        //} WFSTTUCAPS, *LPWFSTTUCAPS;

        //typedef struct _wfs_ttu_frm_header
        //{
        //    LPSTR                 lpszFormName;
        //    WORD                  wWidth;
        //    WORD                  wHeight;
        //    WORD                  wVersionMajor;
        //    WORD                  wVersionMinor;
        //    WORD                  fwCharSupport;
        //    LPSTR                 lpszFields;
        //    WORD                  wLanguageID;
        //} WFSTTUFRMHEADER, *LPWFSTTUFRMHEADER;

        //typedef struct _wfs_ttu_query_field
        //{
        //    LPSTR                 lpszFormName;
        //    LPSTR                 lpszFieldName;
        //} WFSTTUQUERYFIELD, *LPWFSTTUQUERYFIELD;

        //typedef struct _wfs_ttu_frm_field
        //{
        //    LPSTR                 lpszFieldName;
        //    WORD                  fwType;
        //    WORD                  fwClass;
        //    WORD                  fwAccess;
        //    WORD                  fwOverflow;
        //    LPSTR                 lpszFormat;
        //    WORD                  wLanguageID;
        //} WFSTTUFRMFIELD, *LPWFSTTUFRMFIELD;

        //typedef struct _wfs_ttu_key_detail
        //{
        //    LPSTR                 lpszKeys;
        //    LPWSTR                lpwszUNICODEKeys;
        //    LPWORD                lpwCommandKeys;
        //} WFSTTUKEYDETAIL, *LPWFSTTUKEYDETAIL;

        //typedef struct _wfs_ttu_clear_screen
        //{
        //    WORD                  wPositionX;
        //    WORD                  wPositionY;
        //    WORD                  wWidth;
        //    WORD                  wHeight;
        //} WFSTTUCLEARSCREEN, *LPWFSTTUCLEARSCREEN;

        //typedef struct _wfs_ttu_disp_light
        //{
        //    BOOL                  bMode;
        //} WFSTTUDISPLIGHT, * LPWFSTTUDISPLIGHT;

        //typedef struct _wfs_ttu_set_leds
        //{
        //    WORD                  wLED;
        //    WORD                  fwCommand;
        //} WFSTTUSETLEDS, *LPWFSTTUSETLEDS;

        //typedef struct _wfs_ttu_write_form
        //{
        //    LPSTR                 lpszFormName;
        //    BOOL                  bClearScreen;
        //    LPSTR                 lpszFields;
        //    LPWSTR                lpszUNICODEFields;
        //} WFSTTUWRITEFORM, *LPWFSTTUWRITEFORM;

        //typedef struct _wfs_ttu_read_form
        //{
        //    LPSTR                 lpszFormName;
        //    LPSTR                 lpszFieldNames;
        //} WFSTTUREADFORM, *LPWFSTTUREADFORM;

        //typedef struct _wfs_ttu_read_form_out
        //{
        //    LPSTR                 lpszFields;
        //    LPWSTR                lpszUNICODEFields;
        //} WFSTTUREADFORMOUT, *LPWFSTTUREADFORMOUT;

        //typedef struct _wfs_ttu_def_keys
        //{
        //    LPSTR                 lpszActiveKeys;
        //    LPWSTR                lpwszActiveUNICODEKeys;
        //    LPWORD                lpwActiveCommandKeys;
        //    LPWORD                lpwTerminateCommandKeys;
        //} WFSTTUDEFKEYS, *LPWFSTTUDEFKEYS;

        //typedef struct _wfs_ttu_write
        //{
        //    WORD                  fwMode;
        //    SHORT                 wPosX;
        //    SHORT                 wPosY;
        //    WORD                  fwTextAttr;
        //    LPSTR                 lpsText;
        //    LPWSTR                lpsUNICODEText;
        //} WFSTTUWRITE, *LPWFSTTUWRITE;

        //typedef struct _wfs_ttu_read
        //{
        //    WORD                  wNumOfChars;
        //    WORD                  fwMode;
        //    SHORT                 wPosX;
        //    SHORT                 wPosY;
        //    WORD                  fwEchoMode;
        //    WORD                  fwEchoAttr;
        //    BOOL                  bCursor;
        //    BOOL                  bFlush;
        //    BOOL                  bAutoEnd;
        //    LPSTR                 lpszActiveKeys;
        //    LPWSTR                lpwszActiveUNICODEKeys;
        //    LPWORD                lpwActiveCommandKeys;
        //    LPWORD                lpwTerminateCommandKeys;
        //} WFSTTUREAD, *LPWFSTTUREAD;

        //typedef struct _wfs_ttu_read_in
        //{
        //    LPSTR                 lpszInput;
        //    LPWSTR                lpszUNICODEInput;
        //} WFSTTUREADIN, *LPWFSTTUREADIN;

        //typedef struct _wfs_ttu_power_save_control
        //{
        //    USHORT                usMaxPowerSaveRecoveryTime;
        //} WFSTTUPOWERSAVECONTROL, *LPWFSTTUPOWERSAVECONTROL;

        //typedef struct _wfs_ttu_set_leds_ex
        //{
        //    USHORT                usLED;
        //    DWORD                 dwCommand;
        //} WFSTTUSETLEDSEX, *LPWFSTTUSETLEDSEX;

        ///*=================================================================*/
        ///* TTU Message Structures */
        ///*=================================================================*/

        //typedef struct _wfs_ttu_field_failure
        //{
        //    LPSTR                 lpszFormName;
        //    LPSTR                 lpszFieldName;
        //    WORD                  wFailure;
        //} WFSTTUFIELDFAIL, *LPWFSTTUFIELDFAIL;

        //typedef struct _wfs_ttu_key
        //{
        //    CHAR                  cKey;
        //    WORD                  wUNICODEKey;
        //    WORD                  wCommandKey;
        //} WFSTTUKEY, *LPWFSTTUKEY;

        //typedef struct _wfs_ttu_device_position
        //{
        //    WORD                  wPosition;
        //} WFSTTUDEVICEPOSITION, *LPWFSTTUDEVICEPOSITION;

        //typedef struct _wfs_ttu_power_save_change
        //{
        //    USHORT                usPowerSaveRecoveryTime;
        //} WFSTTUPOWERSAVECHANGE, *LPWFSTTUPOWERSAVECHANGE;

    };//class XFSTTUh
}//namespace

///*   restore alignment   */
//#pragma pack(pop)

//#ifdef __cplusplus
//} /*extern "C"*/
//#endif

//#endif /* __INC_XFSTTU__H */
