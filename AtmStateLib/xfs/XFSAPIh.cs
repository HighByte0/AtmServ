///************************************************************************
//*                                                                       *
//* xfsapi.h      XFS - API functions, types, and definitions             *
//*                                                                       *
//*               Version 3.20  (March 02 2011)                           *
//*                                                                       *
//************************************************************************/

//#ifndef __inc_xfsapi__h
//public const int __inc_xfsapi__h

//#ifdef __cplusplus
//extern "C" {
//#endif

///*   be aware of alignment   */
//#pragma pack(push,1)

///****** Common *********************************************************/

//#include <windows.h>

namespace iopcts
{
    public class XFSAPIh
    {

        //typedef unsigned short USHORT;
        //typedef char CHAR;
        //typedef short SHORT;
        //typedef unsigned long ULONG;
        //typedef unsigned char UCHAR;
        //typedef SHORT * LPSHORT;
        //typedef LPVOID * LPLPVOID;
        //typedef ULONG * LPULONG;
        //typedef USHORT * LPUSHORT;

        //typedef HANDLE HPROVIDER;

        //typedef ULONG REQUESTID;
        //typedef REQUESTID * LPREQUESTID;

        //typedef HANDLE HAPP;
        //typedef HAPP * LPHAPP;

        //typedef USHORT HSERVICE;
        //typedef HSERVICE * LPHSERVICE;

        //typedef LONG HRESULT;
        //typedef HRESULT * LPHRESULT;

        //typedef BOOL (WINAPI * XFSBLOCKINGHOOK)(VOID);
        //typedef XFSBLOCKINGHOOK * LPXFSBLOCKINGHOOK;

        /****** String lengths **************************************************/

        public const int WFSDDESCRIPTION_LEN = 256;
        public const int WFSDSYSSTATUS_LEN = 256;

        /****** Values of WFSDEVSTATUS.fwState **********************************/

        public const int WFS_STAT_DEVONLINE = (0);
        public const int WFS_STAT_DEVOFFLINE = (1);
        public const int WFS_STAT_DEVPOWEROFF = (2);
        public const int WFS_STAT_DEVNODEVICE = (3);
        public const int WFS_STAT_DEVHWERROR = (4);
        public const int WFS_STAT_DEVUSERERROR = (5);
        public const int WFS_STAT_DEVBUSY = (6);
        public const int WFS_STAT_DEVFRAUDATTEMPT = (7);
        public const int WFS_STAT_DEVPOTENTIALFRAUD = (8);
        /****** Value of WFS_DEFAULT_HAPP ***************************************/

        public const int WFS_DEFAULT_HAPP = (0);

        ///****** Data Structures *************************************************/

        //typedef struct _wfs_result
        //{
        //    REQUESTID       RequestID;
        //    HSERVICE        hService;
        //    SYSTEMTIME      tsTimestamp;
        //    HRESULT         hResult;
        //    union {
        //        DWORD       dwCommandCode;
        //        DWORD       dwEventID;
        //    } u;
        //    LPVOID          lpBuffer;
        //} WFSRESULT, * LPWFSRESULT;

        //typedef struct _wfsversion
        //{
        //    WORD            wVersion;
        //    WORD            wLowVersion;
        //    WORD            wHighVersion;
        //    CHAR            szDescription[WFSDDESCRIPTION_LEN+1];
        //    CHAR            szSystemStatus[WFSDSYSSTATUS_LEN+1];
        //} WFSVERSION, * LPWFSVERSION;

        ///****** Message Structures **********************************************/

        //typedef struct _wfs_devstatus
        //{
        //    LPSTR           lpszPhysicalName;
        //    LPSTR           lpszWorkstationName;
        //    DWORD           dwState;
        //} WFSDEVSTATUS, * LPWFSDEVSTATUS;

        //typedef struct _wfs_undevmsg
        //{
        //    LPSTR           lpszLogicalName;
        //    LPSTR           lpszWorkstationName;
        //    LPSTR           lpszAppID;
        //    DWORD           dwSize;
        //    LPBYTE          lpbDescription;
        //    DWORD           dwMsg;
        //    LPWFSRESULT     lpWFSResult;
        //} WFSUNDEVMSG, * LPWFSUNDEVMSG;  

        //typedef struct _wfs_appdisc
        //{
        //    LPSTR           lpszLogicalName;
        //    LPSTR           lpszWorkstationName;
        //    LPSTR           lpszAppID;
        //} WFSAPPDISC, * LPWFSAPPDISC;

        //typedef struct _wfs_hwerror
        //{
        //    LPSTR           lpszLogicalName;
        //    LPSTR           lpszPhysicalName;
        //    LPSTR           lpszWorkstationName;
        //    LPSTR           lpszAppID;
        //    DWORD           dwAction;
        //    DWORD           dwSize;
        //    LPBYTE          lpbDescription;
        //} WFSHWERROR, * LPWFSHWERROR;

        //typedef struct _wfs_vrsnerror
        //{
        //    LPSTR           lpszLogicalName;
        //    LPSTR           lpszWorkstationName;
        //    LPSTR           lpszAppID;
        //    DWORD           dwSize;
        //    LPBYTE          lpbDescription;
        //    LPWFSVERSION    lpWFSVersion;
        //} WFSVRSNERROR, * LPWFSVRSNERROR;

        /****** Error codes ******************************************************/

        public const int WFS_SUCCESS = (0);
        public const int WFS_ERR_ALREADY_STARTED = (-1);
        public const int WFS_ERR_API_VER_TOO_HIGH = (-2);
        public const int WFS_ERR_API_VER_TOO_LOW = (-3);
        public const int WFS_ERR_CANCELED = (-4);
        public const int WFS_ERR_CFG_INVALID_HKEY = (-5);
        public const int WFS_ERR_CFG_INVALID_NAME = (-6);
        public const int WFS_ERR_CFG_INVALID_SUBKEY = (-7);
        public const int WFS_ERR_CFG_INVALID_VALUE = (-8);
        public const int WFS_ERR_CFG_KEY_NOT_EMPTY = (-9);
        public const int WFS_ERR_CFG_NAME_TOO_LONG = (-10);
        public const int WFS_ERR_CFG_NO_MORE_ITEMS = (-11);
        public const int WFS_ERR_CFG_VALUE_TOO_LONG = (-12);
        public const int WFS_ERR_DEV_NOT_READY = (-13);
        public const int WFS_ERR_HARDWARE_ERROR = (-14);
        public const int WFS_ERR_INTERNAL_ERROR = (-15);
        public const int WFS_ERR_INVALID_ADDRESS = (-16);
        public const int WFS_ERR_INVALID_APP_HANDLE = (-17);
        public const int WFS_ERR_INVALID_BUFFER = (-18);
        public const int WFS_ERR_INVALID_CATEGORY = (-19);
        public const int WFS_ERR_INVALID_COMMAND = (-20);
        public const int WFS_ERR_INVALID_EVENT_CLASS = (-21);
        public const int WFS_ERR_INVALID_HSERVICE = (-22);
        public const int WFS_ERR_INVALID_HPROVIDER = (-23);
        public const int WFS_ERR_INVALID_HWND = (-24);
        public const int WFS_ERR_INVALID_HWNDREG = (-25);
        public const int WFS_ERR_INVALID_POINTER = (-26);
        public const int WFS_ERR_INVALID_REQ_ID = (-27);
        public const int WFS_ERR_INVALID_RESULT = (-28);
        public const int WFS_ERR_INVALID_SERVPROV = (-29);
        public const int WFS_ERR_INVALID_TIMER = (-30);
        public const int WFS_ERR_INVALID_TRACELEVEL = (-31);
        public const int WFS_ERR_LOCKED = (-32);
        public const int WFS_ERR_NO_BLOCKING_CALL = (-33);
        public const int WFS_ERR_NO_SERVPROV = (-34);
        public const int WFS_ERR_NO_SUCH_THREAD = (-35);
        public const int WFS_ERR_NO_TIMER = (-36);
        public const int WFS_ERR_NOT_LOCKED = (-37);
        public const int WFS_ERR_NOT_OK_TO_UNLOAD = (-38);
        public const int WFS_ERR_NOT_STARTED = (-39);
        public const int WFS_ERR_NOT_REGISTERED = (-40);
        public const int WFS_ERR_OP_IN_PROGRESS = (-41);
        public const int WFS_ERR_OUT_OF_MEMORY = (-42);
        public const int WFS_ERR_SERVICE_NOT_FOUND = (-43);
        public const int WFS_ERR_SPI_VER_TOO_HIGH = (-44);
        public const int WFS_ERR_SPI_VER_TOO_LOW = (-45);
        public const int WFS_ERR_SRVC_VER_TOO_HIGH = (-46);
        public const int WFS_ERR_SRVC_VER_TOO_LOW = (-47);
        public const int WFS_ERR_TIMEOUT = (-48);
        public const int WFS_ERR_UNSUPP_CATEGORY = (-49);
        public const int WFS_ERR_UNSUPP_COMMAND = (-50);
        public const int WFS_ERR_VERSION_ERROR_IN_SRVC = (-51);
        public const int WFS_ERR_INVALID_DATA = (-52);
        public const int WFS_ERR_SOFTWARE_ERROR = (-53);
        public const int WFS_ERR_CONNECTION_LOST = (-54);
        public const int WFS_ERR_USER_ERROR = (-55);
        public const int WFS_ERR_UNSUPP_DATA = (-56);
        public const int WFS_ERR_FRAUD_ATTEMPT = (-57);
        public const int WFS_ERR_SEQUENCE_ERROR = (-58);


        public const int WFS_INDEFINITE_WAIT = 0;

        /****** Messages ********************************************************/

        /* Message-No = (WM_USER + No) */

        //public const int WFS_OPEN_COMPLETE                       = (WM_USER + 1);
        //public const int WFS_CLOSE_COMPLETE                      = (WM_USER + 2);
        //public const int WFS_LOCK_COMPLETE                       = (WM_USER + 3);
        //public const int WFS_UNLOCK_COMPLETE                     = (WM_USER + 4);
        //public const int WFS_REGISTER_COMPLETE                   = (WM_USER + 5);
        //public const int WFS_DEREGISTER_COMPLETE                 = (WM_USER + 6);
        //public const int WFS_GETINFO_COMPLETE                    = (WM_USER + 7);
        //public const int WFS_EXECUTE_COMPLETE                    = (WM_USER + 8);

        //public const int WFS_EXECUTE_EVENT                       = (WM_USER + 20);
        //public const int WFS_SERVICE_EVENT                       = (WM_USER + 21);
        //public const int WFS_USER_EVENT                          = (WM_USER + 22);
        //public const int WFS_SYSTEM_EVENT                        = (WM_USER + 23);

        //public const int WFS_TIMER_EVENT                         = (WM_USER + 100);

        /****** Event Classes ***************************************************/

        public const int SERVICE_EVENTS = (1);
        public const int USER_EVENTS = (2);
        public const int SYSTEM_EVENTS = (4);
        public const int EXECUTE_EVENTS = (8);

        /****** System Event IDs ************************************************/

        public const int WFS_SYSE_UNDELIVERABLE_MSG = (1);
        public const int WFS_SYSE_HARDWARE_ERROR = (2);
        public const int WFS_SYSE_VERSION_ERROR = (3);
        public const int WFS_SYSE_DEVICE_STATUS = (4);
        public const int WFS_SYSE_APP_DISCONNECT = (5);
        public const int WFS_SYSE_SOFTWARE_ERROR = (6);
        public const int WFS_SYSE_USER_ERROR = (7);
        public const int WFS_SYSE_LOCK_REQUESTED = (8);
        public const int WFS_SYSE_FRAUD_ATTEMPT = (9);


        /****** XFS Trace Level ********************************************/

        public const int WFS_TRACE_API = (0x00000001);
        public const int WFS_TRACE_ALL_API = (0x00000002);
        public const int WFS_TRACE_SPI = (0x00000004);
        public const int WFS_TRACE_ALL_SPI = (0x00000008);
        public const int WFS_TRACE_MGR = (0x00000010);

        /****** XFS Error Actions ********************************************/

        public const int WFS_ERR_ACT_NOACTION = (0x0000);
        public const int WFS_ERR_ACT_RESET = (0x0001);
        public const int WFS_ERR_ACT_SWERROR = (0x0002);
        public const int WFS_ERR_ACT_CONFIG = (0x0004);
        public const int WFS_ERR_ACT_HWCLEAR = (0x0008);
        public const int WFS_ERR_ACT_HWMAINT = (0x0010);
        public const int WFS_ERR_ACT_SUSPEND = (0x0020);

        /****** XFS SNMP MIB Category Codes **********************************/
        /* NOTE: To support the XFS SNMP MIB, the WFSGet[Async]Info category codes between 60000 and 60999 are reserved.*/


        /****** API functions ***************************************************/

        //HRESULT extern WINAPI WFSCancelAsyncRequest ( HSERVICE hService, REQUESTID RequestID);

        //HRESULT extern WINAPI WFSCancelBlockingCall ( DWORD dwThreadID);

        //HRESULT extern WINAPI WFSCleanUp ();

        //HRESULT extern WINAPI WFSClose ( HSERVICE hService);

        //HRESULT extern WINAPI WFSAsyncClose ( HSERVICE hService, HWND hWnd, LPREQUESTID lpRequestID);

        //HRESULT extern WINAPI WFSCreateAppHandle ( LPHAPP lphApp);

        //HRESULT extern WINAPI WFSDeregister ( HSERVICE hService, DWORD dwEventClass, HWND hWndReg);

        //HRESULT extern WINAPI WFSAsyncDeregister ( HSERVICE hService, DWORD dwEventClass, HWND hWndReg, HWND hWnd, LPREQUESTID lpRequestID);

        //HRESULT extern WINAPI WFSDestroyAppHandle ( HAPP hApp);

        //HRESULT extern WINAPI WFSExecute ( HSERVICE hService, DWORD dwCommand, LPVOID lpCmdData, DWORD dwTimeOut, LPWFSRESULT * lppResult);

        //HRESULT extern WINAPI WFSAsyncExecute ( HSERVICE hService, DWORD dwCommand, LPVOID lpCmdData, DWORD dwTimeOut, HWND hWnd, LPREQUESTID lpRequestID);

        //HRESULT extern WINAPI WFSFreeResult ( LPWFSRESULT lpResult);

        //HRESULT extern WINAPI WFSGetInfo ( HSERVICE hService, DWORD dwCategory, LPVOID lpQueryDetails, DWORD dwTimeOut, LPWFSRESULT * lppResult);

        //HRESULT extern WINAPI WFSAsyncGetInfo ( HSERVICE hService, DWORD dwCategory, LPVOID lpQueryDetails, DWORD dwTimeOut, HWND hWnd, LPREQUESTID lpRequestID);

        //BOOL extern WINAPI WFSIsBlocking ();

        //HRESULT extern WINAPI WFSLock ( HSERVICE hService, DWORD dwTimeOut, LPWFSRESULT * lppResult);

        //HRESULT extern WINAPI WFSAsyncLock ( HSERVICE hService, DWORD dwTimeOut, HWND hWnd,  LPREQUESTID lpRequestID);

        //HRESULT extern WINAPI WFSOpen ( LPSTR lpszLogicalName, HAPP hApp, LPSTR lpszAppID, DWORD dwTraceLevel, DWORD dwTimeOut, DWORD dwSrvcVersionsRequired, LPWFSVERSION lpSrvcVersion, LPWFSVERSION lpSPIVersion, LPHSERVICE lphService);

        //HRESULT extern WINAPI WFSAsyncOpen ( LPSTR lpszLogicalName, HAPP hApp, LPSTR lpszAppID, DWORD dwTraceLevel, DWORD dwTimeOut, LPHSERVICE lphService, HWND hWnd, DWORD dwSrvcVersionsRequired, LPWFSVERSION lpSrvcVersion, LPWFSVERSION lpSPIVersion, LPREQUESTID lpRequestID);

        //HRESULT extern WINAPI WFSRegister ( HSERVICE hService, DWORD dwEventClass, HWND hWndReg);

        //HRESULT extern WINAPI WFSAsyncRegister ( HSERVICE hService, DWORD dwEventClass, HWND hWndReg, HWND hWnd, LPREQUESTID lpRequestID);

        //HRESULT extern WINAPI WFSSetBlockingHook ( XFSBLOCKINGHOOK lpBlockFunc, LPXFSBLOCKINGHOOK lppPrevFunc);

        //HRESULT extern WINAPI WFSStartUp ( DWORD dwVersionsRequired, LPWFSVERSION lpWFSVersion);

        //HRESULT extern WINAPI WFSUnhookBlockingHook ();

        //HRESULT extern WINAPI WFSUnlock ( HSERVICE hService);

        //HRESULT extern WINAPI WFSAsyncUnlock ( HSERVICE hService, HWND hWnd, LPREQUESTID lpRequestID);

        //HRESULT extern WINAPI WFMSetTraceLevel ( HSERVICE hService, DWORD dwTraceLevel);


        ///*   restore alignment   */
        //#pragma pack(pop)

        //#ifdef __cplusplus
        //}       /*extern "C"*/
        //#endif

        //#endif  /* __inc_xfsapi__h */
    }//class XFSAPIh
} //namespace iopcts
