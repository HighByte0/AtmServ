﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AtmStateLib
{

    //public class AtmState2
    //{
    //    public static int ReadWFSStateData2(ref WFSATM state, byte[] state_bytes)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    public class AtmState
    {
        public static readonly uint CDMFLAG = (0x1 << 0);
        public static readonly uint IDCFLAG = (0x1 << 1);
        public static readonly uint JRNFLAG = (0x1 << 2);
        public static readonly uint PINFLAG = (0x1 << 3);
        public static readonly uint RCPFLAG = (0x1 << 4);
        public static readonly uint SIUFLAG = (0x1 << 5);
        public static readonly uint TTUFLAG = (0x1 << 6);
        public static readonly uint CASHFLAG = (0x1 << 7);
        public static readonly uint CASHUSERVFLAG = (0x1 << 8);

        public static int ReadWFSStateData(ref WFSATM state, IntPtr state_ptr, int buff_size, out uint _wfsComponentsincluded)
        {
            int offset = 0;
            _wfsComponentsincluded = (UInt32)Marshal.ReadInt32(state_ptr, offset);
            offset += Marshal.SizeOf(_wfsComponentsincluded);
            //cdm
            state.cdm.status = Marshal.ReadInt32(state_ptr, offset);
            offset += iopcts.wfs_sizof.hresult;
            if (state.cdm.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                if (state.cdm.dwGuidLights == null)
                {
                    state.cdm.dwGuidLights = new Int32[iopcts.cdmsizof.GUIDLIGHTS_SIZE];
                }
                state.cdm.fwDevice = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.cdmsizof.fwDevice;
                state.cdm.fwSafeDoor = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.fwSafeDoor;
                state.cdm.fwDispenserstate = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.fwDispenser;
                state.cdm.fwintermediateStacker = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.fwIntermediateStacker;
                int state_cdm_pos_count = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.positionsCount;
                int state_cdm_extra_len = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.lpszExtraLength;
                FillInt32Array(state.cdm.dwGuidLights, ptr_at_offset(state_ptr, offset));
                offset += iopcts.cdmsizof.dwGuidLights;
                state.cdm.wDevicePosition = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.wDevicePosition;
                state.cdm.usPowerSaveRecoveryTime = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.usPowerSaveRecoveryTime;
                state.cdm.wAntiFraudModule = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmsizof.wAntiFraudModule;
                state.cdm.positions = new CDMPOS[state_cdm_pos_count];
                for (int i = 0; i < state_cdm_pos_count; i++)
                {
                    state.cdm.positions[i].fwPosition = Marshal.ReadInt16(state_ptr, offset);
                    offset += iopcts.cdmpossz.fwPosition;
                    state.cdm.positions[i].fwPositionStatus = Marshal.ReadInt16(state_ptr, offset);
                    offset += iopcts.cdmpossz.fwPositionStatus;
                    state.cdm.positions[i].fwShutter = Marshal.ReadInt16(state_ptr, offset);
                    offset += iopcts.cdmpossz.fwShutter;
                    state.cdm.positions[i].fwTransport = Marshal.ReadInt16(state_ptr, offset);
                    offset += iopcts.cdmpossz.fwTransport;
                    state.cdm.positions[i].fwTransportStatus = Marshal.ReadInt16(state_ptr, offset);
                    offset += iopcts.cdmpossz.fwTransportStatus;
                    //Marshal.PtrToStructure<CDMPOS>(state_ptr + offset, state.cdm.positions[i]);
                }
                //StringBuilder ss;ss.
                long cdm_extra_ptr = state_ptr.ToInt64() + offset;
                state.cdm.szExtra = Marshal.PtrToStringAnsi((IntPtr)cdm_extra_ptr, state_cdm_extra_len);
                offset += state_cdm_extra_len;
            }//WFS_SUCCESS
            //Console.WriteLine("CDM READ OFFSET " + offset);
            //cashunits begin
            if ((AtmState.CASHUSERVFLAG & _wfsComponentsincluded) == AtmState.CASHUSERVFLAG)
            {
            state.cashUnits.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (state.cashUnits.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                state.cashUnits.usTellerID = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += (iopcts.cdm_cu_info.usTellerID);
                state.cashUnits.usCount = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += (iopcts.cdm_cu_info.usCount);
                state.cashUnits.lppList = new WFSCDMCASHUNIT[state.cashUnits.usCount];
                for (int i = 0; i < state.cashUnits.usCount; i++)
                {
                    ref WFSCDMCASHUNIT cashunit_i = ref state.cashUnits.lppList[i];
                    cashunit_i.bAppLock = Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.bAppLock);

                    cashunit_i.cCurrencyID = ReadAndConvertBytes(state_ptr, offset, iopcts.cdmcashunit.cCurrencyID);
                    offset += (iopcts.cdmcashunit.cCurrencyID);

                    byte[] bytes02 = new byte[iopcts.cdmcashunit.cUnitID];

                    cashunit_i.cUnitID = ReadAndConvertBytes(state_ptr, offset, iopcts.cdmcashunit.cUnitID);
                    offset += (iopcts.cdmcashunit.cUnitID);

                    cashunit_i.lpszCashUnitName = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                    offset += cashunit_i.lpszCashUnitName.Length + 1;

                    cashunit_i.ulCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulCount);
                    cashunit_i.ulDispensedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulDispensedCount);
                    cashunit_i.ulInitialCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulInitialCount);
                    cashunit_i.ulMaximum = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulMaximum);
                    cashunit_i.ulMinimum = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulMaximum);
                    cashunit_i.ulPresentedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulPresentedCount);
                    cashunit_i.ulRejectCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulRejectCount);
                    cashunit_i.ulRetractedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulRetractedCount);
                    cashunit_i.ulValues = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulValues);
                    cashunit_i.usNumber = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usNumber;
                    cashunit_i.usNumPhysicalCUs = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usNumPhysicalCUs;
                    cashunit_i.lppPhysical = new WFSCDMPHCU[cashunit_i.usNumPhysicalCUs];
                    for (int j = 0; j < cashunit_i.usNumPhysicalCUs; j++)
                    {
                        ref WFSCDMPHCU cashunit_ij = ref cashunit_i.lppPhysical[j];
                        cashunit_ij.bHardwareSensor = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.bHardwareSensor;
                        cashunit_ij.cUnitID = ReadAndConvertBytes(state_ptr, offset, iopcts.cdmphcu.cUnitID);
                        offset += (iopcts.cdmphcu.cUnitID);
                        cashunit_ij.lpPhysicalPositionName = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                        offset += cashunit_ij.lpPhysicalPositionName.Length + 1;
                        cashunit_ij.ulCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulCount;
                        cashunit_ij.ulDispensedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulDispensedCount;
                        cashunit_ij.ulInitialCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulInitialCount;
                        cashunit_ij.ulMaximum = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulMaximum;
                        cashunit_ij.ulPresentedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulPresentedCount;
                        cashunit_ij.ulRejectCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulRejectCount;
                        cashunit_ij.ulRetractedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulRetractedCount;
                        cashunit_ij.usPStatus = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmphcu.usPStatus;
                    }
                    cashunit_i.usStatus = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usStatus;
                    cashunit_i.usType = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usType;
                }
            }//WFS_SUCCESS
            }
            //cashunits end
            //Console.WriteLine("CDM-CUs READ OFFSET " + offset);
            //IDC begin
            state.idc.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (state.idc.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                if (state.idc.dwGuidLights == null)
                {
                    state.idc.dwGuidLights = new Int32[iopcts.idc_sizof.GUIDLIGHTS_SIZE];
                }
                state.idc.fwDevice = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwDevice;
                state.idc.fwMedia = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwMedia;
                state.idc.fwRetainBin = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwRetainBin;
                state.idc.fwSecurity = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwSecurity;
                state.idc.usCards = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.usCards;
                state.idc.fwChipPower = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwChipPower;
                FillInt32Array(state.idc.dwGuidLights, ptr_at_offset(state_ptr, offset));
                offset += iopcts.idc_sizof.dwGuidLights;
                state.idc.fwChipModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwChipModule;
                state.idc.fwMagReadModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwMagReadModule;
                state.idc.fwMagWriteModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwMagWriteModule;
                state.idc.fwFrontImageModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwFrontImageModule;
                state.idc.fwBackImageModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.fwBackImageModule;
                state.idc.wDevicePosition = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.wDevicePosition;
                state.idc.usPowerSaveRecoveryTime = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.usPowerSaveRecoveryTime;
                state.idc.wAntiFraudModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.idc_sizof.wAntiFraudModule;
                state.idc.szExtra = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                offset += state.idc.szExtra.Length + 1;
                while (true)
                {
                    Int16 psm_i = Marshal.ReadInt16(state_ptr, offset);
                    offset += (int)iopcts.idc_sizof.wParkingStationMedia;
                    if (0 == psm_i) { break; }
                }
            }//WFS_SUCCESS
            //idc end
            //Console.WriteLine("IDC READ OFFSET " + offset);
            //jrn begin
            state.jrn.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (state.jrn.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                //if (state.jrn.dwGuidLights) state.jrn.dwGuidLights;
                state.jrn.dwGuidLights = ReadInt32Array(ptr_at_offset(state_ptr, offset), iopcts.ptr_sizof.GUIDLIGHTS_SIZE);
                offset += iopcts.ptr_sizof.dwGuidLights;
                state.jrn.fwDevice = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwDevice;
                state.jrn.fwInk = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwInk;
                state.jrn.fwLamp = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwLamp;
                state.jrn.fwMedia = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwMedia;
                state.jrn.fwPaper = ReadInt16Array(ptr_at_offset(state_ptr, offset), iopcts.ptr_sizof.SUPPLYSIZE);
                offset += iopcts.ptr_sizof.fwPaper;
                state.jrn.fwToner = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwToner;
                state.jrn.usMediaOnStacker = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.usMediaOnStacker;
                state.jrn.usPowerSaveRecoveryTime = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.usPowerSaveRecoveryTime;
                state.jrn.wAntiFraudModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.wAntiFraudModule;
                state.jrn.wDevicePosition = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.wDevicePosition;
                state.jrn.wPaperType = ReadInt16Array(ptr_at_offset(state_ptr, offset), iopcts.ptr_sizof.SUPPLYSIZE);
                offset += iopcts.ptr_sizof.wPaperType;
                state.jrn.szExtra = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                offset += state.jrn.szExtra.Length + 1;
                int jrn_retractbinsNb = Marshal.ReadInt32(state_ptr, offset);
                offset += iopcts.wfs_sizof.ObjsCount;
                if (jrn_retractbinsNb > 0)
                {
                    state.jrn.RetractBins = new WFSPTRRETRACTBINS[jrn_retractbinsNb];
                    for (int i = 0; i < jrn_retractbinsNb; i++)
                    {
                        state.jrn.RetractBins[i] = new WFSPTRRETRACTBINS();
                        state.jrn.RetractBins[i].usRetractCount = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                        offset += iopcts.ptrretractbins_sizof.usRetractCount;
                        state.jrn.RetractBins[i].wRetractBin = Marshal.ReadInt16(state_ptr, offset);
                        offset += iopcts.ptrretractbins_sizof.wRetractBin;
                    }
                }
            }//WFS_SUCCESS
            //jrn end
            //Console.WriteLine("JRN READ OFFSET " + offset);
            //pin begin
            state.pin.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (state.pin.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                state.pin.dwCertificateState = Marshal.ReadInt32(state_ptr, offset);
                offset += iopcts.pin_sizof.dwCertificateState;
                state.pin.dwGuidLights = ReadInt32Array(ptr_at_offset(state_ptr, offset), iopcts.pin_sizof.GUIDLIGHTS_SIZE);
                offset += iopcts.pin_sizof.dwGuidLights;
                state.pin.fwAutoBeepMode = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.pin_sizof.fwAutoBeepMode;
                state.pin.fwDevice = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.pin_sizof.fwDevice;
                state.pin.fwEncStat = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.pin_sizof.fwEncStat;
                state.pin.usPowerSaveRecoveryTime = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.pin_sizof.usPowerSaveRecoveryTime;
                state.pin.wAntiFraudModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.pin_sizof.wAntiFraudModule;
                state.pin.wDevicePosition = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.pin_sizof.wDevicePosition;
                state.pin.szExtra = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                offset += state.pin.szExtra.Length + 1;
            }//WFS_SUCCESS
             //pin end
             //Console.WriteLine("PIN READ OFFSET " + offset);
             //rcp begin
            state.rcp.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (state.rcp.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                //if (state.rcp.dwGuidLights) state.rcp.dwGuidLights;
                state.rcp.dwGuidLights = ReadInt32Array(ptr_at_offset(state_ptr, offset), iopcts.ptr_sizof.GUIDLIGHTS_SIZE);
                offset += iopcts.ptr_sizof.dwGuidLights;
                state.rcp.fwDevice = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwDevice;
                state.rcp.fwInk = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwInk;
                state.rcp.fwLamp = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwLamp;
                state.rcp.fwMedia = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwMedia;
                state.rcp.fwPaper = ReadInt16Array(ptr_at_offset(state_ptr, offset), iopcts.ptr_sizof.SUPPLYSIZE);
                offset += iopcts.ptr_sizof.fwPaper;
                state.rcp.fwToner = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.fwToner;
                state.rcp.usMediaOnStacker = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.usMediaOnStacker;
                state.rcp.usPowerSaveRecoveryTime = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.usPowerSaveRecoveryTime;
                state.rcp.wAntiFraudModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.wAntiFraudModule;
                state.rcp.wDevicePosition = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ptr_sizof.wDevicePosition;
                state.rcp.wPaperType = ReadInt16Array(ptr_at_offset(state_ptr, offset), iopcts.ptr_sizof.SUPPLYSIZE);
                offset += iopcts.ptr_sizof.wPaperType;
                state.rcp.szExtra = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                offset += state.rcp.szExtra.Length + 1;
                int rcp_retractbinsNb = Marshal.ReadInt32(state_ptr, offset);
                offset += iopcts.wfs_sizof.ObjsCount;
                if (rcp_retractbinsNb > 0)
                {
                    state.rcp.RetractBins = new WFSPTRRETRACTBINS[rcp_retractbinsNb];
                    for (int i = 0; i < rcp_retractbinsNb; i++)
                    {
                        state.rcp.RetractBins[i] = new WFSPTRRETRACTBINS();
                        state.rcp.RetractBins[i].usRetractCount = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                        offset += iopcts.ptrretractbins_sizof.usRetractCount;
                        state.rcp.RetractBins[i].wRetractBin = Marshal.ReadInt16(state_ptr, offset);
                        offset += iopcts.ptrretractbins_sizof.wRetractBin;
                    }
                }
            }//WFS_SUCCESS
             //rcp end
             //Console.WriteLine("RCP READ OFFSET " + offset);
             //SIU BEGIN
            state.siu.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (state.siu.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                if (state.siu.fwSensors == null)
                {
                    state.siu.fwSensors = new Int16[iopcts.siu_sizof.SENSORS_SIZE];
                    state.siu.fwDoors = new Int16[iopcts.siu_sizof.DOORS_SIZE];
                    state.siu.fwIndicators = new Int16[iopcts.siu_sizof.INDICATORS_SIZE];
                    state.siu.fwAuxiliaries = new Int16[iopcts.siu_sizof.AUXILIARIES_SIZE];
                    state.siu.fwGuidLights = new Int16[iopcts.siu_sizof.GUIDLIGHTS_SIZE];
                }
                state.siu.fwDevice = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.siu_sizof.fwDevice;
                FillInt16Array(state.siu.fwSensors, ptr_at_offset(state_ptr, offset));
                offset += iopcts.siu_sizof.fwSensors;
                FillInt16Array(state.siu.fwDoors, ptr_at_offset(state_ptr, offset));
                offset += iopcts.siu_sizof.fwDoors;
                FillInt16Array(state.siu.fwIndicators, ptr_at_offset(state_ptr, offset));
                offset += iopcts.siu_sizof.fwIndicators;
                FillInt16Array(state.siu.fwAuxiliaries, ptr_at_offset(state_ptr, offset));
                offset += iopcts.siu_sizof.fwAuxiliaries;
                FillInt16Array(state.siu.fwGuidLights, ptr_at_offset(state_ptr, offset));
                offset += iopcts.siu_sizof.fwGuidLights;
                int siu_extralen = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.siu_sizof.szofExtraLen;
                state.siu.usPowerSaveRecoveryTime = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.siu_sizof.usPSRecovryTime;
                state.siu.wAntiFraudModule = Marshal.ReadInt16(state_ptr, offset); offset += iopcts.siu_sizof.wAntiFraudModul;
                long siu_extra_ptr = state_ptr.ToInt64() + offset;
                state.siu.szExtra = Marshal.PtrToStringAnsi((IntPtr)siu_extra_ptr, siu_extralen);
                offset += siu_extralen;
            }//WFS_SUCCESS
             //SIU END
             //Console.WriteLine("SIU READ OFFSET " + offset);
             //ttu begin
            state.ttu.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (state.ttu.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                state.ttu.fwDevice = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.fwDevice;
                state.ttu.usPowerSaveRecoveryTime = (UInt16)Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.usPowerSaveRecoveryTime;
                state.ttu.wAntiFraudModule = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.wAntiFraudModule;
                state.ttu.wDevicePosition = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.wDevicePosition;
                state.ttu.wDisplaySizeX = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.wDisplaySizeX;
                state.ttu.wDisplaySizeY = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.wDisplaySizeY;
                state.ttu.wKeyboard = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.wKeyboard;
                state.ttu.wKeylock = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttu_sizof.wKeylock;
                state.ttu.wLEDs = ReadInt16Array(ptr_at_offset(state_ptr, offset), iopcts.ttu_sizof.LEDS_MAX);
                offset += iopcts.ttu_sizof.wLEDs;
                int ttu_LEDEx_nb = Marshal.ReadInt16(state_ptr, offset);
                offset += iopcts.ttuledex_sizof.usNumOfLEDs;
                state.ttu.dwLEDsEx = ReadInt32Array(ptr_at_offset(state_ptr, offset), ttu_LEDEx_nb);
                offset += ttu_LEDEx_nb * iopcts.ttuledex_sizof.lpdwLEDs/*DWORD*/;
                state.ttu.szExtra = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                offset += state.ttu.szExtra.Length + 1;
            }//WFS_SUCCESS
            //ttu end
            //Console.WriteLine("TTU READ OFFSET " + offset);
            return offset;
        }

        public static int ReadCashunitsData(ref WFSCASHUNITS cashUnits, IntPtr state_ptr, int buff_size, bool _getPhysicalUnits)
        {
            int offset = 0;
            //cashunits begin
            cashUnits.status = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.wfs_sizof.hresult;
            if (cashUnits.status == iopcts.XFSAPIh.WFS_SUCCESS)
            {
                cashUnits.usTellerID = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += (iopcts.cdm_cu_info.usTellerID);
                cashUnits.usCount = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += (iopcts.cdm_cu_info.usCount);
                cashUnits.lppList = new WFSCDMCASHUNIT[cashUnits.usCount];
                for (int i = 0; i < cashUnits.usCount; i++)
                {
                    ref WFSCDMCASHUNIT cashunit_i = ref cashUnits.lppList[i];
                    cashunit_i.bAppLock = Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.bAppLock);

                    cashunit_i.cCurrencyID = ReadAndConvertBytes(state_ptr, offset, iopcts.cdmcashunit.cCurrencyID);
                    offset += (iopcts.cdmcashunit.cCurrencyID);

                    byte[] bytes02 = new byte[iopcts.cdmcashunit.cUnitID];

                    cashunit_i.cUnitID = ReadAndConvertBytes(state_ptr, offset, iopcts.cdmcashunit.cUnitID);
                    offset += (iopcts.cdmcashunit.cUnitID);

                    cashunit_i.lpszCashUnitName = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                    offset += cashunit_i.lpszCashUnitName.Length + 1;

                    cashunit_i.ulCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulCount);
                    cashunit_i.ulDispensedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulDispensedCount);
                    cashunit_i.ulInitialCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulInitialCount);
                    cashunit_i.ulMaximum = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulMaximum);
                    cashunit_i.ulMinimum = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulMaximum);
                    cashunit_i.ulPresentedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulPresentedCount);
                    cashunit_i.ulRejectCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulRejectCount);
                    cashunit_i.ulRetractedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulRetractedCount);
                    cashunit_i.ulValues = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += (iopcts.cdmcashunit.ulValues);
                    cashunit_i.usNumber = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usNumber;
                    if (_getPhysicalUnits)
                    {
                        cashunit_i.usNumPhysicalCUs = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usNumPhysicalCUs;
                        cashunit_i.lppPhysical = new WFSCDMPHCU[cashunit_i.usNumPhysicalCUs];
                        for (int j = 0; j < cashunit_i.usNumPhysicalCUs; j++)
                        {
                            ref WFSCDMPHCU cashunit_ij = ref cashunit_i.lppPhysical[j];
                            cashunit_ij.bHardwareSensor = Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.bHardwareSensor;
                            cashunit_ij.cUnitID = ReadAndConvertBytes(state_ptr, offset, iopcts.cdmphcu.cUnitID);
                            offset += (iopcts.cdmphcu.cUnitID);
                            cashunit_ij.lpPhysicalPositionName = Marshal.PtrToStringAnsi(ptr_at_offset(state_ptr, offset));
                            offset += cashunit_ij.lpPhysicalPositionName.Length + 1;
                            cashunit_ij.ulCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulCount;
                            cashunit_ij.ulDispensedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulDispensedCount;
                            cashunit_ij.ulInitialCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulInitialCount;
                            cashunit_ij.ulMaximum = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulMaximum;
                            cashunit_ij.ulPresentedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulPresentedCount;
                            cashunit_ij.ulRejectCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulRejectCount;
                            cashunit_ij.ulRetractedCount = (UInt32)Marshal.ReadInt32(state_ptr, offset); offset += iopcts.cdmphcu.ulRetractedCount;
                            cashunit_ij.usPStatus = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmphcu.usPStatus;
                        }
                    }
                    else
                    {
                        cashunit_i.usNumPhysicalCUs = (UInt16)0;
                    }
                    cashunit_i.usStatus = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usStatus;
                    cashunit_i.usType = (UInt16)Marshal.ReadInt16(state_ptr, offset); offset += iopcts.cdmcashunit.usType;
                }
            }//WFS_SUCCESS
            //cashunits end
            return offset;
        }
        public static WFSCDMCASHUNIT ReadCashUnitInfo(int _size, IntPtr _dataptr)
        {
            WFSCDMCASHUNIT cashunit_i = new WFSCDMCASHUNIT();
            int offset = 0;
            cashunit_i.bAppLock = Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.bAppLock);

            cashunit_i.cCurrencyID = ReadAndConvertBytes(_dataptr, offset, iopcts.cdmcashunit.cCurrencyID);
            offset += (iopcts.cdmcashunit.cCurrencyID);

            byte[] bytes02 = new byte[iopcts.cdmcashunit.cUnitID];

            cashunit_i.cUnitID = ReadAndConvertBytes(_dataptr, offset, iopcts.cdmcashunit.cUnitID);
            offset += (iopcts.cdmcashunit.cUnitID);

            cashunit_i.lpszCashUnitName = Marshal.PtrToStringAnsi(ptr_at_offset(_dataptr, offset));
            offset += cashunit_i.lpszCashUnitName.Length + 1;

            cashunit_i.ulCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulCount);
            cashunit_i.ulDispensedCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulDispensedCount);
            cashunit_i.ulInitialCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulInitialCount);
            cashunit_i.ulMaximum = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulMaximum);
            cashunit_i.ulMinimum = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulMaximum);
            cashunit_i.ulPresentedCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulPresentedCount);
            cashunit_i.ulRejectCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulRejectCount);
            cashunit_i.ulRetractedCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulRetractedCount);
            cashunit_i.ulValues = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += (iopcts.cdmcashunit.ulValues);
            cashunit_i.usNumber = (UInt16)Marshal.ReadInt16(_dataptr, offset); offset += iopcts.cdmcashunit.usNumber;
            cashunit_i.usNumPhysicalCUs = (UInt16)Marshal.ReadInt16(_dataptr, offset); offset += iopcts.cdmcashunit.usNumPhysicalCUs;
            cashunit_i.lppPhysical = new WFSCDMPHCU[cashunit_i.usNumPhysicalCUs];
            for (int j = 0; j < cashunit_i.usNumPhysicalCUs; j++)
            {
                ref WFSCDMPHCU cashunit_ij = ref cashunit_i.lppPhysical[j];
                cashunit_ij.bHardwareSensor = Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.bHardwareSensor;
                cashunit_ij.cUnitID = ReadAndConvertBytes(_dataptr, offset, iopcts.cdmphcu.cUnitID);
                offset += (iopcts.cdmphcu.cUnitID);
                cashunit_ij.lpPhysicalPositionName = Marshal.PtrToStringAnsi(ptr_at_offset(_dataptr, offset));
                offset += cashunit_ij.lpPhysicalPositionName.Length + 1;
                cashunit_ij.ulCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.ulCount;
                cashunit_ij.ulDispensedCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.ulDispensedCount;
                cashunit_ij.ulInitialCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.ulInitialCount;
                cashunit_ij.ulMaximum = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.ulMaximum;
                cashunit_ij.ulPresentedCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.ulPresentedCount;
                cashunit_ij.ulRejectCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.ulRejectCount;
                cashunit_ij.ulRetractedCount = (UInt32)Marshal.ReadInt32(_dataptr, offset); offset += iopcts.cdmphcu.ulRetractedCount;
                cashunit_ij.usPStatus = (UInt16)Marshal.ReadInt16(_dataptr, offset); offset += iopcts.cdmphcu.usPStatus;
            }
            cashunit_i.usStatus = (UInt16)Marshal.ReadInt16(_dataptr, offset); offset += iopcts.cdmcashunit.usStatus;
            cashunit_i.usType = (UInt16)Marshal.ReadInt16(_dataptr, offset); offset += iopcts.cdmcashunit.usType;
            System.Diagnostics.Debug.Assert((offset == _size));
            return cashunit_i;
        }

        private static void FillInt32Array(Int32[] _int_array, IntPtr intPtr)
        {
            for (int i = 0; i < _int_array.Length; i++)
            {
                _int_array[i] = Marshal.ReadInt32(intPtr, i * sizeof(Int32));
            }
        }

        private static void FillInt16Array(Int16[] _int_array, IntPtr intPtr)
        {
            for (int i = 0; i < _int_array.Length; i++)
            {
                _int_array[i] = Marshal.ReadInt16(intPtr, i * sizeof(Int16));
            }
        }

        private static Int32[] ReadInt32Array(IntPtr intPtr, int len)
        {
            Int32[] int_array = new Int32[len];

            for (int i = 0; i < len; i++)
            {
                int_array[i] = Marshal.ReadInt32(intPtr, i * sizeof(Int32));
            }
            return int_array;
        }
        private static Int16[] ReadInt16Array(IntPtr intPtr, int len)
        {
            Int16[] int_array = new Int16[len];

            for (int i = 0; i < len; i++)
            {
                int_array[i] = Marshal.ReadInt16(intPtr, i * sizeof(Int16));
            }
            return int_array;
        }
        private static IntPtr ptr_at_offset(IntPtr base_ptr, int offset)
        {
            long new_ptr = base_ptr.ToInt64() + offset;
            return (IntPtr)new_ptr;
        }

        private static char[] ReadAndConvertBytes(IntPtr src_ptr, int ofs, int len)
        {
            byte[] bytes = new byte[len];
            Marshal.Copy(ptr_at_offset(src_ptr, ofs), bytes, 0, len);
            char[] chars = Encoding.ASCII.GetChars(bytes);//ASCII ??
            return chars;
        }
    }
    [Serializable]
    public struct CDMPOS
    {
        public short fwPosition;
        public short fwShutter;
        public short fwPositionStatus;
        public short fwTransport;
        public short fwTransportStatus;
    }
    [Serializable]
    public struct WFSCDMMNGED /*: WFSStatus*/
    {
        public int status;
        public int fwDevice;
        public int fwDispenserstate;
        public int fwintermediateStacker;
        public int fwSafeDoor;
        public short wDevicePosition;
        public ushort usPowerSaveRecoveryTime;
        public short wAntiFraudModule;
        public Int32[] dwGuidLights;
        public CDMPOS[] positions;
        public string szExtra;
    }
    [Serializable]
    public struct WFSCDMPHCU
    {
        public string lpPhysicalPositionName;
        public char[/*5*/] cUnitID;
        public UInt32 ulInitialCount;
        public UInt32 ulCount;
        public UInt32 ulRejectCount;
        public UInt32 ulMaximum;
        public UInt16 usPStatus;
        public Int32/*Boolean*/ bHardwareSensor;
        public UInt32 ulDispensedCount;
        public UInt32 ulPresentedCount;
        public UInt32 ulRetractedCount;
    };
    [Serializable]
    public struct WFSCDMCASHUNIT
    {
        public UInt16 usNumber;
        public UInt16 usType;
        public string lpszCashUnitName;
        public char[/*5*/] cUnitID;
        public char[/*3*/] cCurrencyID;
        public UInt32 ulValues;
        public UInt32 ulInitialCount;
        public UInt32 ulCount;
        public UInt32 ulRejectCount;
        public UInt32 ulMinimum;
        public UInt32 ulMaximum;
        public Int32/*Boolean*/ bAppLock;
        public UInt16 usStatus;
        public UInt16 usNumPhysicalCUs;
        public WFSCDMPHCU[] lppPhysical;
        public UInt32 ulDispensedCount;
        public UInt32 ulPresentedCount;
        public UInt32 ulRetractedCount;
    };
    [Serializable]
    public struct WFSCASHUNITS
    {
        public int status;
        public UInt16 usTellerID;
        public UInt16 usCount;
        public WFSCDMCASHUNIT[] lppList;
    };
    [Serializable]
    public struct WFSIDCMNGED
    {
        public int status;
        //IDC
        public Int16 fwDevice;
        public Int16 fwMedia;
        public Int16 fwRetainBin;
        public Int16 fwSecurity;
        public UInt16 usCards;
        public Int16 fwChipPower;
        public string szExtra;
        public Int32[] dwGuidLights;//WFS_IDC_GUIDLIGHTS_SIZE
        public Int16 fwChipModule;
        public Int16 fwMagReadModule;
        public Int16 fwMagWriteModule;
        public Int16 fwFrontImageModule;
        public Int16 fwBackImageModule;
        public Int16 wDevicePosition;
        public UInt16 usPowerSaveRecoveryTime;
        public Int16[] wParkingStationMedias;
        public Int16 wAntiFraudModule;
    };

    [Serializable]
    public class WFSPTRRETRACTBINS
    {
        public Int16 wRetractBin;
        public UInt16 usRetractCount;
    };
    [Serializable]
    public struct WFSPTRMNGED
    {
        public int status;
        public Int16 fwDevice;
        public Int16 fwMedia;
        public Int16[] fwPaper;//WFS_PTR_SUPPLYSIZE
        public Int16 fwToner;
        public Int16 fwInk;
        public Int16 fwLamp;
        public UInt16 usMediaOnStacker;
        public Int32[] dwGuidLights;//WFS_PTR_GUIDLIGHTS_SIZE
        public Int16 wDevicePosition;
        public UInt16 usPowerSaveRecoveryTime;
        public Int16[] wPaperType;//WFS_PTR_SUPPLYSIZE
        public Int16 wAntiFraudModule;
        public string szExtra;
        public WFSPTRRETRACTBINS[] RetractBins;
    };
    [Serializable]
    public struct WFSPINMNGED
    {
        public int status;
        public Int16 fwDevice;
        public Int16 fwEncStat;
        public string szExtra;
        public Int32[] dwGuidLights;//WFS_PIN_GUIDLIGHTS_SIZE
        public Int16 fwAutoBeepMode;
        public Int32 dwCertificateState;
        public Int16 wDevicePosition;
        public UInt16 usPowerSaveRecoveryTime;
        public Int16 wAntiFraudModule;
    };
    [Serializable]
    public struct WFSSIUMNGED
    {
        public int status;
        public Int16 fwDevice;
        public Int16[] fwSensors;
        public Int16[] fwDoors;
        public Int16[] fwIndicators;
        public Int16[] fwAuxiliaries;
        public Int16[] fwGuidLights;
        public string szExtra;
        public UInt16 usPowerSaveRecoveryTime;
        public Int16 wAntiFraudModule;
    }
    [Serializable]
    public struct WFSTTUMNGED
    {
        public int status;
        public Int16 fwDevice;
        public Int16 wKeyboard;
        public Int16 wKeylock;
        public Int16[] wLEDs;//WFS_TTU_LEDS_MAX
        public Int16 wDisplaySizeX;
        public Int16 wDisplaySizeY;
        public string szExtra;
        public Int16 wDevicePosition;
        public UInt16 usPowerSaveRecoveryTime;
        public Int32[] dwLEDsEx;
        public Int16 wAntiFraudModule;
    };
    [Serializable]
    public struct WFSATM
    {
        public WFSCDMMNGED cdm;
        /*[NonSerialized]*/
        public WFSCASHUNITS cashUnits;
        public WFSIDCMNGED idc;
        public WFSPTRMNGED jrn;
        public WFSPINMNGED pin;
        public WFSPTRMNGED rcp;
        public WFSSIUMNGED siu;
        public WFSTTUMNGED ttu;
    }
    public struct WFSATMState
    {
        public WFSCDMMNGED cdm;
        public WFSIDCMNGED idc;
        public WFSPTRMNGED jrn;
        public WFSPINMNGED pin;
        public WFSPTRMNGED rcp;
        public WFSSIUMNGED siu;
        public WFSTTUMNGED ttu;
    }


}
