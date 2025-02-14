/******************************************************************************
*                                                                             *
* xfsptr.h    XFS - Banking Printer (PTR) definitions                         *
*               (receipt, journal, passbook and document printer)             *
*                                                                             *
*             Version 3.20  (March 02 2011)                                   *
*                                                                             *
******************************************************************************/

//#ifndef __INC_XFSPTR__H
//public const int __INC_XFSPTR__H

//#ifdef __cplusplus
//extern "C" {
//#endif

//#include "XFSAPI.H"

///* be aware of alignment */
//#pragma pack(push,1)

namespace iopcts
{
    public class XFSPTRh
    {

        /* value of WFSPTRCAPS.wClass */

        public const int WFS_SERVICE_CLASS_PTR = (1);
        public const int WFS_SERVICE_CLASS_VERSION_PTR = (0x1403); /* Version 3.20 */
        public const string WFS_SERVICE_CLASS_NAME_PTR = "PTR";

        public const int PTR_SERVICE_OFFSET = (WFS_SERVICE_CLASS_PTR * 100);

        /* PTR Info Commands */

        public const int WFS_INF_PTR_STATUS = (PTR_SERVICE_OFFSET + 1);
        public const int WFS_INF_PTR_CAPABILITIES = (PTR_SERVICE_OFFSET + 2);
        public const int WFS_INF_PTR_FORM_LIST = (PTR_SERVICE_OFFSET + 3);
        public const int WFS_INF_PTR_MEDIA_LIST = (PTR_SERVICE_OFFSET + 4);
        public const int WFS_INF_PTR_QUERY_FORM = (PTR_SERVICE_OFFSET + 5);
        public const int WFS_INF_PTR_QUERY_MEDIA = (PTR_SERVICE_OFFSET + 6);
        public const int WFS_INF_PTR_QUERY_FIELD = (PTR_SERVICE_OFFSET + 7);
        public const int WFS_INF_PTR_CODELINE_MAPPING = (PTR_SERVICE_OFFSET + 8);

        /* PTR Execute Commands */

        public const int WFS_CMD_PTR_CONTROL_MEDIA = (PTR_SERVICE_OFFSET + 1);
        public const int WFS_CMD_PTR_PRINT_FORM = (PTR_SERVICE_OFFSET + 2);
        public const int WFS_CMD_PTR_READ_FORM = (PTR_SERVICE_OFFSET + 3);
        public const int WFS_CMD_PTR_RAW_DATA = (PTR_SERVICE_OFFSET + 4);
        public const int WFS_CMD_PTR_MEDIA_EXTENTS = (PTR_SERVICE_OFFSET + 5);
        public const int WFS_CMD_PTR_RESET_COUNT = (PTR_SERVICE_OFFSET + 6);
        public const int WFS_CMD_PTR_READ_IMAGE = (PTR_SERVICE_OFFSET + 7);
        public const int WFS_CMD_PTR_RESET = (PTR_SERVICE_OFFSET + 8);
        public const int WFS_CMD_PTR_RETRACT_MEDIA = (PTR_SERVICE_OFFSET + 9);
        public const int WFS_CMD_PTR_DISPENSE_PAPER = (PTR_SERVICE_OFFSET + 10);
        public const int WFS_CMD_PTR_SET_GUIDANCE_LIGHT = (PTR_SERVICE_OFFSET + 11);
        public const int WFS_CMD_PTR_PRINT_RAW_FILE = (PTR_SERVICE_OFFSET + 12);
        public const int WFS_CMD_PTR_LOAD_DEFINITION = (PTR_SERVICE_OFFSET + 13);
        public const int WFS_CMD_PTR_SUPPLY_REPLENISH = (PTR_SERVICE_OFFSET + 14);
        public const int WFS_CMD_PTR_POWER_SAVE_CONTROL = (PTR_SERVICE_OFFSET + 15);
        public const int WFS_CMD_PTR_CONTROL_PASSBOOK = (PTR_SERVICE_OFFSET + 16);

        /* PTR Messages */

        public const int WFS_EXEE_PTR_NOMEDIA = (PTR_SERVICE_OFFSET + 1);
        public const int WFS_EXEE_PTR_MEDIAINSERTED = (PTR_SERVICE_OFFSET + 2);
        public const int WFS_EXEE_PTR_FIELDERROR = (PTR_SERVICE_OFFSET + 3);
        public const int WFS_EXEE_PTR_FIELDWARNING = (PTR_SERVICE_OFFSET + 4);
        public const int WFS_USRE_PTR_RETRACTBINTHRESHOLD = (PTR_SERVICE_OFFSET + 5);
        public const int WFS_SRVE_PTR_MEDIATAKEN = (PTR_SERVICE_OFFSET + 6);
        public const int WFS_USRE_PTR_PAPERTHRESHOLD = (PTR_SERVICE_OFFSET + 7);
        public const int WFS_USRE_PTR_TONERTHRESHOLD = (PTR_SERVICE_OFFSET + 8);
        public const int WFS_SRVE_PTR_MEDIAINSERTED = (PTR_SERVICE_OFFSET + 9);
        public const int WFS_USRE_PTR_LAMPTHRESHOLD = (PTR_SERVICE_OFFSET + 10);
        public const int WFS_USRE_PTR_INKTHRESHOLD = (PTR_SERVICE_OFFSET + 11);
        public const int WFS_SRVE_PTR_MEDIADETECTED = (PTR_SERVICE_OFFSET + 12);
        public const int WFS_SRVE_PTR_RETRACTBINSTATUS = (PTR_SERVICE_OFFSET + 13);
        public const int WFS_EXEE_PTR_MEDIAPRESENTED = (PTR_SERVICE_OFFSET + 14);
        public const int WFS_SRVE_PTR_DEFINITIONLOADED = (PTR_SERVICE_OFFSET + 15);
        public const int WFS_EXEE_PTR_MEDIAREJECTED = (PTR_SERVICE_OFFSET + 16);
        public const int WFS_SRVE_PTR_MEDIAPRESENTED = (PTR_SERVICE_OFFSET + 17);
        public const int WFS_SRVE_PTR_MEDIAAUTORETRACTED = (PTR_SERVICE_OFFSET + 18);
        public const int WFS_SRVE_PTR_DEVICEPOSITION = (PTR_SERVICE_OFFSET + 19);
        public const int WFS_SRVE_PTR_POWER_SAVE_CHANGE = (PTR_SERVICE_OFFSET + 20);

        /* values of WFSPTRSTATUS.fwDevice */

        public const int WFS_PTR_DEVONLINE = XFSAPIh.WFS_STAT_DEVONLINE;
        public const int WFS_PTR_DEVOFFLINE = XFSAPIh.WFS_STAT_DEVOFFLINE;
        public const int WFS_PTR_DEVPOWEROFF = XFSAPIh.WFS_STAT_DEVPOWEROFF;
        public const int WFS_PTR_DEVNODEVICE = XFSAPIh.WFS_STAT_DEVNODEVICE;
        public const int WFS_PTR_DEVHWERROR = XFSAPIh.WFS_STAT_DEVHWERROR;
        public const int WFS_PTR_DEVUSERERROR = XFSAPIh.WFS_STAT_DEVUSERERROR;
        public const int WFS_PTR_DEVBUSY = XFSAPIh.WFS_STAT_DEVBUSY;
        public const int WFS_PTR_DEVFRAUDATTEMPT = XFSAPIh.WFS_STAT_DEVFRAUDATTEMPT;
        public const int WFS_PTR_DEVPOTENTIALFRAUD = XFSAPIh.WFS_STAT_DEVPOTENTIALFRAUD;

        /* values of WFSPTRSTATUS.fwMedia and
                     WFSPTRMEDIADETECTED.wPosition */

        public const int WFS_PTR_MEDIAPRESENT = (0);
        public const int WFS_PTR_MEDIANOTPRESENT = (1);
        public const int WFS_PTR_MEDIAJAMMED = (2);
        public const int WFS_PTR_MEDIANOTSUPP = (3);
        public const int WFS_PTR_MEDIAUNKNOWN = (4);
        public const int WFS_PTR_MEDIAENTERING = (5);
        public const int WFS_PTR_MEDIARETRACTED = (6);

        /* additional values for WFSPTRMEDIADETECTED.wPosition */
        public const int WFS_PTR_MEDIAEXPELLED = (7);

        /* Size and max index of WFSPTRSTATUS.fwPaper and
                                 WFSPTRSTATUS.wPaperType */

        public const int WFS_PTR_SUPPLYSIZE = (16);
        public const int WFS_PTR_SUPPLYMAX = (WFS_PTR_SUPPLYSIZE - 1);

        /* Indices of WFSPTRSTATUS.fwPaper [...] */

        public const int WFS_PTR_SUPPLYUPPER = (0);
        public const int WFS_PTR_SUPPLYLOWER = (1);
        public const int WFS_PTR_SUPPLYexternAL = (2);
        public const int WFS_PTR_SUPPLYAUX = (3);
        public const int WFS_PTR_SUPPLYAUX2 = (4);
        public const int WFS_PTR_SUPPLYPARK = (5);

        /* values of WFSPTRSTATUS.fwPaper and
                     WFSPTRPAPERTHRESHOLD.wPaperThreshold */

        public const int WFS_PTR_PAPERFULL = (0);
        public const int WFS_PTR_PAPERLOW = (1);
        public const int WFS_PTR_PAPEROUT = (2);
        public const int WFS_PTR_PAPERNOTSUPP = (3);
        public const int WFS_PTR_PAPERUNKNOWN = (4);
        public const int WFS_PTR_PAPERJAMMED = (5);

        /* values of WFSPTRSTATUS.fwToner */

        public const int WFS_PTR_TONERFULL = (0);
        public const int WFS_PTR_TONERLOW = (1);
        public const int WFS_PTR_TONEROUT = (2);
        public const int WFS_PTR_TONERNOTSUPP = (3);
        public const int WFS_PTR_TONERUNKNOWN = (4);

        /* values of WFSPTRSTATUS.fwInk */

        public const int WFS_PTR_INKFULL = (0);
        public const int WFS_PTR_INKLOW = (1);
        public const int WFS_PTR_INKOUT = (2);
        public const int WFS_PTR_INKNOTSUPP = (3);
        public const int WFS_PTR_INKUNKNOWN = (4);

        /* values of WFSPTRSTATUS.fwLamp */

        public const int WFS_PTR_LAMPOK = (0);
        public const int WFS_PTR_LAMPFADING = (1);
        public const int WFS_PTR_LAMPINOP = (2);
        public const int WFS_PTR_LAMPNOTSUPP = (3);
        public const int WFS_PTR_LAMPUNKNOWN = (4);

        /* values of WFSPTRRETRACTBINS.wRetractBin and
                     WFSPTRBINTHRESHOLD.wRetractBin */

        public const int WFS_PTR_RETRACTBINOK = (0);
        public const int WFS_PTR_RETRACTBINFULL = (1);
        public const int WFS_PTR_RETRACTNOTSUPP = (2); /* Deprecated */
        public const int WFS_PTR_RETRACTUNKNOWN = (3);
        public const int WFS_PTR_RETRACTBINHIGH = (4);

        /* additional values of WFSPTRRETRACTBINS.wRetractBin */

        public const int WFS_PTR_RETRACTBINMISSING = (5);

        /* Size and max index of dwGuidLights array */

        public const int WFS_PTR_GUIDLIGHTS_SIZE = (32);
        public const int WFS_PTR_GUIDLIGHTS_MAX = (WFS_PTR_GUIDLIGHTS_SIZE - 1);

        /* Indices of WFSPTRSTATUS.dwGuidLights [...]
                      WFSPTRCAPS.dwGuidLights [...] */

        public const int WFS_PTR_GUIDANCE_PRINTER = (0);

        /* Values of WFSPTRSTATUS.dwGuidLights [...]
                     WFSPTRCAPS.dwGuidLights [...] */

        public const int WFS_PTR_GUIDANCE_NOT_AVAILABLE = (0x00000000);
        public const int WFS_PTR_GUIDANCE_OFF = (0x00000001);
        public const int WFS_PTR_GUIDANCE_SLOW_FLASH = (0x00000004);
        public const int WFS_PTR_GUIDANCE_MEDIUM_FLASH = (0x00000008);
        public const int WFS_PTR_GUIDANCE_QUICK_FLASH = (0x00000010);
        public const int WFS_PTR_GUIDANCE_CONTINUOUS = (0x00000080);
        public const int WFS_PTR_GUIDANCE_RED = (0x00000100);
        public const int WFS_PTR_GUIDANCE_GREEN = (0x00000200);
        public const int WFS_PTR_GUIDANCE_YELLOW = (0x00000400);
        public const int WFS_PTR_GUIDANCE_BLUE = (0x00000800);
        public const int WFS_PTR_GUIDANCE_CYAN = (0x00001000);
        public const int WFS_PTR_GUIDANCE_MAGENTA = (0x00002000);
        public const int WFS_PTR_GUIDANCE_WHITE = (0x00004000);

        /* values of WFSPTRSTATUS.wDevicePosition
                     WFSPTRDEVICEPOSITION.wPosition */

        public const int WFS_PTR_DEVICEINPOSITION = (0);
        public const int WFS_PTR_DEVICENOTINPOSITION = (1);
        public const int WFS_PTR_DEVICEPOSUNKNOWN = (2);
        public const int WFS_PTR_DEVICEPOSNOTSUPP = (3);

        /* values of WFSPTRSTATUS.wPaperType */

        public const int WFS_PTR_PAPERSINGLESIDED = (0);
        public const int WFS_PTR_PAPERDUALSIDED = (1);
        public const int WFS_PTR_PAPERTYPEUNKNOWN = (2);

        /* values of WFSPTRSTATUS.wAntiFraudModule */

        public const int WFS_PTR_AFMNOTSUPP = (0);
        public const int WFS_PTR_AFMOK = (1);
        public const int WFS_PTR_AFMINOP = (2);
        public const int WFS_PTR_AFMDEVICEDETECTED = (3);
        public const int WFS_PTR_AFMUNKNOWN = (4);

        /* values of WFSPTRCAPS.fwType */

        public const int WFS_PTR_TYPERECEIPT = (0x0001);
        public const int WFS_PTR_TYPEPASSBOOK = (0x0002);
        public const int WFS_PTR_TYPEJOURNAL = (0x0004);
        public const int WFS_PTR_TYPEDOCUMENT = (0x0008);
        public const int WFS_PTR_TYPESCANNER = (0x0010);

        /* values of WFSPTRCAPS.wResolution,
                     WFSPTRPRINTFORM.wResolution */

        public const int WFS_PTR_RESLOW = (0x0001);
        public const int WFS_PTR_RESMED = (0x0002);
        public const int WFS_PTR_RESHIGH = (0x0004);
        public const int WFS_PTR_RESVERYHIGH = (0x0008);

        /* values of WFSPTRCAPS.fwReadForm */

        public const int WFS_PTR_READOCR = (0x0001);
        public const int WFS_PTR_READMICR = (0x0002);
        public const int WFS_PTR_READMSF = (0x0004);
        public const int WFS_PTR_READBARCODE = (0x0008);
        public const int WFS_PTR_READPAGEMARK = (0x0010);
        public const int WFS_PTR_READIMAGE = (0x0020);
        public const int WFS_PTR_READEMPTYLINE = (0x0040);

        /* values of WFSPTRCAPS.fwWriteForm */

        public const int WFS_PTR_WRITETEXT = (0x0001);
        public const int WFS_PTR_WRITEGRAPHICS = (0x0002);
        public const int WFS_PTR_WRITEOCR = (0x0004);
        public const int WFS_PTR_WRITEMICR = (0x0008);
        public const int WFS_PTR_WRITEMSF = (0x0010);
        public const int WFS_PTR_WRITEBARCODE = (0x0020);
        public const int WFS_PTR_WRITESTAMP = (0x0040);

        /* values of WFSPTRCAPS.fwExtents */

        public const int WFS_PTR_EXTHORIZONTAL = (0x0001);
        public const int WFS_PTR_EXTVERTICAL = (0x0002);

        /* values of WFSPTRCAPS.fwControl, dwMediaControl */

        public const int WFS_PTR_CTRLEJECT = (0x0001);
        public const int WFS_PTR_CTRLPERFORATE = (0x0002);
        public const int WFS_PTR_CTRLCUT = (0x0004);
        public const int WFS_PTR_CTRLSKIP = (0x0008);
        public const int WFS_PTR_CTRLFLUSH = (0x0010);
        public const int WFS_PTR_CTRLRETRACT = (0x0020);
        public const int WFS_PTR_CTRLSTACK = (0x0040);
        public const int WFS_PTR_CTRLPARTIALCUT = (0x0080);
        public const int WFS_PTR_CTRLALARM = (0x0100);
        public const int WFS_PTR_CTRLATPFORWARD = (0x0200);
        public const int WFS_PTR_CTRLATPBACKWARD = (0x0400);
        public const int WFS_PTR_CTRLTURNMEDIA = (0x0800);
        public const int WFS_PTR_CTRLSTAMP = (0x1000);
        public const int WFS_PTR_CTRLPARK = (0x2000);
        public const int WFS_PTR_CTRLEXPEL = (0x4000);
        public const int WFS_PTR_CTRLEJECTTOTRANSPORT = (0x8000);

        /* values of WFSPTRCAPS.fwPaperSources,
                     WFSFRMMEDIA.wPaperSources,
                     WFSPTRPRINTFORM.wPaperSource and
                     WFSPTRPAPERTHRESHOLD.wPaperSource */

        public const int WFS_PTR_PAPERANY = (0x0001);
        public const int WFS_PTR_PAPERUPPER = (0x0002);
        public const int WFS_PTR_PAPERLOWER = (0x0004);
        public const int WFS_PTR_PAPERexternAL = (0x0008);
        public const int WFS_PTR_PAPERAUX = (0x0010);
        public const int WFS_PTR_PAPERAUX2 = (0x0020);
        public const int WFS_PTR_PAPERPARK = (0x0040);

        /* values of WFSPTRCAPS.fwControlPassbook
                     WFSPTRCONTROLPASSBOOK.wAction */

        public const int WFS_PTR_PBKCTRLNOTSUPP = (0x0000);
        public const int WFS_PTR_PBKCTRLTURNFORWARD = (0x0001);
        public const int WFS_PTR_PBKCTRLTURNBACKWARD = (0x0002);
        public const int WFS_PTR_PBKCTRLCLOSEFORWARD = (0x0004);
        public const int WFS_PTR_PBKCTRLCLOSEBACKWARD = (0x0008);

        /* values of WFSPTRCAPS.fwImageType,
                     WFSPTRIMAGEREQUEST.wFrontImageType and
                     WFSPTRIMAGEREQUEST.wBackImageType */

        public const int WFS_PTR_IMAGETIF = (0x0001);
        public const int WFS_PTR_IMAGEWMF = (0x0002);
        public const int WFS_PTR_IMAGEBMP = (0x0004);
        public const int WFS_PTR_IMAGEJPG = (0x0008);

        /* values of WFSPTRCAPS.fwFrontImageColorFormat,
                     WFSPTRCAPS.fwBackImageColorFormat,
                     WFSPTRIMAGEREQUEST.wFrontImageColorFormat and
                     WFSPTRIMAGEREQUEST.wBackImageColorFormat */

        public const int WFS_PTR_IMAGECOLORBINARY = (0x0001);
        public const int WFS_PTR_IMAGECOLORGRAYSCALE = (0x0002);
        public const int WFS_PTR_IMAGECOLORFULL = (0x0004);

        /* values of WFSPTRCAPS.fwCodelineFormat and
                     WFSPTRIMAGEREQUEST.wCodelineFormat */

        public const int WFS_PTR_CODELINECMC7 = (0x0001);
        public const int WFS_PTR_CODELINEE13B = (0x0002);
        public const int WFS_PTR_CODELINEOCR = (0x0004);

        /* values of WFSPTRCAPS.fwImageSource,
                     WFSPTRIMAGEREQUEST.fwImageSource and
                     WFSPTRIMAGE.wImageSource */

        public const int WFS_PTR_IMAGEFRONT = (0x0001);
        public const int WFS_PTR_IMAGEBACK = (0x0002);
        public const int WFS_PTR_CODELINE = (0x0004);

        /* values of WFSPTRCAPS.fwCharSupport,
                     WFSFRMHEADER.fwCharSupport */

        public const int WFS_PTR_ASCII = (0x0001);
        public const int WFS_PTR_UNICODE = (0x0002);

        /* values of WFSPTRCAPS.fwCoercivityType */

        public const int WFS_PTR_COERCIVITYNOTSUPP = (0x0001);
        public const int WFS_PTR_COERCIVITYLOW = (0x0002);
        public const int WFS_PTR_COERCIVITYHIGH = (0x0004);
        public const int WFS_PTR_COERCIVITYAUTO = (0x0008);

        /* values of WFSPTRCAPS.wPrintSides */

        public const int WFS_PTR_PRINTSIDESNOTSUPP = (0x0000);
        public const int WFS_PTR_PRINTSIDESSINGLE = (0x0001);
        public const int WFS_PTR_PRINTSIDESDUAL = (0x0002);

        /* values of WFSFRMHEADER.wBase,
                     WFSFRMMEDIA.wBase,
                     WFSPTRMEDIAUNIT.wBase */

        public const int WFS_FRM_INCH = (0);
        public const int WFS_FRM_MM = (1);
        public const int WFS_FRM_ROWCOLUMN = (2);

        /* values of WFSFRMHEADER.wAlignment */

        public const int WFS_FRM_TOPLEFT = (0);
        public const int WFS_FRM_TOPRIGHT = (1);
        public const int WFS_FRM_BOTTOMLEFT = (2);
        public const int WFS_FRM_BOTTOMRIGHT = (3);

        /* values of WFSFRMHEADER.wOrientation */

        public const int WFS_FRM_PORTRAIT = (0);
        public const int WFS_FRM_LANDSCAPE = (1);

        /* values of WFSFRMMEDIA.fwMediaType */

        public const int WFS_FRM_MEDIAGENERIC = (0);
        public const int WFS_FRM_MEDIAPASSBOOK = (1);
        public const int WFS_FRM_MEDIAMULTIPART = (2);

        /* values of WFSFRMMEDIA.wFoldType */

        public const int WFS_FRM_FOLDNONE = (0);
        public const int WFS_FRM_FOLDHORIZONTAL = (1);
        public const int WFS_FRM_FOLDVERTICAL = (2);

        /* values of WFSFRMFIELD.fwType */

        public const int WFS_FRM_FIELDTEXT = (0);
        public const int WFS_FRM_FIELDMICR = (1);
        public const int WFS_FRM_FIELDOCR = (2);
        public const int WFS_FRM_FIELDMSF = (3);
        public const int WFS_FRM_FIELDBARCODE = (4);
        public const int WFS_FRM_FIELDGRAPHIC = (5);
        public const int WFS_FRM_FIELDPAGEMARK = (6);

        /* values of WFSFRMFIELD.fwClass */

        public const int WFS_FRM_CLASSSTATIC = (0);
        public const int WFS_FRM_CLASSOPTIONAL = (1);
        public const int WFS_FRM_CLASSREQUIRED = (2);

        /* values of WFSFRMFIELD.fwAccess */

        public const int WFS_FRM_ACCESSREAD = (0x0001);
        public const int WFS_FRM_ACCESSWRITE = (0x0002);

        /* values of WFSFRMFIELD.fwOverflow */

        public const int WFS_FRM_OVFTERMINATE = (0);
        public const int WFS_FRM_OVFTRUNCATE = (1);
        public const int WFS_FRM_OVFBESTFIT = (2);
        public const int WFS_FRM_OVFOVERWRITE = (3);
        public const int WFS_FRM_OVFWORDWRAP = (4);

        /* values of WFSFRMFIELD.wCoercivity */

        public const int WFS_FRM_COERCIVITYAUTO = (0);
        public const int WFS_FRM_COERCIVITYLOW = (1);
        public const int WFS_FRM_COERCIVITYHIGH = (2);

        /* values of WFSPTRFIELDFAIL.wFailure */

        public const int WFS_PTR_FIELDREQUIRED = (0);
        public const int WFS_PTR_FIELDSTATICOVWR = (1);
        public const int WFS_PTR_FIELDOVERFLOW = (2);
        public const int WFS_PTR_FIELDNOTFOUND = (3);
        public const int WFS_PTR_FIELDNOTREAD = (4);
        public const int WFS_PTR_FIELDNOTWRITE = (5);
        public const int WFS_PTR_FIELDHWERROR = (6);
        public const int WFS_PTR_FIELDTYPENOTSUPPORTED = (7);
        public const int WFS_PTR_FIELDGRAPHIC = (8);
        public const int WFS_PTR_CHARSETFORM = (9);

        /* values of WFSPTRPRINTFORM.wAlignment */

        public const int WFS_PTR_ALNUSEFORMDEFN = (0);
        public const int WFS_PTR_ALNTOPLEFT = (1);
        public const int WFS_PTR_ALNTOPRIGHT = (2);
        public const int WFS_PTR_ALNBOTTOMLEFT = (3);
        public const int WFS_PTR_ALNBOTTOMRIGHT = (4);

        /* values of WFSPTRPRINTFORM.wOffsetX and
                     WFSPTRPRINTFORM.wOffsetY */

        public const int WFS_PTR_OFFSETUSEFORMDEFN = (0xffff);

        /* values of WFSPTRRAWDATA.wInputData */

        public const int WFS_PTR_NOINPUTDATA = (0);
        public const int WFS_PTR_INPUTDATA = (1);

        /* values of WFSPTRIMAGE.wStatus */

        public const int WFS_PTR_DATAOK = (0);
        public const int WFS_PTR_DATASRCNOTSUPP = (1);
        public const int WFS_PTR_DATASRCMISSING = (2);

        /* values of WFSPTRBINSTATUS.wRetractBin */

        public const int WFS_PTR_RETRACTBININSERTED = (1);
        public const int WFS_PTR_RETRACTBINREMOVED = (2);

        /* values of WFSPTRDEFINITIONLOADED.dwDefinitionType */

        public const int WFS_PTR_FORMLOADED = (0x00000001);
        public const int WFS_PTR_MEDIALOADED = (0x00000002);

        /* values of WFSPTRSUPPLYREPLEN.fwSupplyReplen */

        public const int WFS_PTR_REPLEN_PAPERUPPER = (0x0001);
        public const int WFS_PTR_REPLEN_PAPERLOWER = (0x0002);
        public const int WFS_PTR_REPLEN_PAPERAUX = (0x0004);
        public const int WFS_PTR_REPLEN_PAPERAUX2 = (0x0008);
        public const int WFS_PTR_REPLEN_TONER = (0x0010);
        public const int WFS_PTR_REPLEN_INK = (0x0020);
        public const int WFS_PTR_REPLEN_LAMP = (0x0040);

        /* values of WFSPTRMEDIAREJECTED.wMediaRejected */

        public const int WFS_PTR_REJECT_SHORT = (0);
        public const int WFS_PTR_REJECT_LONG = (1);
        public const int WFS_PTR_REJECT_MULTIPLE = (2);
        public const int WFS_PTR_REJECT_ALIGN = (3);
        public const int WFS_PTR_REJECT_MOVETOALIGN = (4);
        public const int WFS_PTR_REJECT_SHUTTER = (5);
        public const int WFS_PTR_REJECT_ESCROW = (6);
        public const int WFS_PTR_REJECT_THICK = (7);
        public const int WFS_PTR_REJECT_OTHER = (8);

        /* values of WFSPTRMEDIARETRACTED.wRetractResult */

        public const int WFS_PTR_AUTO_RETRACT_OK = (0);
        public const int WFS_PTR_AUTO_RETRACT_MEDIAJAMMED = (1);

        /* XFS PTR Errors */

        public const int WFS_ERR_PTR_FORMNOTFOUND = (-(PTR_SERVICE_OFFSET + 0));
        public const int WFS_ERR_PTR_FIELDNOTFOUND = (-(PTR_SERVICE_OFFSET + 1));
        public const int WFS_ERR_PTR_NOMEDIAPRESENT = (-(PTR_SERVICE_OFFSET + 2));
        public const int WFS_ERR_PTR_READNOTSUPPORTED = (-(PTR_SERVICE_OFFSET + 3));
        public const int WFS_ERR_PTR_FLUSHFAIL = (-(PTR_SERVICE_OFFSET + 4));
        public const int WFS_ERR_PTR_MEDIAOVERFLOW = (-(PTR_SERVICE_OFFSET + 5));
        public const int WFS_ERR_PTR_FIELDSPECFAILURE = (-(PTR_SERVICE_OFFSET + 6));
        public const int WFS_ERR_PTR_FIELDERROR = (-(PTR_SERVICE_OFFSET + 7));
        public const int WFS_ERR_PTR_MEDIANOTFOUND = (-(PTR_SERVICE_OFFSET + 8));
        public const int WFS_ERR_PTR_EXTENTNOTSUPPORTED = (-(PTR_SERVICE_OFFSET + 9));
        public const int WFS_ERR_PTR_MEDIAINVALID = (-(PTR_SERVICE_OFFSET + 10));
        public const int WFS_ERR_PTR_FORMINVALID = (-(PTR_SERVICE_OFFSET + 11));
        public const int WFS_ERR_PTR_FIELDINVALID = (-(PTR_SERVICE_OFFSET + 12));
        public const int WFS_ERR_PTR_MEDIASKEWED = (-(PTR_SERVICE_OFFSET + 13));
        public const int WFS_ERR_PTR_RETRACTBINFULL = (-(PTR_SERVICE_OFFSET + 14));
        public const int WFS_ERR_PTR_STACKERFULL = (-(PTR_SERVICE_OFFSET + 15));
        public const int WFS_ERR_PTR_PAGETURNFAIL = (-(PTR_SERVICE_OFFSET + 16));
        public const int WFS_ERR_PTR_MEDIATURNFAIL = (-(PTR_SERVICE_OFFSET + 17));
        public const int WFS_ERR_PTR_SHUTTERFAIL = (-(PTR_SERVICE_OFFSET + 18));
        public const int WFS_ERR_PTR_MEDIAJAMMED = (-(PTR_SERVICE_OFFSET + 19));
        public const int WFS_ERR_PTR_FILE_IO_ERROR = (-(PTR_SERVICE_OFFSET + 20));
        public const int WFS_ERR_PTR_CHARSETDATA = (-(PTR_SERVICE_OFFSET + 21));
        public const int WFS_ERR_PTR_PAPERJAMMED = (-(PTR_SERVICE_OFFSET + 22));
        public const int WFS_ERR_PTR_PAPEROUT = (-(PTR_SERVICE_OFFSET + 23));
        public const int WFS_ERR_PTR_INKOUT = (-(PTR_SERVICE_OFFSET + 24));
        public const int WFS_ERR_PTR_TONEROUT = (-(PTR_SERVICE_OFFSET + 25));
        public const int WFS_ERR_PTR_LAMPINOP = (-(PTR_SERVICE_OFFSET + 26));
        public const int WFS_ERR_PTR_SOURCEINVALID = (-(PTR_SERVICE_OFFSET + 27));
        public const int WFS_ERR_PTR_SEQUENCEINVALID = (-(PTR_SERVICE_OFFSET + 28));
        public const int WFS_ERR_PTR_MEDIASIZE = (-(PTR_SERVICE_OFFSET + 29));
        public const int WFS_ERR_PTR_INVALID_PORT = (-(PTR_SERVICE_OFFSET + 30));
        public const int WFS_ERR_PTR_MEDIARETAINED = (-(PTR_SERVICE_OFFSET + 31));
        public const int WFS_ERR_PTR_BLACKMARK = (-(PTR_SERVICE_OFFSET + 32));
        public const int WFS_ERR_PTR_DEFINITIONEXISTS = (-(PTR_SERVICE_OFFSET + 33));
        public const int WFS_ERR_PTR_MEDIAREJECTED = (-(PTR_SERVICE_OFFSET + 34));
        public const int WFS_ERR_PTR_MEDIARETRACTED = (-(PTR_SERVICE_OFFSET + 35));
        public const int WFS_ERR_PTR_MSFERROR = (-(PTR_SERVICE_OFFSET + 36));
        public const int WFS_ERR_PTR_NOMSF = (-(PTR_SERVICE_OFFSET + 37));
        public const int WFS_ERR_PTR_FILENOTFOUND = (-(PTR_SERVICE_OFFSET + 38));
        public const int WFS_ERR_PTR_POWERSAVETOOSHORT = (-(PTR_SERVICE_OFFSET + 39));
        public const int WFS_ERR_PTR_POWERSAVEMEDIAPRESENT = (-(PTR_SERVICE_OFFSET + 40));
        public const int WFS_ERR_PTR_PASSBOOKCLOSED = (-(PTR_SERVICE_OFFSET + 41));
        public const int WFS_ERR_PTR_LASTORFIRSTPAGEREACHED = (-(PTR_SERVICE_OFFSET + 42));

        ///*=================================================================*/
        ///* PTR Info Command Structures */
        ///*=================================================================*/

        //typedef struct _wfs_ptr_retract_bins
        //{
        //    WORD                 wRetractBin;
        //    USHORT               usRetractCount;
        //} WFSPTRRETRACTBINS, *LPWFSPTRRETRACTBINS;

        //typedef struct _wfs_ptr_status
        //{
        //    WORD                 fwDevice;
        //    WORD                 fwMedia;
        //    WORD                 fwPaper[WFS_PTR_SUPPLYSIZE];
        //    WORD                 fwToner;
        //    WORD                 fwInk;
        //    WORD                 fwLamp;
        //    LPWFSPTRRETRACTBINS *lppRetractBins;
        //    USHORT               usMediaOnStacker;
        //    LPSTR                lpszExtra;
        //    DWORD                dwGuidLights[WFS_PTR_GUIDLIGHTS_SIZE];
        //    WORD                 wDevicePosition;
        //    USHORT               usPowerSaveRecoveryTime;
        //    WORD                 wPaperType[WFS_PTR_SUPPLYSIZE];
        //    WORD                 wAntiFraudModule;
        //} WFSPTRSTATUS, *LPWFSPTRSTATUS;

        //typedef struct _wfs_ptr_caps
        //{
        //    WORD                 wClass;
        //    WORD                 fwType;
        //    BOOL                 bCompound;
        //    WORD                 wResolution;
        //    WORD                 fwReadForm;
        //    WORD                 fwWriteForm;
        //    WORD                 fwExtents;
        //    WORD                 fwControl;
        //    USHORT               usMaxMediaOnStacker;
        //    BOOL                 bAcceptMedia;
        //    BOOL                 bMultiPage;
        //    WORD                 fwPaperSources;
        //    BOOL                 bMediaTaken;
        //    USHORT               usRetractBins;
        //    LPUSHORT             lpusMaxRetract;
        //    WORD                 fwImageType;
        //    WORD                 fwFrontImageColorFormat;
        //    WORD                 fwBackImageColorFormat;
        //    WORD                 fwCodelineFormat;
        //    WORD                 fwImageSource;
        //    WORD                 fwCharSupport;
        //    BOOL                 bDispensePaper;
        //    LPSTR                lpszExtra;
        //    DWORD                dwGuidLights[WFS_PTR_GUIDLIGHTS_SIZE];
        //    LPSTR                lpszWindowsPrinter;
        //    BOOL                 bMediaPresented;
        //    USHORT               usAutoRetractPeriod;
        //    BOOL                 bRetractToTransport;
        //    BOOL                 bPowerSaveControl;
        //    WORD                 fwCoercivityType;
        //    WORD                 fwControlPassbook;
        //    WORD                 wPrintSides;
        //    BOOL                 bAntiFraudModule; } WFSPTRCAPS, *LPWFSPTRCAPS;

        //typedef struct _wfs_frm_header
        //{
        //    LPSTR                lpszFormName;
        //    WORD                 wBase;
        //    WORD                 wUnitX;
        //    WORD                 wUnitY;
        //    WORD                 wWidth;
        //    WORD                 wHeight;
        //    WORD                 wAlignment;
        //    WORD                 wOrientation;
        //    WORD                 wOffsetX;
        //    WORD                 wOffsetY;
        //    WORD                 wVersionMajor;
        //    WORD                 wVersionMinor;
        //    LPSTR                lpszUserPrompt;
        //    WORD                 fwCharSupport;
        //    LPSTR                lpszFields;
        //    WORD                 wLanguageID;
        //} WFSFRMHEADER, *LPWFSFRMHEADER;

        //typedef struct _wfs_frm_media
        //{
        //    WORD                 fwMediaType;
        //    WORD                 wBase;
        //    WORD                 wUnitX;
        //    WORD                 wUnitY;
        //    WORD                 wSizeWidth;
        //    WORD                 wSizeHeight;
        //    WORD                 wPageCount;
        //    WORD                 wLineCount;
        //    WORD                 wPrintAreaX;
        //    WORD                 wPrintAreaY;
        //    WORD                 wPrintAreaWidth;
        //    WORD                 wPrintAreaHeight;
        //    WORD                 wRestrictedAreaX;
        //    WORD                 wRestrictedAreaY;
        //    WORD                 wRestrictedAreaWidth;
        //    WORD                 wRestrictedAreaHeight;
        //    WORD                 wStagger;
        //    WORD                 wFoldType;
        //    WORD                 wPaperSources;
        //} WFSFRMMEDIA, *LPWFSFRMMEDIA;

        //typedef struct _wfs_ptr_query_field
        //{
        //    LPSTR                lpszFormName;
        //    LPSTR                lpszFieldName;
        //} WFSPTRQUERYFIELD, *LPWFSPTRQUERYFIELD;

        //typedef struct _wfs_frm_field
        //{
        //    LPSTR                lpszFieldName;
        //    WORD                 wIndexCount;
        //    WORD                 fwType;
        //    WORD                 fwClass;
        //    WORD                 fwAccess;
        //    WORD                 fwOverflow;
        //    LPSTR                lpszInitialValue;
        //    LPWSTR               lpszUNICODEInitialValue;
        //    LPSTR                lpszFormat;
        //    LPWSTR               lpszUNICODEFormat;
        //    WORD                 wLanguageID;
        //    WORD                 wCoercivity;
        //} WFSFRMFIELD, *LPWFSFRMFIELD;

        //typedef struct _wfs_ptr_hex_data
        //{
        //    USHORT               usLength;
        //    LPBYTE               lpbData;
        //} WFSPTRXDATA, *LPWFSPTRXDATA;

        ///* WFS_INF_PTR_CODELINE_MAPPING input and output structures */

        //typedef struct _wfs_ptr_codeline_mapping
        //{
        //    WORD                 wCodelineFormat;
        //}WFSPTRCODELINEMAPPING, *LPWFSPTRCODELINEMAPPING;

        //typedef struct _wfs_ptr_codeline_mapping_out
        //{
        //    WORD                 wCodelineFormat;
        //    LPWFSPTRXDATA        lpxCharMapping;
        //} WFSPTRCODELINEMAPPINGOUT, *LPWFSPTRCODELINEMAPPINGOUT;

        ///*=================================================================*/
        ///* PTR Execute Command Structures */
        ///*=================================================================*/

        //typedef struct _wfs_ptr_print_form
        //{
        //    LPSTR                lpszFormName;
        //    LPSTR                lpszMediaName;
        //    WORD                 wAlignment;
        //    WORD                 wOffsetX;
        //    WORD                 wOffsetY;
        //    WORD                 wResolution;
        //    DWORD                dwMediaControl;
        //    LPSTR                lpszFields;
        //    LPWSTR               lpszUNICODEFields;
        //    WORD                 wPaperSource;
        //} WFSPTRPRINTFORM, *LPWFSPTRPRINTFORM;

        //typedef struct _wfs_ptr_read_form
        //{
        //    LPSTR                lpszFormName;
        //    LPSTR                lpszFieldNames;
        //    LPSTR                lpszMediaName;
        //    DWORD                dwMediaControl;
        //} WFSPTRREADFORM, *LPWFSPTRREADFORM;

        //typedef struct _wfs_ptr_read_form_out
        //{
        //    LPSTR                lpszFields;
        //    LPWSTR               lpszUNICODEFields;
        //} WFSPTRREADFORMOUT, *LPWFSPTRREADFORMOUT;

        //typedef struct _wfs_ptr_raw_data
        //{
        //    WORD                 wInputData;
        //    ULONG                ulSize;
        //    LPBYTE               lpbData;
        //} WFSPTRRAWDATA, *LPWFSPTRRAWDATA;

        //typedef struct _wfs_ptr_raw_data_in
        //{
        //    ULONG                ulSize;
        //    LPBYTE               lpbData;
        //} WFSPTRRAWDATAIN, *LPWFSPTRRAWDATAIN;

        //typedef struct _wfs_ptr_media_unit
        //{
        //    WORD                 wBase;
        //    WORD                 wUnitX;
        //    WORD                 wUnitY;
        //} WFSPTRMEDIAUNIT, *LPWFSPTRMEDIAUNIT;

        //typedef struct _wfs_ptr_media_ext
        //{
        //    ULONG                ulSizeX;
        //    ULONG                ulSizeY;
        //} WFSPTRMEDIAEXT, *LPWFSPTRMEDIAEXT;

        //typedef struct _wfs_ptr_image_request
        //{
        //    WORD                 wFrontImageType;
        //    WORD                 wBackImageType;
        //    WORD                 wFrontImageColorFormat;
        //    WORD                 wBackImageColorFormat;
        //    WORD                 wCodelineFormat;
        //    WORD                 fwImageSource;
        //    LPSTR                lpszFrontImageFile;
        //    LPSTR                lpszBackImageFile;
        //} WFSPTRIMAGEREQUEST, *LPWFSPTRIMAGEREQUEST;

        //typedef struct _wfs_ptr_image
        //{
        //    WORD                 wImageSource;
        //    WORD                 wStatus;
        //    ULONG                ulDataLength;
        //    LPBYTE               lpbData;
        //} WFSPTRIMAGE, *LPWFSPTRIMAGE;

        //typedef struct _wfs_ptr_reset
        //{
        //    DWORD                dwMediaControl;
        //    USHORT               usRetractBinNumber;
        //} WFSPTRRESET, *LPWFSPTRRESET;

        //typedef struct _wfs_ptr_set_guidlight
        //{
        //    WORD                 wGuidLight;
        //    DWORD                dwCommand;
        //} WFSPTRSETGUIDLIGHT, *LPWFSPTRSETGUIDLIGHT;

        //typedef struct _wfs_ptr_print_raw_file
        //{
        //    LPSTR                lpszFileName;
        //    DWORD                dwMediaControl;
        //    DWORD                dwPaperSource;
        //} WFSPTRPRINTRAWFILE, *LPWFSPTRPRINTRAWFILE;

        //typedef struct _wfs_ptr_load_definition
        //{
        //    LPSTR                lpszFileName;
        //    BOOL                 bOverwrite;
        //} WFSPTRLOADDEFINITION, *LPWFSPTRLOADDEFINITION;

        //typedef struct _wfs_ptr_supply_replen
        //{
        //    WORD                 fwSupplyReplen;
        //} WFSPTRSUPPLYREPLEN, *LPWFSPTRSUPPLYREPLEN;

        //typedef struct _wfs_ptr_power_save_control
        //{
        //    USHORT               usMaxPowerSaveRecoveryTime;
        //} WFSPTRPOWERSAVECONTROL, *LPWFSPTRPOWERSAVECONTROL;

        //typedef struct _wfs_ptr_control_passbook
        //{
        //    WORD                 wAction;
        //    USHORT               usCount;
        //} WFSPTRCONTROLPASSBOOK, *LPWFSPTRCONTROLPASSBOOK;

        ///*=================================================================*/
        ///* PTR Message Structures */
        ///*=================================================================*/

        //typedef struct _wfs_ptr_field_failure
        //{
        //    LPSTR                lpszFormName;
        //    LPSTR                lpszFieldName;
        //    WORD                 wFailure;
        //} WFSPTRFIELDFAIL, *LPWFSPTRFIELDFAIL;

        //typedef struct _wfs_ptr_bin_threshold
        //{
        //    USHORT               usBinNumber;
        //    WORD                 wRetractBin;
        //} WFSPTRBINTHRESHOLD, *LPWFSPTRBINTHRESHOLD;

        //typedef struct _wfs_ptr_paper_threshold
        //{
        //     WORD                wPaperSource;
        //     WORD                wPaperThreshold;
        //} WFSPTRPAPERTHRESHOLD, *LPWFSPTRPAPERTHRESHOLD;

        //typedef struct _wfs_ptr_media_detected
        //{
        //    WORD                 wPosition;
        //    USHORT               usRetractBinNumber;
        //} WFSPTRMEDIADETECTED, *LPWFSPTRMEDIADETECTED;

        //typedef struct _wfs_ptr_bin_status
        //{
        //    USHORT               usBinNumber;
        //    WORD                 wRetractBin;
        //} WFSPTRBINSTATUS, *LPWFSPTRBINSTATUS;

        //typedef struct _wfs_ptr_media_presented
        //{
        //    USHORT               usWadIndex;
        //    USHORT               usTotalWads;
        //} WFSPTRMEDIAPRESENTED, *LPWFSPTRMEDIAPRESENTED;

        //typedef struct _wfs_ptr_definition_loaded
        //{
        //    LPSTR                lpszDefinitionName;
        //    DWORD                dwDefinitionType;
        //} WFSPTRDEFINITIONLOADED, *LPWFSPTRDEFINITIONLOADED;

        //typedef struct _wfs_ptr_media_rejected
        //{
        //    WORD                 wMediaRejected;
        //} WFSPTRMEDIAREJECTED, *LPWFSPTRMEDIAREJECTED;

        //typedef struct _wfs_ptr_media_retracted
        //{
        //    WORD                 wRetractResult;
        //    USHORT               usBinNumber;
        //} WFSPTRMEDIARETRACTED, *LPWFSPTRMEDIARETRACTED;

        //typedef struct _wfs_ptr_device_position
        //{
        //    WORD                 wPosition;
        //} WFSPTRDEVICEPOSITION, *LPWFSPTRDEVICEPOSITION;

        //typedef struct _wfs_ptr_power_save_change
        //{
        //    USHORT               usPowerSaveRecoveryTime;
        //} WFSPTRPOWERSAVECHANGE, *LPWFSPTRPOWERSAVECHANGE;

    }//class XFSPTRh
}//namespace 

///* restore alignment */
//#pragma pack(pop)

//#ifdef __cplusplus
//} /*extern "C"*/
//#endif
//#endif /* __INC_XFSPTR__H */
