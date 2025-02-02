/******************************************************************************
*                                                                             *
* xfsidc.h    XFS - Identification card unit (IDC) definitions                *
*                                                                             *
*             Version 3.20  (March 02 2011)                                   *
*                                                                             *
******************************************************************************/

//#ifndef __INC_XFSIDC__H
//public const int __INC_XFSIDC__H

//#ifdef __cplusplus
//extern "C" {
//#endif

//#include "XFSAPI.H"

///*   be aware of alignment   */
//#pragma pack(push,1)
namespace iopcts
{
    public class XFSIDCh
    {

        /* values of WFSIDCCAPS.wClass */

        public const int WFS_SERVICE_CLASS_IDC = (2);
        public const string WFS_SERVICE_CLASS_NAME_IDC = "IDC";
        public const int WFS_SERVICE_CLASS_VERSION_IDC = (0x1403); /* Version 3.20 */

        public const int IDC_SERVICE_OFFSET = (WFS_SERVICE_CLASS_IDC * 100);

        /* IDC Info Commands */

        public const int WFS_INF_IDC_STATUS = (IDC_SERVICE_OFFSET + 1);
        public const int WFS_INF_IDC_CAPABILITIES = (IDC_SERVICE_OFFSET + 2);
        public const int WFS_INF_IDC_FORM_LIST = (IDC_SERVICE_OFFSET + 3);
        public const int WFS_INF_IDC_QUERY_FORM = (IDC_SERVICE_OFFSET + 4);
        public const int WFS_INF_IDC_QUERY_IFM_IDENTIFIER = (IDC_SERVICE_OFFSET + 5);

        /* IDC Execute Commands */

        public const int WFS_CMD_IDC_READ_TRACK = (IDC_SERVICE_OFFSET + 1);
        public const int WFS_CMD_IDC_WRITE_TRACK = (IDC_SERVICE_OFFSET + 2);
        public const int WFS_CMD_IDC_EJECT_CARD = (IDC_SERVICE_OFFSET + 3);
        public const int WFS_CMD_IDC_RETAIN_CARD = (IDC_SERVICE_OFFSET + 4);
        public const int WFS_CMD_IDC_RESET_COUNT = (IDC_SERVICE_OFFSET + 5);
        public const int WFS_CMD_IDC_SETKEY = (IDC_SERVICE_OFFSET + 6);
        public const int WFS_CMD_IDC_READ_RAW_DATA = (IDC_SERVICE_OFFSET + 7);
        public const int WFS_CMD_IDC_WRITE_RAW_DATA = (IDC_SERVICE_OFFSET + 8);
        public const int WFS_CMD_IDC_CHIP_IO = (IDC_SERVICE_OFFSET + 9);
        public const int WFS_CMD_IDC_RESET = (IDC_SERVICE_OFFSET + 10);
        public const int WFS_CMD_IDC_CHIP_POWER = (IDC_SERVICE_OFFSET + 11);
        public const int WFS_CMD_IDC_PARSE_DATA = (IDC_SERVICE_OFFSET + 12);
        public const int WFS_CMD_IDC_SET_GUIDANCE_LIGHT = (IDC_SERVICE_OFFSET + 13);
        public const int WFS_CMD_IDC_POWER_SAVE_CONTROL = (IDC_SERVICE_OFFSET + 14);
        public const int WFS_CMD_IDC_PARK_CARD = (IDC_SERVICE_OFFSET + 15);

        /* IDC Messages */

        public const int WFS_EXEE_IDC_INVALIDTRACKDATA = (IDC_SERVICE_OFFSET + 1);
        public const int WFS_EXEE_IDC_MEDIAINSERTED = (IDC_SERVICE_OFFSET + 3);
        public const int WFS_SRVE_IDC_MEDIAREMOVED = (IDC_SERVICE_OFFSET + 4);
        public const int WFS_SRVE_IDC_CARDACTION = (IDC_SERVICE_OFFSET + 5);
        public const int WFS_USRE_IDC_RETAINBINTHRESHOLD = (IDC_SERVICE_OFFSET + 6);
        public const int WFS_EXEE_IDC_INVALIDMEDIA = (IDC_SERVICE_OFFSET + 7);
        public const int WFS_EXEE_IDC_MEDIARETAINED = (IDC_SERVICE_OFFSET + 8);
        public const int WFS_SRVE_IDC_MEDIADETECTED = (IDC_SERVICE_OFFSET + 9);
        public const int WFS_SRVE_IDC_RETAINBININSERTED = (IDC_SERVICE_OFFSET + 10);
        public const int WFS_SRVE_IDC_RETAINBINREMOVED = (IDC_SERVICE_OFFSET + 11);
        public const int WFS_EXEE_IDC_INSERTCARD = (IDC_SERVICE_OFFSET + 12);
        public const int WFS_SRVE_IDC_DEVICEPOSITION = (IDC_SERVICE_OFFSET + 13);
        public const int WFS_SRVE_IDC_POWER_SAVE_CHANGE = (IDC_SERVICE_OFFSET + 14);
        public const int WFS_EXEE_IDC_TRACKDETECTED = (IDC_SERVICE_OFFSET + 15);

        /* values of WFSIDCSTATUS.fwDevice */

        public const int WFS_IDC_DEVONLINE = XFSAPIh.WFS_STAT_DEVONLINE;
        public const int WFS_IDC_DEVOFFLINE = XFSAPIh.WFS_STAT_DEVOFFLINE;
        public const int WFS_IDC_DEVPOWEROFF = XFSAPIh.WFS_STAT_DEVPOWEROFF;
        public const int WFS_IDC_DEVNODEVICE = XFSAPIh.WFS_STAT_DEVNODEVICE;
        public const int WFS_IDC_DEVHWERROR = XFSAPIh.WFS_STAT_DEVHWERROR;
        public const int WFS_IDC_DEVUSERERROR = XFSAPIh.WFS_STAT_DEVUSERERROR;
        public const int WFS_IDC_DEVBUSY = XFSAPIh.WFS_STAT_DEVBUSY;
        public const int WFS_IDC_DEVFRAUDATTEMPT = XFSAPIh.WFS_STAT_DEVFRAUDATTEMPT;
        public const int WFS_IDC_DEVPOTENTIALFRAUD = XFSAPIh.WFS_STAT_DEVPOTENTIALFRAUD;

        /* values of WFSIDCSTATUS.fwMedia, WFSIDCRETAINCARD.fwPosition,  */
        /* WFSIDCCARDACT.wPosition, WFSIDCSTATUS.lpwParkingStationMedia  */

        public const int WFS_IDC_MEDIAPRESENT = (1);
        public const int WFS_IDC_MEDIANOTPRESENT = (2);
        public const int WFS_IDC_MEDIAJAMMED = (3);
        public const int WFS_IDC_MEDIANOTSUPP = (4);
        public const int WFS_IDC_MEDIAUNKNOWN = (5);
        public const int WFS_IDC_MEDIAENTERING = (6);
        public const int WFS_IDC_MEDIALATCHED = (7);

        /* values of WFSIDCSTATUS.fwRetainBin */

        public const int WFS_IDC_RETAINBINOK = (1);
        public const int WFS_IDC_RETAINNOTSUPP = (2);
        public const int WFS_IDC_RETAINBINFULL = (3);
        public const int WFS_IDC_RETAINBINHIGH = (4);
        public const int WFS_IDC_RETAINBINMISSING = (5);

        /* values of WFSIDCSTATUS.fwSecurity */

        public const int WFS_IDC_SECNOTSUPP = (1);
        public const int WFS_IDC_SECNOTREADY = (2);
        public const int WFS_IDC_SECOPEN = (3);

        /* values of WFSIDCSTATUS.fwChipPower */

        public const int WFS_IDC_CHIPONLINE = (0);
        public const int WFS_IDC_CHIPPOWEREDOFF = (1);
        public const int WFS_IDC_CHIPBUSY = (2);
        public const int WFS_IDC_CHIPNODEVICE = (3);
        public const int WFS_IDC_CHIPHWERROR = (4);
        public const int WFS_IDC_CHIPNOCARD = (5);
        public const int WFS_IDC_CHIPNOTSUPP = (6);
        public const int WFS_IDC_CHIPUNKNOWN = (7);

        /* Size and max index of dwGuidLights array */
        public const int WFS_IDC_GUIDLIGHTS_SIZE = (32);
        public const int WFS_IDC_GUIDLIGHTS_MAX = (WFS_IDC_GUIDLIGHTS_SIZE - 1);

        /* Indices of WFSIDCSTATUS.dwGuidLights [...]
                      WFSIDCCAPS.dwGuidLights [...]
        */
        public const int WFS_IDC_GUIDANCE_CARDUNIT = (0);

        /* Values of WFSIDCSTATUS.dwGuidLights [...]
                     WFSIDCCAPS.dwGuidLights [...]
        */
        public const int WFS_IDC_GUIDANCE_NOT_AVAILABLE = (0x00000000);
        public const int WFS_IDC_GUIDANCE_OFF = (0x00000001);
        public const int WFS_IDC_GUIDANCE_ON = (0x00000002);
        public const int WFS_IDC_GUIDANCE_SLOW_FLASH = (0x00000004);
        public const int WFS_IDC_GUIDANCE_MEDIUM_FLASH = (0x00000008);
        public const int WFS_IDC_GUIDANCE_QUICK_FLASH = (0x00000010);
        public const int WFS_IDC_GUIDANCE_CONTINUOUS = (0x00000080);
        public const int WFS_IDC_GUIDANCE_RED = (0x00000100);
        public const int WFS_IDC_GUIDANCE_GREEN = (0x00000200);
        public const int WFS_IDC_GUIDANCE_YELLOW = (0x00000400);
        public const int WFS_IDC_GUIDANCE_BLUE = (0x00000800);
        public const int WFS_IDC_GUIDANCE_CYAN = (0x00001000);
        public const int WFS_IDC_GUIDANCE_MAGENTA = (0x00002000);
        public const int WFS_IDC_GUIDANCE_WHITE = (0x00004000);



        /* values of WFSIDCSTATUS.fwChipModule */

        public const int WFS_IDC_CHIPMODOK = (1);
        public const int WFS_IDC_CHIPMODINOP = (2);
        public const int WFS_IDC_CHIPMODUNKNOWN = (3);
        public const int WFS_IDC_CHIPMODNOTSUPP = (4);

        /* values of WFSIDCSTATUS.fwMagReadModule and
                     WFSIDCSTATUS.fwMagWriteModule  */

        public const int WFS_IDC_MAGMODOK = (1);
        public const int WFS_IDC_MAGMODINOP = (2);
        public const int WFS_IDC_MAGMODUNKNOWN = (3);
        public const int WFS_IDC_MAGMODNOTSUPP = (4);

        /* values of WFSIDCSTATUS.fwFrontImageModule and
                     WFSIDCSTATUS.fwBackImageModule */

        public const int WFS_IDC_IMGMODOK = (1);
        public const int WFS_IDC_IMGMODINOP = (2);
        public const int WFS_IDC_IMGMODUNKNOWN = (3);
        public const int WFS_IDC_IMGMODNOTSUPP = (4);

        /* values of WFSIDCSTATUS.wDevicePosition
                     WFSIDCDEVICEPOSITION.wPosition */

        public const int WFS_IDC_DEVICEINPOSITION = (0);
        public const int WFS_IDC_DEVICENOTINPOSITION = (1);
        public const int WFS_IDC_DEVICEPOSUNKNOWN = (2);
        public const int WFS_IDC_DEVICEPOSNOTSUPP = (3);

        /* values of WFSIDCCAPS.fwType */

        public const int WFS_IDC_TYPEMOTOR = (1);
        public const int WFS_IDC_TYPESWIPE = (2);
        public const int WFS_IDC_TYPEDIP = (3);
        public const int WFS_IDC_TYPECONTACTLESS = (4);
        public const int WFS_IDC_TYPELATCHEDDIP = (5);
        public const int WFS_IDC_TYPEPERMANENT = (6);

        /* values of WFSIDCCAPS.fwReadTracks,
                     WFSIDCCAPS.fwWriteTracks,
                     WFSIDCCARDDATA.wDataSource,
                     WFSIDCCAPS.fwChipProtocols,
                     WFSIDCCAPS.fwWriteMode,
                     WFSIDCCAPS.fwChipPower */

        public const int WFS_IDC_NOTSUPP = 0x0000;

        /* values of WFSIDCCAPS.fwReadTracks, WFSIDCCAPS.fwWriteTracks,
                     WFSIDCCARDDATA.wDataSource,
                     WFS_CMD_IDC_READ_RAW_DATA */

        public const int WFS_IDC_TRACK1 = 0x0001;
        public const int WFS_IDC_TRACK2 = 0x0002;
        public const int WFS_IDC_TRACK3 = 0x0004;
        public const int WFS_IDC_FRONT_TRACK_1 = 0x0080;
        public const int WFS_IDC_TRACK1_JIS1 = 0x0400;
        public const int WFS_IDC_TRACK3_JIS1 = 0x0800;

        /* further values of WFSIDCCARDDATA.wDataSource =(except
           WFS_IDC_FLUXINACTIVE);, WFS_CMD_IDC_READ_RAW_DATA */

        public const int WFS_IDC_CHIP = 0x0008;
        public const int WFS_IDC_SECURITY = 0x0010;
        public const int WFS_IDC_FLUXINACTIVE = 0x0020;
        public const int WFS_IDC_TRACK_WM = 0x8000;
        public const int WFS_IDC_MEMORY_CHIP = 0x0040;
        public const int WFS_IDC_FRONTIMAGE = 0x0100;
        public const int WFS_IDC_BACKIMAGE = 0x0200;
        public const int WFS_IDC_DDI = 0x4000;

        /* values of WFSIDCCAPS.fwChipProtocols */

        public const int WFS_IDC_CHIPT0 = 0x0001;
        public const int WFS_IDC_CHIPT1 = 0x0002;
        public const int WFS_IDC_CHIP_PROTOCOL_NOT_REQUIRED = 0x0004;
        public const int WFS_IDC_CHIPTYPEA_PART3 = 0x0008;
        public const int WFS_IDC_CHIPTYPEA_PART4 = 0x0010;
        public const int WFS_IDC_CHIPTYPEB = 0x0020;
        public const int WFS_IDC_CHIPNFC = 0x0040;

        /* values of WFSIDCCAPS.fwSecType */
        //lkh: changed WFS_IDC_SEC prefix to WFS_IDC_SECTYPE
        public const int WFS_IDC_SECTYPENOTSUPP = (1);
        public const int WFS_IDC_SECTYPEMMBOX = (2);
        public const int WFS_IDC_SECTYPECIM86 = (3);

        /* values of WFSIDCCAPS.fwPowerOnOption, WFSIDCCAPS.fwPowerOffOption*/

        public const int WFS_IDC_NOACTION = (1);
        public const int WFS_IDC_EJECT = (2);
        public const int WFS_IDC_RETAIN = (3);
        public const int WFS_IDC_EJECTTHENRETAIN = (4);
        public const int WFS_IDC_READPOSITION = (5);

        /* values of WFSIDCCAPS.fwWriteMode; WFSIDCWRITETRACK.fwWriteMethod, WFSIDCCARDDATA.fwWriteMethod */

        /* Note: WFS_IDC_UNKNOWN was removed as it was an invalid value */
        public const int WFS_IDC_LOCO = 0x0002;
        public const int WFS_IDC_HICO = 0x0004;
        public const int WFS_IDC_AUTO = 0x0008;

        /* values of WFSIDCCAPS.fwChipPower */

        public const int WFS_IDC_CHIPPOWERCOLD = 0x0002;
        public const int WFS_IDC_CHIPPOWERWARM = 0x0004;
        public const int WFS_IDC_CHIPPOWEROFF = 0x0008;

        /* values of WFSIDCCAPS.fwDIPMode */

        public const int WFS_IDC_DIP_UNKNOWN = 0x0001;
        public const int WFS_IDC_DIP_EXIT = 0x0002;
        public const int WFS_IDC_DIP_ENTRY = 0x0004;
        public const int WFS_IDC_DIP_ENTRY_EXIT = 0x0008;

        /* values of WFSIDCCAPS. lpwMemoryChipProtocols */

        public const int WFS_IDC_MEM_SIEMENS4442 = 0x0001;
        public const int WFS_IDC_MEM_GPM896 = 0x0002;

        /* values of WFSIDCFORM.fwAction */

        public const int WFS_IDC_ACTIONREAD = 0x0001;
        public const int WFS_IDC_ACTIONWRITE = 0x0002;

        /* values of WFSIDCTRACKEVENT.fwStatus, WFSIDCCARDDATA.wStatus */

        public const int WFS_IDC_DATAOK = (0);
        public const int WFS_IDC_DATAMISSING = (1);
        public const int WFS_IDC_DATAINVALID = (2);
        public const int WFS_IDC_DATATOOLONG = (3);
        public const int WFS_IDC_DATATOOSHORT = (4);
        public const int WFS_IDC_DATASRCNOTSUPP = (5);
        public const int WFS_IDC_DATASRCMISSING = (6);

        /* values WFSIDCCARDACT.wAction */

        public const int WFS_IDC_CARDRETAINED = (1);
        public const int WFS_IDC_CARDEJECTED = (2);
        public const int WFS_IDC_CARDREADPOSITION = (3);
        public const int WFS_IDC_CARDJAMMED = (4);

        /* values of WFSIDCCARDDATA.lpbData if security is read */

        public const char WFS_IDC_SEC_READLEVEL1 = '1';
        public const char WFS_IDC_SEC_READLEVEL2 = '2';
        public const char WFS_IDC_SEC_READLEVEL3 = '3';
        public const char WFS_IDC_SEC_READLEVEL4 = '4';
        public const char WFS_IDC_SEC_READLEVEL5 = '5';
        public const char WFS_IDC_SEC_BADREADLEVEL = '6';
        public const char WFS_IDC_SEC_NODATA = '7';
        public const char WFS_IDC_SEC_DATAINVAL = '8';
        public const char WFS_IDC_SEC_HWERROR = '9';
        public const char WFS_IDC_SEC_NOINIT = 'A';

        /* values of WFSIDCIFMIDENTIFIER.wIFMAuthority */

        public const int WFS_IDC_IFMEMV = (1);
        public const int WFS_IDC_IFMEUROPAY = (2);
        public const int WFS_IDC_IFMVISA = (3);
        public const int WFS_IDC_IFMGIECB = (4);

        /* values WFSIDCCAPS.fwEjectPosition, WFSIDCEJECTCARD.wEjectPosition */

        public const int WFS_IDC_EXITPOSITION = (0x0001);
        public const int WFS_IDC_TRANSPORTPOSITION = (0x0002);

        /* values WFSIDCPARKCARD.wDirection */

        public const int WFS_IDC_PARK_IN = 0x0001;
        public const int WFS_IDC_PARK_OUT = 0x0002;

        /* values of WFSIDCSTATUS.wAntiFraudModule */

        public const int WFS_IDC_AFMNOTSUPP = (0);
        public const int WFS_IDC_AFMOK = (1);
        public const int WFS_IDC_AFMINOP = (2);
        public const int WFS_IDC_AFMDEVICEDETECTED = (3);
        public const int WFS_IDC_AFMUNKNOWN = (4);

        /* WOSA/XFS IDC Errors */

        public const int WFS_ERR_IDC_MEDIAJAM = (-(IDC_SERVICE_OFFSET + 0));
        public const int WFS_ERR_IDC_NOMEDIA = (-(IDC_SERVICE_OFFSET + 1));
        public const int WFS_ERR_IDC_MEDIARETAINED = (-(IDC_SERVICE_OFFSET + 2));
        public const int WFS_ERR_IDC_RETAINBINFULL = (-(IDC_SERVICE_OFFSET + 3));
        public const int WFS_ERR_IDC_INVALIDDATA = (-(IDC_SERVICE_OFFSET + 4));
        public const int WFS_ERR_IDC_INVALIDMEDIA = (-(IDC_SERVICE_OFFSET + 5));
        public const int WFS_ERR_IDC_FORMNOTFOUND = (-(IDC_SERVICE_OFFSET + 6));
        public const int WFS_ERR_IDC_FORMINVALID = (-(IDC_SERVICE_OFFSET + 7));
        public const int WFS_ERR_IDC_DATASYNTAX = (-(IDC_SERVICE_OFFSET + 8));
        public const int WFS_ERR_IDC_SHUTTERFAIL = (-(IDC_SERVICE_OFFSET + 9));
        public const int WFS_ERR_IDC_SECURITYFAIL = (-(IDC_SERVICE_OFFSET + 10));
        public const int WFS_ERR_IDC_PROTOCOLNOTSUPP = (-(IDC_SERVICE_OFFSET + 11));
        public const int WFS_ERR_IDC_ATRNOTOBTAINED = (-(IDC_SERVICE_OFFSET + 12));
        public const int WFS_ERR_IDC_INVALIDKEY = (-(IDC_SERVICE_OFFSET + 13));
        public const int WFS_ERR_IDC_WRITE_METHOD = (-(IDC_SERVICE_OFFSET + 14));
        public const int WFS_ERR_IDC_CHIPPOWERNOTSUPP = (-(IDC_SERVICE_OFFSET + 15));
        public const int WFS_ERR_IDC_CARDTOOSHORT = (-(IDC_SERVICE_OFFSET + 16));
        public const int WFS_ERR_IDC_CARDTOOLONG = (-(IDC_SERVICE_OFFSET + 17));
        public const int WFS_ERR_IDC_INVALID_PORT = (-(IDC_SERVICE_OFFSET + 18));
        public const int WFS_ERR_IDC_POWERSAVETOOSHORT = (-(IDC_SERVICE_OFFSET + 19));
        public const int WFS_ERR_IDC_POWERSAVEMEDIAPRESENT = (-(IDC_SERVICE_OFFSET + 20));
        public const int WFS_ERR_IDC_CARDPRESENT = (-(IDC_SERVICE_OFFSET + 21));
        public const int WFS_ERR_IDC_POSITIONINVALID = (-(IDC_SERVICE_OFFSET + 22));

        ///*=================================================================*/
        ///* IDC Info Command Structures and variables */
        ///*=================================================================*/

        //typedef struct _wfs_idc_status
        //{
        //    WORD                     fwDevice;
        //    WORD                     fwMedia;
        //    WORD                     fwRetainBin;
        //    WORD                     fwSecurity;
        //    USHORT                   usCards;
        //    WORD                     fwChipPower;
        //    LPSTR                    lpszExtra;
        //    DWORD                    dwGuidLights[WFS_IDC_GUIDLIGHTS_SIZE];
        //    WORD                     fwChipModule;
        //    WORD                     fwMagReadModule;
        //    WORD                     fwMagWriteModule;
        //    WORD                     fwFrontImageModule;
        //    WORD                     fwBackImageModule;
        //    WORD                     wDevicePosition;
        //    USHORT                   usPowerSaveRecoveryTime;
        //    LPWORD                   lpwParkingStationMedia; 
        //    WORD                     wAntiFraudModule;
        //} WFSIDCSTATUS, *LPWFSIDCSTATUS;

        //typedef struct _wfs_idc_caps
        //{
        //    WORD                     wClass;
        //    WORD                     fwType;
        //    BOOL                     bCompound;
        //    WORD                     fwReadTracks;
        //    WORD                     fwWriteTracks;
        //    WORD                     fwChipProtocols;
        //    USHORT                   usCards;
        //    WORD                     fwSecType;
        //    WORD                     fwPowerOnOption;
        //    WORD                     fwPowerOffOption;
        //    BOOL                     bFluxSensorProgrammable;
        //    BOOL                     bReadWriteAccessFollowingEject;
        //    WORD                     fwWriteMode;
        //    WORD                     fwChipPower;
        //    LPSTR                    lpszExtra;
        //    WORD                     fwDIPMode;
        //    LPWORD                   lpwMemoryChipProtocols;
        //    DWORD                    dwGuidLights[WFS_IDC_GUIDLIGHTS_SIZE];
        //    WORD                     fwEjectPosition;
        //    BOOL                     bPowerSaveControl;
        //    USHORT                   usParkingStations; 
        //    BOOL                     bAntiFraudModule;
        //} WFSIDCCAPS, *LPWFSIDCCAPS;

        //typedef struct _wfs_idc_form
        //{
        //    LPSTR                    lpszFormName;
        //    CHAR                     cFieldSeparatorTrack1;
        //    CHAR                     cFieldSeparatorTrack2;
        //    CHAR                     cFieldSeparatorTrack3;
        //    WORD                     fwAction;
        //    LPSTR                    lpszTracks;
        //    BOOL                     bSecure;
        //    LPSTR                    lpszTrack1Fields;
        //    LPSTR                    lpszTrack2Fields;
        //    LPSTR                    lpszTrack3Fields;
        //    LPSTR                    lpszFrontTrack1Fields;
        //    CHAR                     cFieldSeparatorFrontTrack1;
        //    LPSTR                    lpszJIS1Track1Fields;
        //    LPSTR                    lpszJIS1Track3Fields;
        //    CHAR                     cFieldSeparatorJIS1Track1;
        //    CHAR                     cFieldSeparatorJIS1Track3;
        //} WFSIDCFORM, *LPWFSIDCFORM;

        //typedef struct _wfs_idc_ifm_identifier
        //{
        //    WORD                     wIFMAuthority;
        //    LPSTR                    lpszIFMIdentifier;
        //} WFSIDCIFMIDENTIFIER, *LPWFSIDCIFMIDENTIFIER;

        ///*=================================================================*/
        ///* IDC Execute Command Structures */
        ///*=================================================================*/

        //typedef struct _wfs_idc_write_track
        //{
        //    LPSTR                    lpstrFormName;
        //    LPSTR                    lpstrTrackData;
        //    WORD                     fwWriteMethod;
        //} WFSIDCWRITETRACK, *LPWFSIDCWRITETRACK;

        //typedef struct _wfs_idc_retain_card
        //{
        //    USHORT                   usCount;
        //    WORD                     fwPosition;
        //} WFSIDCRETAINCARD, *LPWFSIDCRETAINCARD;

        //typedef struct _wfs_idc_setkey
        //{
        //    USHORT                   usKeyLen;
        //    LPBYTE                   lpbKeyValue;
        //} WFSIDCSETKEY, *LPWFSIDCSETKEY;

        //typedef struct _wfs_idc_card_data
        //{
        //    WORD                     wDataSource;
        //    WORD                     wStatus;
        //    ULONG                    ulDataLength;
        //    LPBYTE                   lpbData;
        //    WORD                     fwWriteMethod;
        //} WFSIDCCARDDATA, *LPWFSIDCCARDDATA;

        //typedef struct _wfs_idc_chip_io
        //{
        //    WORD                     wChipProtocol;
        //    ULONG                    ulChipDataLength;
        //    LPBYTE                   lpbChipData;
        //} WFSIDCCHIPIO, *LPWFSIDCCHIPIO;

        //typedef struct _wfs_idc_chip_power_out
        //{
        //    ULONG                    ulChipDataLength;
        //    LPBYTE                   lpbChipData;
        //} WFSIDCCHIPPOWEROUT, *LPWFSIDCCHIPPOWEROUT;

        //typedef struct _wfs_idc_parse_data
        //{
        //    LPSTR                    lpstrFormName;
        //    LPWFSIDCCARDDATA         *lppCardData;
        //} WFSIDCPARSEDATA, *LPWFSIDCPARSEDATA;

        //typedef struct _wfs_idc_set_guidlight
        //{
        //    WORD                     wGuidLight;
        //    DWORD                    dwCommand;
        //} WFSIDCSETGUIDLIGHT, *LPWFSIDCSETGUIDLIGHT;

        //typedef struct _wfs_idc_eject_card
        //{
        //    WORD                     wEjectPosition;
        //} WFSIDCEJECTCARD, *LPWFSIDCEJECTCARD;

        //typedef struct _wfs_idc_power_save_control
        //{
        //    USHORT                   usMaxPowerSaveRecoveryTime;
        //} WFSIDCPOWERSAVECONTROL, *LPWFSIDCPOWERSAVECONTROL;

        //typedef struct _wfs_idc_park_card
        //{
        //    WORD                     wDirection;
        //    USHORT                   usParkingStation;
        //} WFSIDCPARKCARD, *LPWFSIDCPARKCARD;

        ///*=================================================================*/
        ///* IDC Message Structures */
        ///*=================================================================*/

        //typedef struct _wfs_idc_track_event
        //{
        //    WORD                     fwStatus;
        //    LPSTR                    lpstrTrack;
        //    LPSTR                    lpstrData;
        //} WFSIDCTRACKEVENT, *LPWFSIDCTRACKEVENT;

        //typedef struct _wfs_idc_card_act
        //{
        //    WORD                     wAction;
        //    WORD                     wPosition;
        //} WFSIDCCARDACT, *LPWFSIDCCARDACT;

        //typedef struct _wfs_idc_device_position
        //{
        //    WORD                     wPosition;
        //} WFSIDCDEVICEPOSITION, *LPWFSIDCDEVICEPOSITION;

        //typedef struct _wfs_idc_power_save_change
        //{
        //    USHORT                   usPowerSaveRecoveryTime;
        //} WFSIDCPOWERSAVECHANGE, *LPWFSIDCPOWERSAVECHANGE;

        //typedef struct _wfs_idc_track_detected
        //{
        //    WORD                     fwTracks;
        //} WFSIDCTRACKDETECTED, *LPWFSIDCTRACKDETECTED;

    };//class XFSIDCh
}//namespace


///*   restore alignment   */
//#pragma pack(pop)

//#ifdef __cplusplus
//}       /*extern "C"*/
//#endif

//#endif  /* __INC_XFSIDC__H */
