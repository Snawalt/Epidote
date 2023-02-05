using System;
using System.Management;
using System.Net;

namespace Epidote.Protection
{
    public class HardwareInfo
    {
        public static void GetHardwareInformation()
        {
            try
            {
                // systemGuid
                string systemGuid = GetHardwareIdentifier("Win32_ComputerSystemProduct", "UUID");
                ExceptionLogger.Write(LogEvent.Info, $"System GUID: {systemGuid}", false);

                // Get the processor identifier
                string processorId = GetHardwareIdentifier("Win32_Processor", "ProcessorId");
                ExceptionLogger.Write(LogEvent.Info, $"Processor identifier: {processorId}", false);

                // Get the motherboard identifier
                string motherboardId = GetHardwareIdentifier("Win32_BaseBoard", "SerialNumber");
                ExceptionLogger.Write(LogEvent.Info, $"Motherboard identifier: {motherboardId}", false);

                // Get the hard drive identifier
                string hardDriveId = GetHardwareIdentifier("Win32_DiskDrive", "SerialNumber");
                ExceptionLogger.Write(LogEvent.Info, $"Hard drive identifier: {hardDriveId}", false);


                // Get the operating system serial number
                string osSerial = GetHardwareIdentifier("Win32_OperatingSystem", "SerialNumber");
                ExceptionLogger.Write(LogEvent.Info, $"Operating system serial number: {osSerial}", false);

                // videoCardId
                string videoCardId = GetHardwareIdentifier("Win32_VideoController", "DeviceID");
                ExceptionLogger.Write(LogEvent.Info, $"Video card identifier: {videoCardId}", false);

                // usbId
                string usbId = GetHardwareIdentifier("Win32_USBController", "PNPDeviceID");
                ExceptionLogger.Write(LogEvent.Info, $"USB identifier: {usbId}", false);

                // motherboardManufacturer
                string motherboardManufacturer = GetHardwareIdentifier("Win32_BaseBoard", "Manufacturer");
                ExceptionLogger.Write(LogEvent.Info, $"Motherboard manufacturer: {motherboardManufacturer}", false);

                // processorManufacturer
                string processorManufacturer = GetHardwareIdentifier("Win32_Processor", "Manufacturer");
                ExceptionLogger.Write(LogEvent.Info, $"Processor manufacturer: {processorManufacturer}", false);

                // memoryIds
                string memoryIds = "";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
                ManagementObjectCollection results = searcher.Get();
                foreach (ManagementObject result in results)
                {
                    memoryIds += result["SerialNumber"] + " ";
                }
                ExceptionLogger.Write(LogEvent.Info, $"Memory module identifiers: {memoryIds}", false);

                // biosVersion
                string biosVersion = GetHardwareIdentifier("Win32_BIOS", "SMBIOSBIOSVersion");
                ExceptionLogger.Write(LogEvent.Info, $"BIOS version: {biosVersion}", false);

                // processorClockSpeed
                string processorClockSpeed = GetHardwareIdentifier("Win32_Processor", "MaxClockSpeed");
                ExceptionLogger.Write(LogEvent.Info, $"Processor clock speed: {processorClockSpeed} MHz", false);

                // systemType
                string systemType = GetHardwareIdentifier("Win32_ComputerSystem", "SystemType");
                ExceptionLogger.Write(LogEvent.Info, $"System type: {systemType}", false);

                // systemIp
                string systemIp = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                ExceptionLogger.Write(LogEvent.Info, $"System IP address: {systemIp}", false);

                // osName
                string osName = GetHardwareIdentifier("Win32_OperatingSystem", "Caption");
                ExceptionLogger.Write(LogEvent.Info, $"Operating System name: {osName}", false);

                // userName
                string userName = GetHardwareIdentifier("Win32_ComputerSystem", "UserName");
                ExceptionLogger.Write(LogEvent.Info, $"System user name: {userName}", false);

                // totalRam
                string totalRAM = GetHardwareIdentifier("Win32_ComputerSystem", "TotalPhysicalMemory");
                ExceptionLogger.Write(LogEvent.Info, $"Total system RAM: {totalRAM} bytes", false);

                //systemDateTime
                string systemDateTime = DateTime.Now.ToString();
                ExceptionLogger.Write(LogEvent.Info, $"System date and time: {systemDateTime}", false);

                // Get the Ethernet identifier (MAC address)
                string ethernetId = "";
                ManagementObjectSearcher ethernetSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter=True");
                ManagementObjectCollection ethernetResults = ethernetSearcher.Get();
                foreach (ManagementObject ethernetResult in ethernetResults)
                {
                    ethernetId += ethernetResult["MACAddress"] + " ";
                }
                ExceptionLogger.Write(LogEvent.Info, $"Ethernet identifier: {ethernetId}", false);

            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, $"error at: {ex.ToString()}", false);
            }
        }

        private static string GetHardwareIdentifier(string wmiClass, string wmiProperty)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {wmiClass}");
                ManagementObjectCollection results = searcher.Get();
                string identifier = "";

                foreach (ManagementObject result in results)
                {
                    identifier = result[wmiProperty].ToString();
                }

                return identifier;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, $"error at: {ex.ToString()}", false);
                return null;
            }

        }
    }
}
