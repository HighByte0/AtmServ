namespace AtmStateLib
{
    public class AtmLabels
    {
        static public string[] COMPONENT = {
        "ATM"
            ,"CDM"
            ,"IDC"
            ,"JRN"
            ,"PIN"
            ,"RCP"
            ,"SIU"
            ,"TTU"
    };
    }
    public enum FState
    {
        notsupp,
        ok,
        softerr,
        error,
        danger,
        warning,
        unknown,
        skip,
        todo,
    };
    public class StateDescript
    {
        public string label;
    
        public string descri;
        public FState type;
        public StateDescript(string _label, FState _state, string _desc)
        {
            label = _label;
            type = _state;
            descri = _desc;
            
        }
    }

    public class CDMLabels
    {
        static public StateDescript WfsGetInfo = new StateDescript("CDM SP ERROR", FState.softerr, " Service Provider Could not retrieve info.");
        static public StateDescript[] Device =
        {
new StateDescript("DEVONLINE", FState.ok, "The device is online.")
,new StateDescript("DEVOFFLINE", FState.error, "The device is offline ")
,new StateDescript("DEVPOWEROFF", FState.error, "The device is powered off or physically not connected.")
,new StateDescript("DEVNODEVICE", FState.error, "The device is not intended to be there, e.g. this type of self service machine does not contain such a device or it is internally not configured.")
,new StateDescript("DEVHWERROR", FState.error, "The device is inoperable due to a hardware error.")
,new StateDescript("DEVUSERERROR", FState.error, "The device is present but a person is preventing proper device operation.")
,new StateDescript("DEVBUSY", FState.ok, "The device is busy and unable to process an Execute command at this time.")
,new StateDescript("DEVFRAUDATTEMPT", FState.danger, "The device is present but is inoperable because it has detected a fraud attempt.")
,new StateDescript("DEVPOTENTIALFRAUD", FState.danger, "The device has detected a potential fraud attempt and is capable of remaining in service.")
};
        static public StateDescript[] SafeDoor =
        {
new StateDescript("", FState.skip, "") //0
,new StateDescript("DOORNOTSUPPORTED", FState.notsupp, "") //(1)
,new StateDescript("DOOROPEN", FState.danger, "Safe door is open. ")         //(2)
,new StateDescript("DOORCLOSED", FState.ok, "Safe door is closed. ")       //(3)
,new StateDescript("", FState.skip, "")      //4
,new StateDescript("DOORUNKNOWN", FState.unknown, "Due to a hardware error or other condition, the state of the safe door cannot be determined.")      //(5)
};
        static public StateDescript[] Dispenser =
        {
new StateDescript("DISPOK", FState.ok, "All cash units present are in a good state. ")
,new StateDescript("DISPCUSTATE", FState.warning, "One or more of the cash units is in a low, empty, inoperative or manipulated condition. Items can still be dispensed from at least one of the cash units. ")
,new StateDescript("DISPCUSTOP", FState.error, "Due to a cash unit failure dispensing is impossible. No items can be dispensed because all of the cash units are in an empty, inoperative or manipulated condition. This state may also occur when a reject/retract cash unit is full or no reject/retract cash unit is present, or when an application lock is set on every cash unit. ")
,new StateDescript("DISPCUUNKNOWN", FState.unknown, "Due to a hardware error or other condition, the state of the cash units cannot be determined. ")
};
        static public StateDescript[] IntermediateStacker =
        {
new StateDescript("ISEMPTY", FState.ok, "The intermediate stacker is empty. ")
,new StateDescript("ISNOTEMPTY", FState.ok, "The intermediate stacker is not empty. The items have not been in customer access. ")
,new StateDescript("ISNOTEMPTYCUST", FState.warning, "The intermediate stacker is not empty. The items have been in customer access. If the device is a recycler then the items on the intermediate stacker may be there as a result of a previous cash-in operation")
,new StateDescript("ISNOTEMPTYUNK", FState.warning, "The intermediate stacker is not empty. It is not known if the items have been in customer access")
,new StateDescript("ISUNKNOWN", FState.unknown, "Due to a hardware error or other condition, the state of the intermediate stacker cannot be determined. ")
,new StateDescript("ISNOTSUPPORTED", FState.notsupp, "The physical device has no intermediate stacker")

};
        static public StateDescript[] Shutter01 =
        {
        new StateDescript("SHTCLOSED", FState.ok, "The shutter is closed. ")
        ,new StateDescript("SHTOPEN", FState.ok, "The shutter is opened. ")
        ,new StateDescript("SHTJAMMED", FState.error, "The shutter is jammed. ")
        ,new StateDescript("SHTUNKNOWN", FState.unknown, "Due to a hardware error or other condition, the state of the shutter cannot be determined. ")
        ,new StateDescript("SHTNOTSUPPORTED", FState.notsupp, "The physical device has no shutter or shutter state reporting is not supported. ")
        };
        static public StateDescript[] Transport01 =
        {
        new StateDescript("TPOK", FState.ok, "The transport is in a good state. ")
        ,new StateDescript("TPINOP", FState.error, "The transport is inoperative due to a hardware failure or media jam.")
        ,new StateDescript("TPUNKNOWN", FState.unknown, "Due to a hardware error or other condition the state of the transport cannot be determined.")
        ,new StateDescript("TPNOTSUPPORTED", FState.notsupp, "The physical device has no transport or transport state reporting is not supported")
        };
        static public StateDescript[] DevicePosition =
        {
        new StateDescript("DEVICEINPOSITION", FState.ok, "The device is in its normal operating position, or is fixed in place and cannot be moved. ")
        ,new StateDescript("DEVICENOTINPOSITION", FState.error, "The device has been removed from its normal operating position. ")
        ,new StateDescript("DEVICEPOSUNKNOWN", FState.unknown, "Due to a hardware error or other condition, the position of the device cannot be determined. ")
        ,new StateDescript("DEVICEPOSNOTSUPP", FState.notsupp, "The physical device does not have the capability of detecting the position. ")
        };
        static public StateDescript[] AntiFraudModule =
        {
        new StateDescript("AFMNOTSUPP", FState.notsupp, "No anti-fraud module is available. ")
        ,new StateDescript("AFMOK", FState.ok, "Anti-fraud module is in a good state and no foreign device is detected. ")
        ,new StateDescript("AFMINOP", FState.error, "Anti-fraud module is inoperable. ")
        ,new StateDescript("AFMDEVICEDETECTED", FState.danger, "Anti-fraud module detected the presence of a foreign device. ")
        ,new StateDescript("AFMUNKNOWN", FState.unknown, "The state of the anti-fraud module cannot be determined. ")
        };
    }
    public class CULabels
    {
        /* values of WFSCDMCASHUNIT.usStatus */
        static public StateDescript[] K7Status =
        {
            new StateDescript("OK", FState.ok, "The cash unit is in a good state. "),
            new StateDescript("FULL", FState.error, "The cash unit is full."),
            new StateDescript("HIGH", FState.warning, "The cash unit is almost full "),
            new StateDescript("LOW", FState.warning, "The cash unit is almost empty "),
            new StateDescript("EMPTY", FState.error, "The cash unit is empty, or insufficient items in the cash unit are preventing further dispense operations."),
            new StateDescript("INOP", FState.error, "The cash unit is inoperative. "),
            new StateDescript("MISSING", FState.error, "The cash unit is missing. "),
            new StateDescript("NOVAL", FState.error, "The values of the specified cash unit are not available. "),
            new StateDescript("NOREF", FState.error, "There is no reference value available for the notes in this cash unit. The cash unit has not been calibrated. "),
            new StateDescript("MANIP", FState.error, "The cash unit has been inserted (including removal followed by a reinsertion) when the device was not in the exchange state. This cash unit cannot be dispensed from. "),
        };
    }
    public class IDCLabels
    {
        static public StateDescript WfsGetInfo = new StateDescript("IDC SP ERROR", FState.softerr, " Service Provider Could not retrieve info.");
        static public StateDescript[] Device =
   {
new StateDescript("DEVONLINE", FState.ok, "The device is online.")
,new StateDescript("DEVOFFLINE", FState.error, "The device is offline ")
,new StateDescript("DEVPOWEROFF", FState.error, "The device is powered off or physically not connected.")
,new StateDescript("DEVNODEVICE", FState.error, "The device is not intended to be there, e.g. this type of self service machine does not contain such a device or it is internally not configured.")
,new StateDescript("DEVHWERROR", FState.error, "The device is inoperable due to a hardware error.")
,new StateDescript("DEVUSERERROR", FState.error, "The device is present but a person is preventing proper device operation.")
,new StateDescript("DEVBUSY", FState.ok, "The device is busy and unable to process an Execute command at this time.")
,new StateDescript("DEVFRAUDATTEMPT", FState.danger, "The device is present but is inoperable because it has detected a fraud attempt.")
,new StateDescript("DEVPOTENTIALFRAUD", FState.danger, "The device has detected a potential fraud attempt and is capable of remaining in service.")
};
        static public StateDescript[] Media =
        {
        new StateDescript("", FState.skip, "")
        ,new StateDescript("MEDIAPRESENT", FState.ok, " A Card is present in the device")
        ,new StateDescript("MEDIANOTPRESENT", FState.ok, "No card is not present in the device")
        ,new StateDescript("MEDIAJAMMED", FState.error, "A card is jammed in the device; operator intervention is required.")
        ,new StateDescript("MEDIANOTSUPP", FState.notsupp, "Capability to report media position is not supported by the device (e.g. a typical swipereader or contactless chip card reader).")
        ,new StateDescript("MEDIAUNKNOWN", FState.error, "The card state cannot be determined with the device in its current state")
        ,new StateDescript("MEDIAENTERING", FState.ok, "Media is at the entry/exit slot of a motorized device")
        ,new StateDescript("MEDIALATCHED", FState.ok, "Media is present")
        };
        static public StateDescript[] RetainBin =
        {
        new StateDescript("", FState.skip, "")
        ,new StateDescript("RETAINBINOK", FState.ok, "The retain bin is in a good state.")
        ,new StateDescript("RETAINNOTSUPP", FState.notsupp, "The ID card unit does not support retain capability.")
        ,new StateDescript("RETAINBINFULL", FState.error, "The retain bin of the ID card unit is full.")
        ,new StateDescript("RETAINBINHIGH", FState.warning, "The retain bin of the ID card unit is nearly full.")
        ,new StateDescript("RETAINBINMISSING", FState.error, "The retain bin of the ID card unit is missing.")
        };
        static public StateDescript[] Security =
        {
        new StateDescript("", FState.skip, "")
        ,new StateDescript("SECNOTSUPP", FState.notsupp, "No security module is available.")
        ,new StateDescript("SECNOTREADY", FState.error, "The security module is not ready to process cards or is inoperable.")
        ,new StateDescript("SECOPEN", FState.ok, "The security module is open and ready to process cards. ")
        };
        static public string CardsDescr = "The number of cards retained";
        static public StateDescript[] ChipModule =
        {
        new StateDescript("", FState.skip, "")
        ,new StateDescript("CHIPMODOK", FState.ok, "The chip is present, powered on and online")
        ,new StateDescript("CHIPMODINOP", FState.error, "The chip is present, but powered off")
        ,new StateDescript("CHIPHWERROR", FState.error, "The chip is present, but inoperable due to a hardware error")
        ,new StateDescript("CHIPMODUNKNOWN", FState.unknown, "Capability to report the state of the chip is not supported by the ID card unit device.")
        ,new StateDescript("CHIPMODNOTSUPP", FState.notsupp, "The state of the chip cannot be determined")
        };
        static public StateDescript[] MagReadModule =
        {
        new StateDescript("", FState.skip, "")
        ,new StateDescript("MAGMODOK", FState.ok, "The magnetic card reading module is in a good state.")
        ,new StateDescript("MAGMODINOP", FState.error, "The magnetic card reading module is inoperable.")
        ,new StateDescript("MAGMODUNKNOWN", FState.unknown, "The state of the magnetic reading module cannot be determined.")
        ,new StateDescript("MAGMODNOTSUPP", FState.notsupp, "Reporting the magnetic reading module status is not supported.")
        };
        static public StateDescript[] DevicePosition =
        {
        new StateDescript("DEVICEINPOSITION", FState.ok, "The device is in its normal operating position, or is fixed in place and cannot be moved. ")
        ,new StateDescript("DEVICENOTINPOSITION", FState.error, "The device has been removed from its normal operating position. ")
        ,new StateDescript("DEVICEPOSUNKNOWN", FState.unknown, "Due to a hardware error or other condition, the position of the device cannot be determined. ")
        ,new StateDescript("DEVICEPOSNOTSUPP", FState.notsupp, "The physical device does not have the capability of detecting the position. ")
        };
        static public StateDescript[] AntiFraudModule =
        {
        new StateDescript("AFMNOTSUPP", FState.notsupp, "No anti-fraud module is available.")
        ,new StateDescript("AFMOK", FState.ok, "Anti-fraud module is in a good state")
        ,new StateDescript("AFMINOP", FState.error, "Anti-fraud module is inoperable.")
        ,new StateDescript("AFMDEVICEDETECTED", FState.danger, "Anti-fraud module detected the presence of a foreign device.")
        ,new StateDescript("AFMUNKNOWN", FState.unknown, "The state of the anti-fraud module cannot be determined.")
        };
    }
    public class PTRLabels
    {
        static public StateDescript WfsGetInfo = new StateDescript("PTR SP ERROR", FState.softerr, " Service Provider Could not retrieve info.");
        static public StateDescript[] Device =
   {
new StateDescript("DEVONLINE", FState.ok, "The device is online.")
,new StateDescript("DEVOFFLINE", FState.error, "The device is offline ")
,new StateDescript("DEVPOWEROFF", FState.error, "The device is powered off or physically not connected.")
,new StateDescript("DEVNODEVICE", FState.error, "The device is not intended to be there, e.g. this type of self service machine does not contain such a device or it is internally not configured.")
,new StateDescript("DEVHWERROR", FState.error, "The device is inoperable due to a hardware error.")
,new StateDescript("DEVUSERERROR", FState.error, "The device is present but a person is preventing proper device operation.")
,new StateDescript("DEVBUSY", FState.ok, "The device is busy and unable to process an Execute command at this time.")
,new StateDescript("DEVFRAUDATTEMPT", FState.danger, "The device is present but is inoperable because it has detected a fraud attempt.")
,new StateDescript("DEVPOTENTIALFRAUD", FState.danger, "The device has detected a potential fraud attempt and is capable of remaining in service.")
};
        static public StateDescript[] Media =
        {
                new StateDescript("MEDIAPRESENT  ", FState.ok, "Media is in the print position")
                ,new StateDescript("MEDIANOTPRESENT", FState.ok, "Media is not in the print position or on the stacker. ")
                ,new StateDescript("MEDIAJAMMED   ", FState.error, "Media is jammed in the device.")
                ,new StateDescript("MEDIANOTSUPP  ", FState.notsupp, "The capability to report the state of the print media is not supported by the device.")
                ,new StateDescript("MEDIAUNKNOWN  ", FState.unknown, "The state of the print media cannot be determined")
                ,new StateDescript("MEDIAENTERING ", FState.ok, "Media is at the entry/exit slot of the device")
                ,new StateDescript("MEDIARETRACTED", FState.ok, "Media was retracted during the reset operation. ")
        };
        static public StateDescript[] Paper =
        {
         new StateDescript("PAPERFULL", FState.ok, "The paper supply is full. ")
        ,new StateDescript("PAPERLOW", FState.warning, "The paper supply is low. ")
        ,new StateDescript("PAPEROUT", FState.error, "The paper supply is empty. ")
        ,new StateDescript("PAPERNOTSUPP", FState.notsupp, "Capability not supported by device. ")
        ,new StateDescript("PAPERUNKNOWN", FState.unknown, "Status cannot be determined with device in its current state.")
        ,new StateDescript("PAPERJAMMED", FState.error, "The paper supply is jammed. ")
    };
    }
    public class PINLabels
    {
        static public StateDescript WfsGetInfo = new StateDescript("PIN SP ERROR", FState.softerr, " Service Provider Could not retrieve info.");
        static public StateDescript[] Device =
       {
new StateDescript("DEVONLINE", FState.ok, "The device is online.")
,new StateDescript("DEVOFFLINE", FState.error, "The device is offline ")
,new StateDescript("DEVPOWEROFF", FState.error, "The device is powered off or physically not connected.")
,new StateDescript("DEVNODEVICE", FState.error, "The device is not intended to be there, e.g. this type of self service machine does not contain such a device or it is internally not configured.")
,new StateDescript("DEVHWERROR", FState.error, "The device is inoperable due to a hardware error.")
,new StateDescript("DEVUSERERROR", FState.error, "The device is present but a person is preventing proper device operation.")
,new StateDescript("DEVBUSY", FState.ok, "The device is busy and unable to process an Execute command at this time.")
,new StateDescript("DEVFRAUDATTEMPT", FState.danger, "The device is present but is inoperable because it has detected a fraud attempt.")
,new StateDescript("DEVPOTENTIALFRAUD", FState.danger, "The device has detected a potential fraud attempt and is capable of remaining in service.")
};
        static public StateDescript[] EncStat =
        {
        new StateDescript("ENCREADY", FState.ok, "The encryption module is ready")
        ,new StateDescript("ENCNOTREADY", FState.error, "The encryption module is not available or not ready due to hardware error or communication error.")
        ,new StateDescript("ENCNOTINITIALIZED", FState.error, "The encryption module is not initialized (no master key loaded).")
        ,new StateDescript("ENCBUSY", FState.ok, "The encryption module is busy")
        ,new StateDescript("ENCUNDEFINED", FState.unknown, "The encryption module state is undefined.")
        ,new StateDescript("ENCINITIALIZED", FState.ok, "The encryption module is initialized")
        ,new StateDescript("ENCPINTAMPERED", FState.danger, "")
        };
        static public StateDescript[] AntiFraudModule =
         {
        new StateDescript("AFMNOTSUPP", FState.notsupp, "No anti-fraud module is available.")
        ,new StateDescript("AFMOK", FState.ok, "Anti-fraud module is in a good state")
        ,new StateDescript("AFMINOP", FState.error, "Anti-fraud module is inoperable.")
        ,new StateDescript("AFMDEVICEDETECTED", FState.danger, "Anti-fraud module detected the presence of a foreign device.")
        ,new StateDescript("AFMUNKNOWN", FState.unknown, "The state of the anti-fraud module cannot be determined.")
        };
    }
    public class SIULabels
    {
        static public StateDescript WfsGetInfo = new StateDescript("SIU SP ERROR", FState.softerr, " Service Provider Could not retrieve info.");
        static public StateDescript[] Device =
       {
new StateDescript("DEVONLINE", FState.ok, "The device is online.")
,new StateDescript("DEVOFFLINE", FState.error, "The device is offline ")
,new StateDescript("DEVPOWEROFF", FState.error, "The device is powered off or physically not connected.")
,new StateDescript("DEVNODEVICE", FState.error, "The device is not intended to be there, e.g. this type of self service machine does not contain such a device or it is internally not configured.")
,new StateDescript("DEVHWERROR", FState.error, "The device is inoperable due to a hardware error.")
,new StateDescript("DEVUSERERROR", FState.error, "The device is present but a person is preventing proper device operation.")
,new StateDescript("DEVBUSY", FState.ok, "The device is busy and unable to process an Execute command at this time.")
,new StateDescript("DEVFRAUDATTEMPT", FState.danger, "The device is present but is inoperable because it has detected a fraud attempt.")
,new StateDescript("DEVPOTENTIALFRAUD", FState.danger, "The device has detected a potential fraud attempt and is capable of remaining in service.")
};
        static public StateDescript[] SensorsOp =
        {
            new StateDescript("NOT_AVAILABLE", FState.notsupp, "WFS_SIU_NOT_AVAILABLE The status is not available.")//0
            ,new StateDescript("AVAILABLE (RUN)", FState.ok, "The switch is in Run mode.")//1
            ,new StateDescript("MAINTENANCE", FState.ok, "The switch is in Maintenance mode.")//2
            ,new StateDescript("RUN|MAINTENANCE", FState.ok, "The switch is in Maintenance mode.")//3
            ,new StateDescript("SUPERVISOR", FState.ok, "The switch is in Supervisor mode.")//4
            ,new StateDescript("RUN|SUPERVISOR", FState.ok, "The switch is in Supervisor mode.")//5
            ,new StateDescript("MAINTENANCE|SUPERVISOR", FState.ok, "The switch is in Maintenance mode.")//6
            ,new StateDescript("RUN|MAINTENANCE|SUPERVISOR", FState.ok, "")//7
        };
        static public StateDescript[] Indicators01 =//(OPENCLOSE)
        {
            new StateDescript("NOT_AVAILABLE", FState.notsupp, "The status is not available.")//0
            ,new StateDescript("SIU_CLOSED", FState.ok, "The terminal is closed for a consumer.")// (0x0001);
            ,new StateDescript("SIU_OPEN", FState.ok, "The terminal is open to be used by a consumer.")// (0x0002);
            //,new StateDescript("SIU_LOCKED", FState, "")// (0x0004);
            //,new StateDescript("SIU_BOLTED", FState, "")// (0x0008);
            //,new StateDescript("SIU_SERVICE", FState, "")// (0x0010);btawxzsx
            //,new StateDescript("SIU_KEYBOARD", FState, "")// (0x0020);
            //,new StateDescript("SIU_AJAR", FState, "")// (0x0040);
            //,new StateDescript("SIU_JAMMED", FState, "")// (0x0080);
        };
        static public StateDescript[] Indicators02 =//FASCIALIGHT
        {
            new StateDescript("NOT_AVAILABLE", FState.unknown, "The status is not available.")
            ,new StateDescript("OFF", FState.todo, "The Fascia Light is turned off.")
            ,new StateDescript("ON", FState.todo, "The Fascia Light is turned on.")
        };
        static public StateDescript[] AntiFraudModule =
         {
        new StateDescript("AFMNOTSUPP", FState.notsupp, "No anti-fraud module is available.")
        ,new StateDescript("AFMOK", FState.ok, "Anti-fraud module is in a good state")
        ,new StateDescript("AFMINOP", FState.error, "Anti-fraud module is inoperable.")
        ,new StateDescript("AFMDEVICEDETECTED", FState.danger, "Anti-fraud module detected the presence of a foreign device.")
        ,new StateDescript("AFMUNKNOWN", FState.unknown, "The state of the anti-fraud module cannot be determined.")
        };
    }
    public class TTULabels
    {
        static public StateDescript WfsGetInfo = new StateDescript("TTU SP ERROR", FState.softerr, " Service Provider Could not retrieve info.");
        static public StateDescript[] Device =
      {
new StateDescript("DEVONLINE", FState.ok, "The device is online.")
,new StateDescript("DEVOFFLINE", FState.error, "The device is offline ")
,new StateDescript("DEVPOWEROFF", FState.error, "The device is powered off or physically not connected.")
,new StateDescript("DEVNODEVICE", FState.error, "The device is not intended to be there, e.g. this type of self service machine does not contain such a device or it is internally not configured.")
,new StateDescript("DEVHWERROR", FState.error, "The device is inoperable due to a hardware error.")
,new StateDescript("DEVUSERERROR", FState.error, "The device is present but a person is preventing proper device operation.")
,new StateDescript("DEVBUSY", FState.ok, "The device is busy and unable to process an Execute command at this time.")
,new StateDescript("DEVFRAUDATTEMPT", FState.danger, "The device is present but is inoperable because it has detected a fraud attempt.")
,new StateDescript("DEVPOTENTIALFRAUD", FState.danger, "The device has detected a potential fraud attempt and is capable of remaining in service.")
};
    }
}
