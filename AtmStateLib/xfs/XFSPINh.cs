/****************************************************************************
*                                                                           *
* xfspin.h XFS - Personal Identification Number Keypad (PIN) definitions    *
*                                                                           *
*            Version 3.20  (March 02 2011)                                  *
*                                                                           *
****************************************************************************/

//#ifndef __INC_XFSPIN__H
//public const int __INC_XFSPIN__H

//#ifdef __cplusplus
//extern "C" {
//#endif

//#include "XFSAPI.H"

///*   be aware of alignment   */
//#pragma pack(push,1)

namespace iopcts
{
    public class XFSPINh
    {

        /* values of WFSPINCAPS.wClass */

        public const int WFS_SERVICE_CLASS_PIN = (4);
        public const int WFS_SERVICE_CLASS_VERSION_PIN = (0x1403); /* Version 3.20 */
        public const string WFS_SERVICE_CLASS_NAME_PIN = "PIN";

        public const int PIN_SERVICE_OFFSET = (WFS_SERVICE_CLASS_PIN * 100);


        /* PIN Info Commands */

        public const int WFS_INF_PIN_STATUS = (PIN_SERVICE_OFFSET + 1);
        public const int WFS_INF_PIN_CAPABILITIES = (PIN_SERVICE_OFFSET + 2);
        public const int WFS_INF_PIN_KEY_DETAIL = (PIN_SERVICE_OFFSET + 4);
        public const int WFS_INF_PIN_FUNCKEY_DETAIL = (PIN_SERVICE_OFFSET + 5);
        public const int WFS_INF_PIN_HSM_TDATA = (PIN_SERVICE_OFFSET + 6);
        public const int WFS_INF_PIN_KEY_DETAIL_EX = (PIN_SERVICE_OFFSET + 7);
        public const int WFS_INF_PIN_SECUREKEY_DETAIL = (PIN_SERVICE_OFFSET + 8);
        public const int WFS_INF_PIN_QUERY_LOGICAL_HSM_DETAIL = (PIN_SERVICE_OFFSET + 9);
        public const int WFS_INF_PIN_QUERY_PCIPTS_DEVICE_ID = (PIN_SERVICE_OFFSET + 10);

        /* PIN Command Verbs */

        public const int WFS_CMD_PIN_CRYPT = (PIN_SERVICE_OFFSET + 1);
        public const int WFS_CMD_PIN_IMPORT_KEY = (PIN_SERVICE_OFFSET + 3);
        public const int WFS_CMD_PIN_GET_PIN = (PIN_SERVICE_OFFSET + 5);
        public const int WFS_CMD_PIN_GET_PINBLOCK = (PIN_SERVICE_OFFSET + 7);
        public const int WFS_CMD_PIN_GET_DATA = (PIN_SERVICE_OFFSET + 8);
        public const int WFS_CMD_PIN_INITIALIZATION = (PIN_SERVICE_OFFSET + 9);
        public const int WFS_CMD_PIN_LOCAL_DES = (PIN_SERVICE_OFFSET + 10);
        public const int WFS_CMD_PIN_LOCAL_EUROCHEQUE = (PIN_SERVICE_OFFSET + 11);
        public const int WFS_CMD_PIN_LOCAL_VISA = (PIN_SERVICE_OFFSET + 12);
        public const int WFS_CMD_PIN_CREATE_OFFSET = (PIN_SERVICE_OFFSET + 13);
        public const int WFS_CMD_PIN_DERIVE_KEY = (PIN_SERVICE_OFFSET + 14);
        public const int WFS_CMD_PIN_PRESENT_IDC = (PIN_SERVICE_OFFSET + 15);
        public const int WFS_CMD_PIN_LOCAL_BANKSYS = (PIN_SERVICE_OFFSET + 16);
        public const int WFS_CMD_PIN_BANKSYS_IO = (PIN_SERVICE_OFFSET + 17);
        public const int WFS_CMD_PIN_RESET = (PIN_SERVICE_OFFSET + 18);
        public const int WFS_CMD_PIN_HSM_SET_TDATA = (PIN_SERVICE_OFFSET + 19);
        public const int WFS_CMD_PIN_SECURE_MSG_SEND = (PIN_SERVICE_OFFSET + 20);
        public const int WFS_CMD_PIN_SECURE_MSG_RECEIVE = (PIN_SERVICE_OFFSET + 21);
        public const int WFS_CMD_PIN_GET_JOURNAL = (PIN_SERVICE_OFFSET + 22);
        public const int WFS_CMD_PIN_IMPORT_KEY_EX = (PIN_SERVICE_OFFSET + 23);
        public const int WFS_CMD_PIN_ENC_IO = (PIN_SERVICE_OFFSET + 24);
        public const int WFS_CMD_PIN_HSM_INIT = (PIN_SERVICE_OFFSET + 25);
        public const int WFS_CMD_PIN_IMPORT_RSA_PUBLIC_KEY = (PIN_SERVICE_OFFSET + 26);
        public const int WFS_CMD_PIN_EXPORT_RSA_ISSUER_SIGNED_ITEM = (PIN_SERVICE_OFFSET + 27);
        public const int WFS_CMD_PIN_IMPORT_RSA_SIGNED_DES_KEY = (PIN_SERVICE_OFFSET + 28);
        public const int WFS_CMD_PIN_GENERATE_RSA_KEY_PAIR = (PIN_SERVICE_OFFSET + 29);
        public const int WFS_CMD_PIN_EXPORT_RSA_EPP_SIGNED_ITEM = (PIN_SERVICE_OFFSET + 30);
        public const int WFS_CMD_PIN_LOAD_CERTIFICATE = (PIN_SERVICE_OFFSET + 31);
        public const int WFS_CMD_PIN_GET_CERTIFICATE = (PIN_SERVICE_OFFSET + 32);
        public const int WFS_CMD_PIN_REPLACE_CERTIFICATE = (PIN_SERVICE_OFFSET + 33);
        public const int WFS_CMD_PIN_START_KEY_EXCHANGE = (PIN_SERVICE_OFFSET + 34);
        public const int WFS_CMD_PIN_IMPORT_RSA_ENCIPHERED_PKCS7_KEY = (PIN_SERVICE_OFFSET + 35);
        public const int WFS_CMD_PIN_EMV_IMPORT_PUBLIC_KEY = (PIN_SERVICE_OFFSET + 36);
        public const int WFS_CMD_PIN_DIGEST = (PIN_SERVICE_OFFSET + 37);
        public const int WFS_CMD_PIN_SECUREKEY_ENTRY = (PIN_SERVICE_OFFSET + 38);
        public const int WFS_CMD_PIN_GENERATE_KCV = (PIN_SERVICE_OFFSET + 39);
        public const int WFS_CMD_PIN_SET_GUIDANCE_LIGHT = (PIN_SERVICE_OFFSET + 41);
        public const int WFS_CMD_PIN_MAINTAIN_PIN = (PIN_SERVICE_OFFSET + 42);
        public const int WFS_CMD_PIN_KEYPRESS_BEEP = (PIN_SERVICE_OFFSET + 43);
        public const int WFS_CMD_PIN_SET_PINBLOCK_DATA = (PIN_SERVICE_OFFSET + 44);
        public const int WFS_CMD_PIN_SET_LOGICAL_HSM = (PIN_SERVICE_OFFSET + 45);
        public const int WFS_CMD_PIN_IMPORT_KEYBLOCK = (PIN_SERVICE_OFFSET + 46);
        public const int WFS_CMD_PIN_POWER_SAVE_CONTROL = (PIN_SERVICE_OFFSET + 47);

        /* PIN Messages */

        public const int WFS_EXEE_PIN_KEY = (PIN_SERVICE_OFFSET + 1);
        public const int WFS_SRVE_PIN_INITIALIZED = (PIN_SERVICE_OFFSET + 2);
        public const int WFS_SRVE_PIN_ILLEGAL_KEY_ACCESS = (PIN_SERVICE_OFFSET + 3);
        public const int WFS_SRVE_PIN_OPT_REQUIRED = (PIN_SERVICE_OFFSET + 4);
        public const int WFS_SRVE_PIN_HSM_TDATA_CHANGED = (PIN_SERVICE_OFFSET + 5);
        public const int WFS_SRVE_PIN_CERTIFICATE_CHANGE = (PIN_SERVICE_OFFSET + 6);
        public const int WFS_SRVE_PIN_HSM_CHANGED = (PIN_SERVICE_OFFSET + 7);
        public const int WFS_EXEE_PIN_ENTERDATA = (PIN_SERVICE_OFFSET + 8);
        public const int WFS_SRVE_PIN_DEVICEPOSITION = (PIN_SERVICE_OFFSET + 9);
        public const int WFS_SRVE_PIN_POWER_SAVE_CHANGE = (PIN_SERVICE_OFFSET + 10);

        /* values of WFSPINSTATUS.fwDevice */

        public const int WFS_PIN_DEVONLINE = XFSAPIh.WFS_STAT_DEVONLINE;
        public const int WFS_PIN_DEVOFFLINE = XFSAPIh.WFS_STAT_DEVOFFLINE;
        public const int WFS_PIN_DEVPOWEROFF = XFSAPIh.WFS_STAT_DEVPOWEROFF;
        public const int WFS_PIN_DEVNODEVICE = XFSAPIh.WFS_STAT_DEVNODEVICE;
        public const int WFS_PIN_DEVHWERROR = XFSAPIh.WFS_STAT_DEVHWERROR;
        public const int WFS_PIN_DEVUSERERROR = XFSAPIh.WFS_STAT_DEVUSERERROR;
        public const int WFS_PIN_DEVBUSY = XFSAPIh.WFS_STAT_DEVBUSY;
        public const int WFS_PIN_DEVFRAUDATTEMPT = XFSAPIh.WFS_STAT_DEVFRAUDATTEMPT;
        public const int WFS_PIN_DEVPOTENTIALFRAUD = XFSAPIh.WFS_STAT_DEVPOTENTIALFRAUD;

        /* values of WFSPINSTATUS.fwEncStat */

        public const int WFS_PIN_ENCREADY = (0);
        public const int WFS_PIN_ENCNOTREADY = (1);
        public const int WFS_PIN_ENCNOTINITIALIZED = (2);
        public const int WFS_PIN_ENCBUSY = (3);
        public const int WFS_PIN_ENCUNDEFINED = (4);
        public const int WFS_PIN_ENCINITIALIZED = (5);
        public const int WFS_PIN_ENCPINTAMPERED = (6);

        /* Size and max index of dwGuidLights array */

        public const int WFS_PIN_GUIDLIGHTS_SIZE = (32);
        public const int WFS_PIN_GUIDLIGHTS_MAX = (WFS_PIN_GUIDLIGHTS_SIZE - 1);

        /* Indices of WFSPINSTATUS.dwGuidLights [...]
                      WFSPINCAPS.dwGuidLights [...]
        */

        public const int WFS_PIN_GUIDANCE_PINPAD = (0);

        /* Values of WFSPINSTATUS.dwGuidLights [...]
                     WFSPINCAPS.dwGuidLights [...]
        */

        public const uint WFS_PIN_GUIDANCE_NOT_AVAILABLE = (0x00000000);
        public const uint WFS_PIN_GUIDANCE_OFF = (0x00000001);
        public const uint WFS_PIN_GUIDANCE_ON = (0x00000002);
        public const uint WFS_PIN_GUIDANCE_SLOW_FLASH = (0x00000004);
        public const uint WFS_PIN_GUIDANCE_MEDIUM_FLASH = (0x00000008);
        public const uint WFS_PIN_GUIDANCE_QUICK_FLASH = (0x00000010);
        public const uint WFS_PIN_GUIDANCE_CONTINUOUS = (0x00000080);
        public const uint WFS_PIN_GUIDANCE_RED = (0x00000100);
        public const uint WFS_PIN_GUIDANCE_GREEN = (0x00000200);
        public const uint WFS_PIN_GUIDANCE_YELLOW = (0x00000400);
        public const uint WFS_PIN_GUIDANCE_BLUE = (0x00000800);
        public const uint WFS_PIN_GUIDANCE_CYAN = (0x00001000);
        public const uint WFS_PIN_GUIDANCE_MAGENTA = (0x00002000);
        public const uint WFS_PIN_GUIDANCE_WHITE = (0x00004000);

        /* values for WFSPINSTATUS.fwAutoBeepMode and
        WFS_CMD_PIN_KEYPRESS_BEEP lpwMode parameter */

        public const uint WFS_PIN_BEEP_ON_ACTIVE = (0x0001);
        public const uint WFS_PIN_BEEP_ON_INACTIVE = (0x0002);

        /* values of WFSPINSTATUS.wDevicePosition
                     WFSPINDEVICEPOSITION.wPosition */

        public const int WFS_PIN_DEVICEINPOSITION = (0);
        public const int WFS_PIN_DEVICENOTINPOSITION = (1);
        public const int WFS_PIN_DEVICEPOSUNKNOWN = (2);
        public const int WFS_PIN_DEVICEPOSNOTSUPP = (3);

        /* values of WFSPINCAPS.fwType */

        public const uint WFS_PIN_TYPEEPP = (0x0001);
        public const uint WFS_PIN_TYPEEDM = (0x0002);
        public const uint WFS_PIN_TYPEHSM = (0x0004);

        /* values of WFSPINCAPS.fwAlgorithms, WFSPINCRYPT.wAlgorithm */

        public const uint WFS_PIN_CRYPTDESECB = (0x0001);
        public const uint WFS_PIN_CRYPTDESCBC = (0x0002);
        public const uint WFS_PIN_CRYPTDESCFB = (0x0004);
        public const uint WFS_PIN_CRYPTRSA = (0x0008);
        public const uint WFS_PIN_CRYPTECMA = (0x0010);
        public const uint WFS_PIN_CRYPTDESMAC = (0x0020);
        public const uint WFS_PIN_CRYPTTRIDESECB = (0x0040);
        public const uint WFS_PIN_CRYPTTRIDESCBC = (0x0080);
        public const uint WFS_PIN_CRYPTTRIDESCFB = (0x0100);
        public const uint WFS_PIN_CRYPTTRIDESMAC = (0x0200);
        public const uint WFS_PIN_CRYPTMAAMAC = (0x0400);

        /* values of WFSPINCAPS.fwPinFormats */

        public const uint WFS_PIN_FORM3624 = (0x0001);
        public const uint WFS_PIN_FORMANSI = (0x0002);
        public const uint WFS_PIN_FORMISO0 = (0x0004);
        public const uint WFS_PIN_FORMISO1 = (0x0008);
        public const uint WFS_PIN_FORMECI2 = (0x0010);
        public const uint WFS_PIN_FORMECI3 = (0x0020);
        public const uint WFS_PIN_FORMVISA = (0x0040);
        public const uint WFS_PIN_FORMDIEBOLD = (0x0080);
        public const uint WFS_PIN_FORMDIEBOLDCO = (0x0100);
        public const uint WFS_PIN_FORMVISA3 = (0x0200);
        public const uint WFS_PIN_FORMBANKSYS = (0x0400);
        public const uint WFS_PIN_FORMEMV = (0x0800);
        public const uint WFS_PIN_FORMISO3 = (0x2000);
        public const uint WFS_PIN_FORMAP = (0x4000);

        /* values of WFSPINCAPS.fwDerivationAlgorithms */

        public const uint WFS_PIN_CHIP_ZKA = (0x0001);


        /* values of WFSPINCAPS.fwPresentationAlgorithms */

        public const uint WFS_PIN_PRESENT_CLEAR = (0x0001);

        /* values of WFSPINCAPS.fwDisplay */

        public const uint WFS_PIN_DISPNONE = (1);
        public const uint WFS_PIN_DISPLEDTHROUGH = (2);
        public const uint WFS_PIN_DISPDISPLAY = (3);

        /* values of WFSPINCAPS.fwIDKey */

        public const uint WFS_PIN_IDKEYINITIALIZATION = (0x0001);
        public const uint WFS_PIN_IDKEYIMPORT = (0x0002);

        /* values of WFSPINCAPS.fwValidationAlgorithms */

        public const uint WFS_PIN_DES = (0x0001);
        public const uint WFS_PIN_EUROCHEQUE = (0x0002);
        public const uint WFS_PIN_VISA = (0x0004);
        public const uint WFS_PIN_DES_OFFSET = (0x0008);
        public const uint WFS_PIN_BANKSYS = (0x0010);

        /* values of WFSPINCAPS.fwKeyCheckModes and
                  WFSPINIMPORTKEYEX.wKeyCheckMode */

        public const uint WFS_PIN_KCVNONE = (0x0000);
        public const uint WFS_PIN_KCVSELF = (0x0001);
        public const uint WFS_PIN_KCVZERO = (0x0002);

        /* values of WFSPINCAPS.fwAutoBeep */

        public const uint WFS_PIN_BEEP_ACTIVE_AVAILABLE = (0x0001);
        public const uint WFS_PIN_BEEP_ACTIVE_SELECTABLE = (0x0002);
        public const uint WFS_PIN_BEEP_INACTIVE_AVAILABLE = (0x0004);
        public const uint WFS_PIN_BEEP_INACTIVE_SELECTABLE = (0x0008);

        /* values of WFSPINCAPS.fwKeyBlockImportFormats */

        public const uint WFS_PIN_ANSTR31KEYBLOCK = (0x0001);

        /* values of WFSPINKEYDETAIL.fwUse and values of WFSPINKEYDETAILEX.dwUse */

        public const uint WFS_PIN_USECRYPT = (0x0001);
        public const uint WFS_PIN_USEFUNCTION = (0x0002);
        public const uint WFS_PIN_USEMACING = (0x0004);
        public const uint WFS_PIN_USEKEYENCKEY = (0x0020);
        public const uint WFS_PIN_USENODUPLICATE = (0x0040);
        public const uint WFS_PIN_USESVENCKEY = (0x0080);
        public const uint WFS_PIN_USECONSTRUCT = (0x0100);
        public const uint WFS_PIN_USESECURECONSTRUCT = (0x0200);
        public const uint WFS_PIN_USEANSTR31MASTER = (0x0400);

        /* additional values for WFSPINKEYDETAILEX.dwUse */

        public const uint WFS_PIN_USEPINLOCAL = (0x00010000);
        public const uint WFS_PIN_USERSAPUBLIC = (0x00020000);
        public const uint WFS_PIN_USERSAPRIVATE = (0x00040000);
        public const uint WFS_PIN_USECHIPINFO = (0x00100000);
        public const uint WFS_PIN_USECHIPPIN = (0x00200000);
        public const uint WFS_PIN_USECHIPPS = (0x00400000);
        public const uint WFS_PIN_USECHIPMAC = (0x00800000);
        public const uint WFS_PIN_USECHIPLT = (0x01000000);
        public const uint WFS_PIN_USECHIPMACLZ = (0x02000000);
        public const uint WFS_PIN_USECHIPMACAZ = (0x04000000);
        public const uint WFS_PIN_USERSAPUBLICVERIFY = (0x08000000);
        public const uint WFS_PIN_USERSAPRIVATESIGN = (0x10000000);

        /* values of WFSPINFUNCKEYDETAIL.ulFuncMask */

        public const uint WFS_PIN_FK_0 = (0x00000001);
        public const uint WFS_PIN_FK_1 = (0x00000002);
        public const uint WFS_PIN_FK_2 = (0x00000004);
        public const uint WFS_PIN_FK_3 = (0x00000008);
        public const uint WFS_PIN_FK_4 = (0x00000010);
        public const uint WFS_PIN_FK_5 = (0x00000020);
        public const uint WFS_PIN_FK_6 = (0x00000040);
        public const uint WFS_PIN_FK_7 = (0x00000080);
        public const uint WFS_PIN_FK_8 = (0x00000100);
        public const uint WFS_PIN_FK_9 = (0x00000200);
        public const uint WFS_PIN_FK_ENTER = (0x00000400);
        public const uint WFS_PIN_FK_CANCEL = (0x00000800);
        public const uint WFS_PIN_FK_CLEAR = (0x00001000);
        public const uint WFS_PIN_FK_BACKSPACE = (0x00002000);
        public const uint WFS_PIN_FK_HELP = (0x00004000);
        public const uint WFS_PIN_FK_DECPOuint = (0x00008000);
        public const uint WFS_PIN_FK_00 = (0x00010000);
        public const uint WFS_PIN_FK_000 = (0x00020000);
        public const uint WFS_PIN_FK_RES1 = (0x00040000);
        public const uint WFS_PIN_FK_RES2 = (0x00080000);
        public const uint WFS_PIN_FK_RES3 = (0x00100000);
        public const uint WFS_PIN_FK_RES4 = (0x00200000);
        public const uint WFS_PIN_FK_RES5 = (0x00400000);
        public const uint WFS_PIN_FK_RES6 = (0x00800000);
        public const uint WFS_PIN_FK_RES7 = (0x01000000);
        public const uint WFS_PIN_FK_RES8 = (0x02000000);
        public const uint WFS_PIN_FK_OEM1 = (0x04000000);
        public const uint WFS_PIN_FK_OEM2 = (0x08000000);
        public const uint WFS_PIN_FK_OEM3 = (0x10000000);
        public const uint WFS_PIN_FK_OEM4 = (0x20000000);
        public const uint WFS_PIN_FK_OEM5 = (0x40000000);
        public const uint WFS_PIN_FK_OEM6 = (0x80000000);

        /* additional values of WFSPINFUNCKEYDETAIL.ulFuncMask */

        public const uint WFS_PIN_FK_UNUSED = (0x00000000);

        public const uint WFS_PIN_FK_A = WFS_PIN_FK_RES1;
        public const uint WFS_PIN_FK_B = WFS_PIN_FK_RES2;
        public const uint WFS_PIN_FK_C = WFS_PIN_FK_RES3;
        public const uint WFS_PIN_FK_D = WFS_PIN_FK_RES4;
        public const uint WFS_PIN_FK_E = WFS_PIN_FK_RES5;
        public const uint WFS_PIN_FK_F = WFS_PIN_FK_RES6;
        public const uint WFS_PIN_FK_SHIFT = WFS_PIN_FK_RES7;

        /* values of WFSPINFDK.ulFDK */

        public const uint WFS_PIN_FK_FDK01 = (0x00000001);
        public const uint WFS_PIN_FK_FDK02 = (0x00000002);
        public const uint WFS_PIN_FK_FDK03 = (0x00000004);
        public const uint WFS_PIN_FK_FDK04 = (0x00000008);
        public const uint WFS_PIN_FK_FDK05 = (0x00000010);
        public const uint WFS_PIN_FK_FDK06 = (0x00000020);
        public const uint WFS_PIN_FK_FDK07 = (0x00000040);
        public const uint WFS_PIN_FK_FDK08 = (0x00000080);
        public const uint WFS_PIN_FK_FDK09 = (0x00000100);
        public const uint WFS_PIN_FK_FDK10 = (0x00000200);
        public const uint WFS_PIN_FK_FDK11 = (0x00000400);
        public const uint WFS_PIN_FK_FDK12 = (0x00000800);
        public const uint WFS_PIN_FK_FDK13 = (0x00001000);
        public const uint WFS_PIN_FK_FDK14 = (0x00002000);
        public const uint WFS_PIN_FK_FDK15 = (0x00004000);
        public const uint WFS_PIN_FK_FDK16 = (0x00008000);
        public const uint WFS_PIN_FK_FDK17 = (0x00010000);
        public const uint WFS_PIN_FK_FDK18 = (0x00020000);
        public const uint WFS_PIN_FK_FDK19 = (0x00040000);
        public const uint WFS_PIN_FK_FDK20 = (0x00080000);
        public const uint WFS_PIN_FK_FDK21 = (0x00100000);
        public const uint WFS_PIN_FK_FDK22 = (0x00200000);
        public const uint WFS_PIN_FK_FDK23 = (0x00400000);
        public const uint WFS_PIN_FK_FDK24 = (0x00800000);
        public const uint WFS_PIN_FK_FDK25 = (0x01000000);
        public const uint WFS_PIN_FK_FDK26 = (0x02000000);
        public const uint WFS_PIN_FK_FDK27 = (0x04000000);
        public const uint WFS_PIN_FK_FDK28 = (0x08000000);
        public const uint WFS_PIN_FK_FDK29 = (0x10000000);
        public const uint WFS_PIN_FK_FDK30 = (0x20000000);
        public const uint WFS_PIN_FK_FDK31 = (0x40000000);
        public const uint WFS_PIN_FK_FDK32 = (0x80000000);

        /* values of WFSPINCRYPT.wMode */

        public const int WFS_PIN_MODEENCRYPT = (1);
        public const int WFS_PIN_MODEDECRYPT = (2);
        public const int WFS_PIN_MODERANDOM = (3);

        /* values of WFSPINENTRY.wCompletion */

        public const int WFS_PIN_COMPAUTO = (0);
        public const int WFS_PIN_COMPENTER = (1);
        public const int WFS_PIN_COMPCANCEL = (2);
        public const int WFS_PIN_COMPCONTINUE = (6);
        public const int WFS_PIN_COMPCLEAR = (7);
        public const int WFS_PIN_COMPBACKSPACE = (8);
        public const int WFS_PIN_COMPFDK = (9);
        public const int WFS_PIN_COMPHELP = (10);
        public const int WFS_PIN_COMPFK = (11);
        public const int WFS_PIN_COMPCONTFDK = (12);

        /* values of WFSPINSECMSG.wProtocol */

        public const int WFS_PIN_PROTISOAS = (1);
        public const int WFS_PIN_PROTISOLZ = (2);
        public const int WFS_PIN_PROTISOPS = (3);
        public const int WFS_PIN_PROTCHIPZKA = (4);
        public const int WFS_PIN_PROTRAWDATA = (5);
        public const int WFS_PIN_PROTPBM = (6);
        public const int WFS_PIN_PROTHSMLDI = (7);
        public const int WFS_PIN_PROTGENAS = (8);
        public const int WFS_PIN_PROTCHIPINCHG = (9);
        public const int WFS_PIN_PROTPINCMP = (10);
        public const int WFS_PIN_PROTISOPINCHG = (11);

        /* values of WFSPINHSMINIT.wInitMode. */

        public const int WFS_PIN_INITTEMP = (1);
        public const int WFS_PIN_INITDEFINITE = (2);
        public const int WFS_PIN_INITIRREVERSIBLE = (3);

        /* values of WFSPINENCIO.wProtocol and WFSPINCAPS.fwENCIOProtocols */

        public const int WFS_PIN_ENC_PROT_CH = (0x0001);
        public const int WFS_PIN_ENC_PROT_GIECB = (0x0002);
        public const int WFS_PIN_ENC_PROT_LUX = (0x0004);

        /* values for WFS_SRVE_PIN_CERTIFICATE_CHANGE and WFSPINSTATUS.dwCertificateState */

        public const int WFS_PIN_CERT_SECONDARY = (0x00000002);

        /* values for WFSPINSTATUS.dwCertificateState*/

        public const int WFS_PIN_CERT_UNKNOWN = (0x00000000);
        public const int WFS_PIN_CERT_PRIMARY = (0x00000001);
        public const int WFS_PIN_CERT_NOTREADY = (0x00000004);

        /* Values for WFSPINCAPS.dwRSAAuthenticationScheme and the fast-track Capabilities 
        lpszExtra parameter, REMOTE_KEY_SCHEME. */

        public const int WFS_PIN_RSA_AUTH_2PARTY_SIG = (0x00000001);
        public const int WFS_PIN_RSA_AUTH_3PARTY_CERT = (0x00000002);

        /* Values for WFSPINCAPS.dwSignatureScheme and the fast-track Capabilities lpzExtra parameter, SIGNATURE_CAPABILITIES. */

        public const int WFS_PIN_SIG_GEN_RSA_KEY_PAIR = (0x00000001);
        public const int WFS_PIN_SIG_RANDOM_NUMBER = (0x00000002);
        public const int WFS_PIN_SIG_EXPORT_EPP_ID = (0x00000004);
        public const int WFS_PIN_SIG_ENHANCED_RKL = (0x00000008);

        /* values of WFSPINIMPORTRSAPUBLICKEY.dwRSASignatureAlgorithm and 
        WFSPINCAPS.dwRSASignatureAlgorithm */

        public const int WFS_PIN_SIGN_NA = (0);
        public const int WFS_PIN_SIGN_RSASSA_PKCS1_V1_5 = (0x00000001);
        public const int WFS_PIN_SIGN_RSASSA_PSS = (0x00000002);

        /* values of WFSPINIMPORTRSAPUBLICKEYOUTPUT.dwRSAKeyCheckMode */

        public const int WFS_PIN_RSA_KCV_NONE = (0x00000000);
        public const int WFS_PIN_RSA_KCV_SHA1 = (0x00000001);

        /* values of WFSPINEXPORTRSAISSUERSIGNEDITEM.wExportItemType and */
        /*           WFSPINEXPORTRSAEPPSIGNEDITEM.wExportItemType        */

        public const int WFS_PIN_EXPORT_EPP_ID = (0x0001);
        public const int WFS_PIN_EXPORT_PUBLIC_KEY = (0x0002);

        /* values of WFSPINIMPORTRSASIGNEDDESKEY.dwRSAEncipherAlgorithm and 
        WFSPINCAPS.dwRSACryptAlgorithm */

        public const int WFS_PIN_CRYPT_RSAES_PKCS1_V1_5 = (0x00000001);
        public const int WFS_PIN_CRYPT_RSAES_OAEP = (0x00000002);

        /* values of WFSPINGENERATERSAKEYPAIR.wExponentValue */

        public const int WFS_PIN_DEFAULT = (0);
        public const int WFS_PIN_EXPONENT_1 = (1);
        public const int WFS_PIN_EXPONENT_4 = (2);
        public const int WFS_PIN_EXPONENT_16 = (3);

        /* values of WFSPINIMPORTRSASIGNEDDESKEYOUTPUT.wKeyLength and */
        /*		WFSPINIMPORTRSAENCIPHEREDPKCS7KEYOUTPUT.wKeyLength */

        public const int WFS_PIN_KEYSINGLE = (0x0001);
        public const int WFS_PIN_KEYDOUBLE = (0x0002);

        /* values of WFSPINGETCERTIFICATE.wGetCertificate  */

        public const int WFS_PIN_PUBLICENCKEY = (1);
        public const int WFS_PIN_PUBLICVERIFICATIONKEY = (2);

        /* values for WFSPINEMVIMPORTPUBLICKEY.wImportScheme and WFSPINCAPS.lpwEMVImportSchemes */

        public const int WFS_PIN_EMV_IMPORT_PLAIN_CA = (1);
        public const int WFS_PIN_EMV_IMPORT_CHKSUM_CA = (2);
        public const int WFS_PIN_EMV_IMPORT_EPI_CA = (3);
        public const int WFS_PIN_EMV_IMPORT_ISSUER = (4);
        public const int WFS_PIN_EMV_IMPORT_ICC = (5);
        public const int WFS_PIN_EMV_IMPORT_ICC_PIN = (6);
        public const int WFS_PIN_EMV_IMPORT_PKCSV1_5_CA = (7);

        /* values for WFSPINDIGEST.wHashAlgorithm and WFSPINCAPS.fwEMVHashAlgorithm */

        public const int WFS_PIN_HASH_SHA1_DIGEST = (0x0001);

        /* values of WFSPINSECUREKEYDETAIL.fwKeyEntryMode */

        public const int WFS_PIN_SECUREKEY_NOTSUPP = (0x0000);
        public const int WFS_PIN_SECUREKEY_REG_SHIFT = (0x0001);
        public const int WFS_PIN_SECUREKEY_REG_UNIQUE = (0x0002);
        public const int WFS_PIN_SECUREKEY_IRREG_SHIFT = (0x0004);
        public const int WFS_PIN_SECUREKEY_IRREG_UNIQUE = (0x0008);

        /* values of WFSPINSTATUS.wAntiFraudModule */

        public const int WFS_PIN_AFMNOTSUPP = (0);
        public const int WFS_PIN_AFMOK = (1);
        public const int WFS_PIN_AFMINOP = (2);
        public const int WFS_PIN_AFMDEVICEDETECTED = (3);
        public const int WFS_PIN_AFMUNKNOWN = (4);

        /* XFS PIN Errors */

        public const int WFS_ERR_PIN_KEYNOTFOUND = (-(PIN_SERVICE_OFFSET + 0));
        public const int WFS_ERR_PIN_MODENOTSUPPORTED = (-(PIN_SERVICE_OFFSET + 1));
        public const int WFS_ERR_PIN_ACCESSDENIED = (-(PIN_SERVICE_OFFSET + 2));
        public const int WFS_ERR_PIN_INVALIDID = (-(PIN_SERVICE_OFFSET + 3));
        public const int WFS_ERR_PIN_DUPLICATEKEY = (-(PIN_SERVICE_OFFSET + 4));
        public const int WFS_ERR_PIN_KEYNOVALUE = (-(PIN_SERVICE_OFFSET + 6));
        public const int WFS_ERR_PIN_USEVIOLATION = (-(PIN_SERVICE_OFFSET + 7));
        public const int WFS_ERR_PIN_NOPIN = (-(PIN_SERVICE_OFFSET + 8));
        public const int WFS_ERR_PIN_INVALIDKEYLENGTH = (-(PIN_SERVICE_OFFSET + 9));
        public const int WFS_ERR_PIN_KEYINVALID = (-(PIN_SERVICE_OFFSET + 10));
        public const int WFS_ERR_PIN_KEYNOTSUPPORTED = (-(PIN_SERVICE_OFFSET + 11));
        public const int WFS_ERR_PIN_NOACTIVEKEYS = (-(PIN_SERVICE_OFFSET + 12));
        public const int WFS_ERR_PIN_NOTERMINATEKEYS = (-(PIN_SERVICE_OFFSET + 14));
        public const int WFS_ERR_PIN_MINIMUMLENGTH = (-(PIN_SERVICE_OFFSET + 15));
        public const int WFS_ERR_PIN_PROTOCOLNOTSUPP = (-(PIN_SERVICE_OFFSET + 16));
        public const int WFS_ERR_PIN_INVALIDDATA = (-(PIN_SERVICE_OFFSET + 17));
        public const int WFS_ERR_PIN_NOTALLOWED = (-(PIN_SERVICE_OFFSET + 18));
        public const int WFS_ERR_PIN_NOKEYRAM = (-(PIN_SERVICE_OFFSET + 19));
        public const int WFS_ERR_PIN_NOCHIPTRANSACTIVE = (-(PIN_SERVICE_OFFSET + 20));
        public const int WFS_ERR_PIN_ALGORITHMNOTSUPP = (-(PIN_SERVICE_OFFSET + 21));
        public const int WFS_ERR_PIN_FORMATNOTSUPP = (-(PIN_SERVICE_OFFSET + 22));
        public const int WFS_ERR_PIN_HSMSTATEINVALID = (-(PIN_SERVICE_OFFSET + 23));
        public const int WFS_ERR_PIN_MACINVALID = (-(PIN_SERVICE_OFFSET + 24));
        public const int WFS_ERR_PIN_PROTINVALID = (-(PIN_SERVICE_OFFSET + 25));
        public const int WFS_ERR_PIN_FORMATINVALID = (-(PIN_SERVICE_OFFSET + 26));
        public const int WFS_ERR_PIN_CONTENTINVALID = (-(PIN_SERVICE_OFFSET + 27));
        public const int WFS_ERR_PIN_SIG_NOT_SUPP = (-(PIN_SERVICE_OFFSET + 29));
        public const int WFS_ERR_PIN_INVALID_MOD_LEN = (-(PIN_SERVICE_OFFSET + 31));
        public const int WFS_ERR_PIN_INVALIDCERTSTATE = (-(PIN_SERVICE_OFFSET + 32));
        public const int WFS_ERR_PIN_KEY_GENERATION_ERROR = (-(PIN_SERVICE_OFFSET + 33));
        public const int WFS_ERR_PIN_EMV_VERIFY_FAILED = (-(PIN_SERVICE_OFFSET + 34));
        public const int WFS_ERR_PIN_RANDOMINVALID = (-(PIN_SERVICE_OFFSET + 35));
        public const int WFS_ERR_PIN_SIGNATUREINVALID = (-(PIN_SERVICE_OFFSET + 36));
        public const int WFS_ERR_PIN_SNSCDINVALID = (-(PIN_SERVICE_OFFSET + 37));
        public const int WFS_ERR_PIN_NORSAKEYPAIR = (-(PIN_SERVICE_OFFSET + 38));
        public const int WFS_ERR_PIN_INVALID_PORT = (-(PIN_SERVICE_OFFSET + 39));
        public const int WFS_ERR_PIN_POWERSAVETOOSHORT = (-(PIN_SERVICE_OFFSET + 40));
        public const int WFS_ERR_PIN_INVALIDHSM = (-(PIN_SERVICE_OFFSET + 41));


        ///*=================================================================*/
        ///* PIN Info Command Structures and variables */
        ///*=================================================================*/

        //typedef struct _wfs_hex_data
        //{
        //    USHORT                 usLength;
        //    LPBYTE                 lpbData;
        //} WFSXDATA, *LPWFSXDATA;

        //typedef struct _wfs_pin_status
        //{
        //    WORD                   fwDevice;
        //    WORD                   fwEncStat;
        //    LPSTR                  lpszExtra;
        //    DWORD                  dwGuidLights[WFS_PIN_GUIDLIGHTS_SIZE];
        //    WORD                   fwAutoBeepMode;
        //    DWORD                  dwCertificateState;
        //    WORD                   wDevicePosition;
        //    USHORT                 usPowerSaveRecoveryTime;
        //    WORD                   wAntiFraudModule;
        //} WFSPINSTATUS, *LPWFSPINSTATUS;

        //typedef struct _wfs_pin_caps
        //{
        //    WORD                   wClass;
        //    WORD                   fwType;
        //    BOOL                   bCompound;
        //    USHORT                 usKeyNum;
        //    WORD                   fwAlgorithms;
        //    WORD                   fwPinFormats;
        //    WORD                   fwDerivationAlgorithms;
        //    WORD                   fwPresentationAlgorithms;
        //    WORD                   fwDisplay;
        //    BOOL                   bIDConnect;
        //    WORD                   fwIDKey;
        //    WORD                   fwValidationAlgorithms;
        //    WORD                   fwKeyCheckModes;
        //    LPSTR                  lpszExtra;
        //    DWORD                  dwGuidLights[WFS_PIN_GUIDLIGHTS_SIZE];
        //    BOOL                   bPINCanPersistAfterUse;
        //    WORD                   fwAutoBeep;
        //    LPSTR                  lpsHSMVendor;
        //    BOOL                   bHSMJournaling;
        //    DWORD                  dwRSAAuthenticationScheme;
        //    DWORD                  dwRSASignatureAlgorithm;
        //    DWORD                  dwRSACryptAlgorithm;
        //    DWORD                  dwRSAKeyCheckMode;
        //    DWORD                  dwSignatureScheme;
        //    LPWORD                 lpwEMVImportSchemes;
        //    WORD                   fwEMVHashAlgorithm;
        //    BOOL                   bKeyImportThroughParts;
        //    WORD                   fwENCIOProtocols;
        //    BOOL                   bTypeCombined;
        //    BOOL                   bSetPinblockDataRequired;
        //    WORD                   fwKeyBlockImportFormats;
        //    BOOL                   bPowerSaveControl;
        //    BOOL                   bAntiFraudModule;
        //} WFSPINCAPS, *LPWFSPINCAPS;

        //typedef struct _wfs_pin_key_detail
        //{
        //    LPSTR                  lpsKeyName;
        //    WORD                   fwUse;
        //    BOOL                   bLoaded;
        //    LPWFSXDATA             lpxKeyBlockHeader;
        //} WFSPINKEYDETAIL, *LPWFSPINKEYDETAIL;

        //typedef struct _wfs_pin_fdk
        //{
        //    ULONG                  ulFDK;
        //    USHORT                 usXPosition;
        //    USHORT                 usYPosition;
        //} WFSPINFDK, *LPWFSPINFDK;

        //typedef struct _wfs_pin_func_key_detail
        //{
        //    ULONG                  ulFuncMask;
        //    USHORT                 usNumberFDKs;
        //    LPWFSPINFDK            *lppFDKs;
        //} WFSPINFUNCKEYDETAIL, *LPWFSPINFUNCKEYDETAIL;

        //typedef struct _wfs_pin_key_detail_ex
        //{
        //    LPSTR                  lpsKeyName;
        //    DWORD                  dwUse;
        //    BYTE                   bGeneration;
        //    BYTE                   bVersion;
        //    BYTE                   bActivatingDate[4];
        //    BYTE                   bExpiryDate[4];
        //    BOOL                   bLoaded;
        //    LPWFSXDATA             lpxKeyBlockHeader;
        //} WFSPINKEYDETAILEX, *LPWFSPINKEYDETAILEX;

        ///* WFS_INF_PIN_SECUREKEY_DETAIL command key layout output structure */
        //typedef struct _wfs_pin_hex_keys
        //{
        //    USHORT                 usXPos;
        //    USHORT                 usYPos;
        //    USHORT                 usXSize;
        //    USHORT                 usYSize;
        //    ULONG                  ulFK;
        //    ULONG                  ulShiftFK;
        //} WFSPINHEXKEYS, *LPWFSPINHEXKEYS;

        ///* WFS_INF_PIN_SECUREKEY_DETAIL command output structure */
        //typedef struct _wfs_pin_secure_key_detail
        //{
        //    WORD                   fwKeyEntryMode;
        //    LPWFSPINFUNCKEYDETAIL  lpFuncKeyDetail;
        //    ULONG                  ulClearFDK;
        //    ULONG                  ulCancelFDK;
        //    ULONG                  ulBackspaceFDK;
        //    ULONG                  ulEnterFDK;
        //    WORD                   wColumns;
        //    WORD                   wRows;
        //    LPWFSPINHEXKEYS        *lppHexKeys;
        //} WFSPINSECUREKEYDETAIL, *LPWFSPINSECUREKEYDETAIL;

        ///* WFS_INF_PIN_PCIPTS_DEVICE_ID command output structure */
        //typedef struct _wfs_pin_pcipts_deviceid
        //{
        //    LPSTR                  lpszManufacturerIdentifier;
        //    LPSTR                  lpszModelIdentifier;
        //    LPSTR                  lpszHardwareIdentifier;
        //    LPSTR                  lpszFirmwareIdentifier;
        //    LPSTR                  lpszApplicationIdentifier;
        //} WFSPINPCIPTSDEVICEID, *LPWFSPINPCIPTSDEVICEID;

        ///*=================================================================*/
        ///* PIN Execute Command Structures */
        ///*=================================================================*/

        //typedef struct _wfs_pin_crypt
        //{
        //    WORD                   wMode;
        //    LPSTR                  lpsKey;
        //    LPWFSXDATA             lpxKeyEncKey;
        //    WORD                   wAlgorithm;
        //    LPSTR                  lpsStartValueKey;
        //    LPWFSXDATA             lpxStartValue;
        //    BYTE                   bPadding;
        //    BYTE                   bCompression;
        //    LPWFSXDATA             lpxCryptData;
        //} WFSPINCRYPT, *LPWFSPINCRYPT;

        //typedef struct _wfs_pin_import
        //{
        //    LPSTR                  lpsKey;
        //    LPSTR                  lpsEncKey;
        //    LPWFSXDATA             lpxIdent;
        //    LPWFSXDATA             lpxValue;
        //    WORD                   fwUse;
        //} WFSPINIMPORT, *LPWFSPINIMPORT;

        //typedef struct _wfs_pin_derive
        //{
        //    WORD                   wDerivationAlgorithm;
        //    LPSTR                  lpsKey;
        //    LPSTR                  lpsKeyGenKey;
        //    LPSTR                  lpsStartValueKey;
        //    LPWFSXDATA             lpxStartValue;
        //    BYTE                   bPadding;
        //    LPWFSXDATA             lpxInputData;
        //    LPWFSXDATA             lpxIdent;
        // } WFSPINDERIVE, *LPWFSPINDERIVE;

        //typedef struct _wfs_pin_getpin
        //{
        //    USHORT                 usMinLen;
        //    USHORT                 usMaxLen;
        //    BOOL                   bAutoEnd;
        //    CHAR                   cEcho;
        //    ULONG                  ulActiveFDKs;
        //    ULONG                  ulActiveKeys;
        //    ULONG                  ulTerminateFDKs;
        //    ULONG                  ulTerminateKeys;
        //} WFSPINGETPIN, *LPWFSPINGETPIN;

        //typedef struct _wfs_pin_entry
        //{
        //    USHORT                 usDigits;
        //    WORD                   wCompletion;
        //} WFSPINENTRY, *LPWFSPINENTRY;

        //typedef struct _wfs_pin_local_des
        //{
        //    LPSTR                  lpsValidationData;
        //    LPSTR                  lpsOffset;
        //    BYTE                   bPadding;
        //    USHORT                 usMaxPIN;
        //    USHORT                 usValDigits;
        //    BOOL                   bNoLeadingZero;
        //    LPSTR                  lpsKey;
        //    LPWFSXDATA             lpxKeyEncKey;
        //    LPSTR                  lpsDecTable;
        //} WFSPINLOCALDES, *LPWFSPINLOCALDES;

        //typedef struct _wfs_pin_create_offset
        //{
        //    LPSTR                  lpsValidationData;
        //    BYTE                   bPadding;
        //    USHORT                 usMaxPIN;
        //    USHORT                 usValDigits;
        //    LPSTR                  lpsKey;
        //    LPWFSXDATA             lpxKeyEncKey;
        //    LPSTR                  lpsDecTable;
        //} WFSPINCREATEOFFSET, *LPWFSPINCREATEOFFSET;

        //typedef struct _wfs_pin_local_eurocheque
        //{
        //    LPSTR                  lpsEurochequeData;
        //    LPSTR                  lpsPVV;
        //    WORD                   wFirstEncDigits;
        //    WORD                   wFirstEncOffset;
        //    WORD                   wPVVDigits;
        //    WORD                   wPVVOffset;
        //    LPSTR                  lpsKey;
        //    LPWFSXDATA             lpxKeyEncKey;
        //    LPSTR                  lpsDecTable;
        //} WFSPINLOCALEUROCHEQUE, *LPWFSPINLOCALEUROCHEQUE;

        //typedef struct _wfs_pin_local_visa
        //{
        //    LPSTR                  lpsPAN;
        //    LPSTR                  lpsPVV;
        //    WORD                   wPVVDigits;
        //    LPSTR                  lpsKey;
        //    LPWFSXDATA             lpxKeyEncKey;
        //} WFSPINLOCALVISA, *LPWFSPINLOCALVISA;

        //typedef struct _wfs_pin_presentidc
        //{
        //    WORD                   wPresentAlgorithm;
        //    WORD                   wChipProtocol;
        //    ULONG                  ulChipDataLength;
        //    LPBYTE                 lpbChipData;
        //    LPVOID                 lpAlgorithmData;
        //} WFSPINPRESENTIDC, *LPWFSPINPRESENTIDC;

        //typedef struct _wfs_pin_present_result
        //{
        //    WORD                   wChipProtocol;
        //    ULONG                  ulChipDataLength;
        //    LPBYTE                 lpbChipData;
        //} WFSPINPRESENTRESULT, *LPWFSPINPRESENTRESULT;

        //typedef struct _wfs_pin_presentclear
        //{
        //    ULONG                  ulPINPointer;
        //    USHORT                 usPINOffset;
        //} WFSPINPRESENTCLEAR, *LPWFSPINPRESENTCLEAR;

        //typedef struct _wfs_pin_block
        //{
        //    LPSTR                  lpsCustomerData;
        //    LPSTR                  lpsXORData;
        //    BYTE                   bPadding;
        //    WORD                   wFormat;
        //    LPSTR                  lpsKey;
        //    LPSTR                  lpsKeyEncKey;
        //} WFSPINBLOCK, *LPWFSPINBLOCK;

        //typedef struct _wfs_pin_getdata
        //{
        //    USHORT                 usMaxLen;
        //    BOOL                   bAutoEnd;
        //    ULONG                  ulActiveFDKs;
        //    ULONG                  ulActiveKeys;
        //    ULONG                  ulTerminateFDKs;
        //    ULONG                  ulTerminateKeys;
        //} WFSPINGETDATA, *LPWFSPINGETDATA;

        //typedef struct _wfs_pin_key
        //{
        //    WORD                   wCompletion;
        //    ULONG                  ulDigit;
        //} WFSPINKEY, *LPWFSPINKEY;

        //typedef struct _wfs_pin_data
        //{
        //    USHORT                 usKeys;
        //    LPWFSPINKEY            *lpPinKeys;
        //    WORD                   wCompletion;
        //} WFSPINDATA, *LPWFSPINDATA;

        //typedef struct _wfs_pin_init
        //{
        //    LPWFSXDATA             lpxIdent;
        //    LPWFSXDATA             lpxKey;
        //} WFSPININIT, *LPWFSPININIT;

        //typedef struct _wfs_pin_local_banksys
        //{
        //    LPWFSXDATA             lpxATMVAC;
        //} WFSPINLOCALBANKSYS, *LPWFSPINLOCALBANKSYS;

        //typedef struct _wfs_pin_banksys_io
        //{
        //    ULONG                  ulLength;
        //    LPBYTE                 lpbData;
        //} WFSPINBANKSYSIO, *LPWFSPINBANKSYSIO;

        //typedef struct _wfs_pin_secure_message
        //    {
        //    WORD                   wProtocol;
        //    ULONG                  ulLength;
        //    LPBYTE                 lpbMsg;
        //} WFSPINSECMSG, *LPWFSPINSECMSG;

        //typedef struct _wfs_pin_import_key_ex
        //{
        //    LPSTR                  lpsKey;
        //    LPSTR                  lpsEncKey;
        //    LPWFSXDATA             lpxValue;
        //    LPWFSXDATA             lpxControlVector;
        //    DWORD                  dwUse;
        //    WORD                   wKeyCheckMode;
        //    LPWFSXDATA             lpxKeyCheckValue;
        //} WFSPINIMPORTKEYEX, *LPWFSPINIMPORTKEYEX;

        //typedef struct _wfs_pin_enc_io
        //{
        //    WORD                   wProtocol;
        //    ULONG                  ulDataLength;
        //    LPVOID                 lpvData;
        //} WFSPINENCIO, *LPWFSPINENCIO;

        ///* WFS_CMD_PIN_SECUREKEY_ENTRY command input structure */
        //typedef struct _wfs_pin_secure_key_entry
        //{
        //USHORT                     usKeyLen;
        //BOOL                       bAutoEnd;
        //ULONG                      ulActiveFDKs;
        //ULONG                      ulActiveKeys;
        //ULONG                      ulTerminateFDKs;
        //ULONG                      ulTerminateKeys;
        //WORD                       wVerificationType;
        //} WFSPINSECUREKEYENTRY, *LPWFSPINSECUREKEYENTRY;

        ///* WFS_CMD_PIN_SECUREKEY_ENTRY command output structure */
        //typedef struct _wfs_pin_secure_key_entry_out
        //{
        //USHORT                     usDigits;
        //WORD                       wCompletion;
        //LPWFSXDATA                 lpxKCV;
        //} WFSPINSECUREKEYENTRYOUT, *LPWFSPINSECUREKEYENTRYOUT;

        ///* WFS_CDM_PIN_IMPORT_KEYBLOCK command input structure */
        //typedef struct _wfs_pin_import_key_block
        //{
        //    LPSTR                  lpsKey;
        //    LPSTR                  lpsEncKey;
        //    LPWFSXDATA             lpxKeyBlock;
        //} WFSPINIMPORTKEYBLOCK, *LPWFSPINIMPORTKEYBLOCK;

        //typedef struct _wfs_pin_import_rsa_public_key
        //{
        //    LPSTR                  lpsKey;
        //    LPWFSXDATA             lpxValue;
        //    DWORD                  dwUse;
        //    LPSTR                  lpsSigKey;
        //    DWORD                  dwRSASignatureAlgorithm;
        //    LPWFSXDATA             lpxSignature;
        //} WFSPINIMPORTRSAPUBLICKEY, *LPWFSPINIMPORTRSAPUBLICKEY;

        //typedef struct _wfs_pin_import_rsa_public_key_output
        //{
        //    DWORD        dwRSAKeyCheckMode;
        //    LPWFSXDATA   lpxKeyCheckValue;
        //} WFSPINIMPORTRSAPUBLICKEYOUTPUT, *LPWFSPINIMPORTRSAPUBLICKEYOUTPUT;

        //typedef struct _wfs_pin_export_rsa_issuer_signed_item
        //{
        //    WORD                   wExportItemType;
        //    LPSTR                  lpsName;
        //} WFSPINEXPORTRSAISSUERSIGNEDITEM, *LPWFSPINEXPORTRSAISSUERSIGNEDITEM;

        //typedef struct _wfs_pin_export_rsa_issuer_signed_item_output
        //{
        //    LPWFSXDATA             lpxValue;
        //    DWORD                  dwRSASignatureAlgorithm;
        //    LPWFSXDATA             lpxSignature;
        //} WFSPINEXPORTRSAISSUERSIGNEDITEMOUTPUT, *LPWFSPINEXPORTRSAISSUERSIGNEDITEMOUTPUT;

        //typedef struct _wfs_pin_import_rsa_signed_des_key
        //{
        //    LPSTR                  lpsKey;
        //    LPSTR                  lpsDecryptKey;
        //    DWORD                  dwRSAEncipherAlgorithm;
        //    LPWFSXDATA             lpxValue;
        //    DWORD                  dwUse;
        //    LPSTR                  lpsSigKey;
        //    DWORD                  dwRSASignatureAlgorithm;
        //    LPWFSXDATA             lpxSignature;
        //} WFSPINIMPORTRSASIGNEDDESKEY, *LPWFSPINIMPORTRSASIGNEDDESKEY;

        //typedef struct _wfs_pin_import_rsa_signed_des_key_output
        //{
        //    WORD                   wKeyLength;
        //    WORD                   wKeyCheckMode;
        //    LPWFSXDATA             lpxKeyCheckValue;
        //} WFSPINIMPORTRSASIGNEDDESKEYOUTPUT, *LPWFSPINIMPORTRSASIGNEDDESKEYOUTPUT;

        //typedef struct _wfs_pin_generate_rsa_key
        //{
        //    LPSTR                  lpsKey;
        //    DWORD                  dwUse;
        //    WORD                   wModulusLength;
        //    WORD                   wExponentValue;
        //} WFSPINGENERATERSAKEYPAIR, *LPWFSPINGENERATERSAKEYPAIR;

        //typedef struct _wfs_pin_export_rsa_epp_signed_item
        //{
        //    WORD                   wExportItemType;
        //    LPSTR                  lpsName;
        //    LPSTR                  lpsSigKey;
        //    DWORD                  dwSignatureAlgorithm;
        //} WFSPINEXPORTRSAEPPSIGNEDITEM, *LPWFSPINEXPORTRSAEPPSIGNEDITEM;

        //typedef struct _wfs_pin_export_rsa_epp_signed_item_output
        //{
        //    LPWFSXDATA             lpxValue;
        //    LPWFSXDATA             lpxSelfSignature;
        //    LPWFSXDATA             lpxSignature;
        //} WFSPINEXPORTRSAEPPSIGNEDITEMOUTPUT, *LPWFSPINEXPORTRSAEPPSIGNEDITEMOUTPUT;

        //typedef struct _wfs_pin_load_certificate
        //{
        //    LPWFSXDATA             lpxLoadCertificate;
        //} WFSPINLOADCERTIFICATE, *LPWFSPINLOADCERTIFICATE;

        //typedef struct _wfs_pin_load_certificate_output
        //{
        //    LPWFSXDATA             lpxCertificateData;
        //} WFSPINLOADCERTIFICATEOUTPUT, *LPWFSPINLOADCERTIFICATEOUTPUT;

        //typedef struct _wfs_pin_get_certificate
        //{
        //    WORD                   wGetCertificate;
        //} WFSPINGETCERTIFICATE, *LPWFSPINGETCERTIFICATE;

        //typedef struct _wfs_pin_get_certificate_output
        //{
        //    LPWFSXDATA             lpxCertificate;
        //} WFSPINGETCERTIFICATEOUTPUT, *LPWFSPINGETCERTIFICATEOUTPUT;

        //typedef struct _wfs_pin_replace_certificate
        //{
        //    LPWFSXDATA             lpxReplaceCertificate;
        //} WFSPINREPLACECERTIFICATE, *LPWFSPINREPLACECERTIFICATE;

        //typedef struct _wfs_pin_replace_certificate_output
        //{
        //   LPWFSXDATA              lpxNewCertificateData;
        //} WFSPINREPLACECERTIFICATEOUTPUT, *LPWFSPINREPLACECERTIFICATEOUTPUT;

        //typedef struct _wfs_pin_start_key_exchange
        //{
        //    LPWFSXDATA             lpxRandomItem;
        //} WFSPINSTARTKEYEXCHANGE, *LPWFSPINSTARTKEYEXCHANGE;

        //typedef struct _wfs_pin_import_rsa_enciphered_pkcs7_key
        //{
        //    LPWFSXDATA             lpxImportRSAKeyIn;
        //    LPSTR                  lpsKey;
        //    DWORD                  dwUse;
        //} WFSPINIMPORTRSAENCIPHEREDPKCS7KEY, *LPWFSPINIMPORTRSAENCIPHEREDPKCS7KEY;

        //typedef struct _wfs_pin_import_rsa_enciphered_pkcs7_key_output
        //{
        //    WORD                   wKeyLength;
        //    LPWFSXDATA             lpxRSAData;
        //}WFSPINIMPORTRSAENCIPHEREDPKCS7KEYOUTPUT, *LPWFSPINIMPORTRSAENCIPHEREDPKCS7KEYOUTPUT;

        //typedef struct _wfs_pin_emv_import_public_key
        //{
        //    LPSTR                  lpsKey;
        //    DWORD                  dwUse;
        //    WORD                   wImportScheme;
        //    LPWFSXDATA             lpxImportData;
        //    LPSTR                  lpsSigKey;
        //} WFSPINEMVIMPORTPUBLICKEY, *LPWFSPINEMVIMPORTPUBLICKEY;
        //typedef struct _wfs_pin_emv_import_public_key_output
        //{
        //    LPSTR                  lpsExpiryDate;
        //} WFSPINEMVIMPORTPUBLICKEYOUTPUT, *LPWFSPINEMVIMPORTPUBLICKEYOUTPUT;

        //typedef struct _wfs_pin_digest
        //{
        //    WORD                   wHashAlgorithm;
        //    LPWFSXDATA             lpxDigestInput;
        //} WFSPINDIGEST, *LPWFSPINDIGEST;

        //typedef struct _wfs_pin_digest_output
        //{
        //    LPWFSXDATA             lpxDigestOutput;
        //} WFSPINDIGESTOUTPUT, *LPWFSPINDIGESTOUTPUT;

        //typedef struct _wfs_pin_hsm_init
        //{
        //    WORD                   wInitMode;
        //    LPWFSXDATA             lpxOnlineTime;
        //} WFSPINHSMINIT, *LPWFSPINHSMINIT;

        //typedef struct _wfs_pin_generate_KCV
        //{
        //    LPSTR                  lpsKey;
        //    WORD                   wKeyCheckMode;
        //} WFSPINGENERATEKCV, *LPWFSPINGENERATEKCV;

        //typedef struct _wfs_pin_kcv
        //{
        //    LPWFSXDATA             lpxKCV;
        //} WFSPINKCV, *LPWFSPINKCV;

        //typedef struct _wfs_pin_set_guidlight
        //{
        //    WORD                   wGuidLight;
        //    DWORD                  dwCommand;
        //} WFSPINSETGUIDLIGHT, *LPWFSPINSETGUIDLIGHT;

        //typedef struct _wfs_pin_maintain_pin
        //{
        //    BOOL                   bMaintainPIN;
        //} WFSPINMAINTAINPIN, *LPWFSPINMAINTAINPIN;

        //typedef struct _wfs_pin_hsm_info
        //{
        //    WORD                   wHSMSerialNumber;
        //    LPSTR                  lpsZKAID;
        //} WFSPINHSMINFO, *LPWFSPINHSMINFO;

        //typedef struct _wfs_pin_hsm_detail
        //{
        //    WORD                   wActiveLogicalHSM;
        //    LPWFSPINHSMINFO        *lppHSMInfo;
        //} WFSPINHSMDETAIL, *LPWFSPINHSMDETAIL;


        //typedef struct _wfs_pin_hsm_identifier
        //{
        //    WORD                   wHSMSerialNumber;
        //} WFSPINHSMIDENTIFIER, *LPWFSPINHSMIDENTIFIER;

        //typedef struct _wfs_pin_power_save_control
        //{
        //    USHORT                 usMaxPowerSaveRecoveryTime;
        //} WFSPINPOWERSAVECONTROL, *LPWFSPINPOWERSAVECONTROL;


        ///*=================================================================*/
        ///* PIN Message Structures */
        ///*=================================================================*/

        //typedef struct _wfs_pin_access
        //{
        //    LPSTR                  lpsKeyName;
        //    LONG                   lErrorCode;
        //} WFSPINACCESS, *LPWFSPINACCESS;

        //typedef struct _wfs_pin_device_position
        //{
        //    WORD                   wPosition;
        //} WFSPINDEVICEPOSITION, *LPWFSPINDEVICEPOSITION;

        //typedef struct _wfs_pin_power_save_change
        //{
        //    USHORT                 usPowerSaveRecoveryTime;
        //} WFSPINPOWERSAVECHANGE, *LPWFSPINPOWERSAVECHANGE;


    };//class XFSPINh
}//namespace

///* restore alignment */
//#pragma pack(pop)

//#ifdef __cplusplus
//}       /*extern "C"*/
//#endif

//#endif    /* __INC_XFSPIN__H */
