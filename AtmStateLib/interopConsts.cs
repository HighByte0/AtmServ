#if __LINE__
#pragma once
#else
//using System.Threading.Tasks;
#endif



//sizeof interop st fields
namespace iopcts
{
    class wfs_sizof
    {
        public const int hresult = 4;
        public const int ObjsCount = 4;
    };//cdm
    class cdmsizof
    {
        public const int fwDevice = 2;
        public const int fwSafeDoor = 2;
        public const int fwDispenser = 2;
        public const int fwIntermediateStacker = 2;
        public const int positionsCount = 4;
        public const int lpszExtraLength = 4;
        public const int dwGuidLights = 128;
        public const int wDevicePosition = 2;
        public const int usPowerSaveRecoveryTime = 2;
        public const int wAntiFraudModule = 2;
        public const int GUIDLIGHTS_SIZE = 32;
    };//cdmsizof
    class cdmpossz
    {
        public const int fwPosition = 2;
        public const int fwShutter = 2;
        public const int fwPositionStatus = 2;
        public const int fwTransport = 2;
        public const int fwTransportStatus = 2;
    };//cdmpossz
    class siu_sizof
    {
        public const int fwDevice = 2;
        public const int fwSensors = 64;
        public const int fwDoors = 32;
        public const int fwIndicators = 32;
        public const int fwAuxiliaries = 32;
        public const int fwGuidLights = 32;
        public const int szofExtraLen = 2;
        public const int usPSRecovryTime = 2;
        public const int wAntiFraudModul = 2;
        public const int SENSORS_SIZE = (32);
        public const int DOORS_SIZE = (16);
        public const int INDICATORS_SIZE = (16);
        public const int AUXILIARIES_SIZE = (16);
        public const int GUIDLIGHTS_SIZE = (16);
    };//siu_sizof
    class cdmphcu
    {
        public const int lpPhysicalPositionName = 4;
        public const int cUnitID = 5;
        public const int ulInitialCount = 4;
        public const int ulCount = 4;
        public const int ulRejectCount = 4;
        public const int ulMaximum = 4;
        public const int usPStatus = 2;
        public const int bHardwareSensor = 4;
        public const int ulDispensedCount = 4;
        public const int ulPresentedCount = 4;
        public const int ulRetractedCount = 4;
    };//cdmphcu
#if __LINE__
#else
    public
#endif
    class cdmcashunit
    {
        public const int usNumber = 2;
        public const int usType = 2;
        public const int lpszCashUnitName = 4;
        public const int cUnitID = 5;
        public const int cCurrencyID = 3;
        public const int ulValues = 4;
        public const int ulInitialCount = 4;
        public const int ulCount = 4;
        public const int ulRejectCount = 4;
        public const int ulMinimum = 4;
        public const int ulMaximum = 4;
        public const int bAppLock = 4;
        public const int usStatus = 2;
        public const int usNumPhysicalCUs = 2;
        public const int lppPhysical = 4;
        public const int ulDispensedCount = 4;
        public const int ulPresentedCount = 4;
        public const int ulRetractedCount = 4;
    };//cdmcashunit
    class cdm_cu_info
    {
        public const int usTellerID = 2;
        public const int usCount = 2;
    };//cdm_cu_info
    class idc_sizof
    {
        public const int fwDevice = 2;
        public const int fwMedia = 2;
        public const int fwRetainBin = 2;
        public const int fwSecurity = 2;
        public const int usCards = 2;
        public const int fwChipPower = 2;
        public const int lpszExtra = 4;
        public const int dwGuidLights = 128;
        public const int fwChipModule = 2;
        public const int fwMagReadModule = 2;
        public const int fwMagWriteModule = 2;
        public const int fwFrontImageModule = 2;
        public const int fwBackImageModule = 2;
        public const int wDevicePosition = 2;
        public const int usPowerSaveRecoveryTime = 2;
        public const int wParkingStationMedia = 2;
        public const int wAntiFraudModule = 2;
        public const int GUIDLIGHTS_SIZE = 32;
    };//idc_sizof
    class pin_sizof
    {
        public const int fwDevice = 2;
        public const int fwEncStat = 2;
        //public const int lpszExtra = 4;
        public const int dwGuidLights = 128;
        public const int fwAutoBeepMode = 2;
        public const int dwCertificateState = 4;
        public const int wDevicePosition = 2;
        public const int usPowerSaveRecoveryTime = 2;
        public const int wAntiFraudModule = 2;
        public const int GUIDLIGHTS_SIZE = 32;
    };//pin_sizof
    class ptrretractbins_sizof
    {
        public const int wRetractBin = 2;
        public const int usRetractCount = 2;
    };//ptrretractbins_sizof
    class ptr_sizof
    {
        public const int fwDevice = 2;
        public const int fwMedia = 2;
        public const int fwPaper = 32;
        public const int fwToner = 2;
        public const int fwInk = 2;
        public const int fwLamp = 2;
        //public const int lppRetractBins = 4;
        public const int usMediaOnStacker = 2;
        //public const int lpszExtra = 4;
        public const int dwGuidLights = 128;
        public const int wDevicePosition = 2;
        public const int usPowerSaveRecoveryTime = 2;
        public const int wPaperType = 32;
        public const int wAntiFraudModule = 2;
        public const int SUPPLYSIZE = 16;
        public const int GUIDLIGHTS_SIZE = 32;
    };//ptr_sizof
    class ttuledex_sizof
    {
        public const int usNumOfLEDs = 2;
        public const int lpdwLEDs = 4;
    };//ttuledex_sizof
    class ttu_sizof
    {
        public const int fwDevice = 2;
        public const int wKeyboard = 2;
        public const int wKeylock = 2;
        public const int wLEDs = 16;
        public const int wDisplaySizeX = 2;
        public const int wDisplaySizeY = 2;
        public const int lpszExtra = 4;
        public const int wDevicePosition = 2;
        public const int usPowerSaveRecoveryTime = 2;
        public const int lpLEDEx = 4;
        public const int wAntiFraudModule = 2;
        public const int LEDS_MAX = 8;
    };//ttu_sizof
}//namespace iopcts
