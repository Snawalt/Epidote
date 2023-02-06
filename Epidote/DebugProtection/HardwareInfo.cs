using System;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Epidote.Protection
{
    public class HardwareInfo
    {
        public static string GetHardwareInformationHash()
        {
            try
            {
                string hardwareId = "";

                // 1
                string systemGuid = GetHardwareIdentifier("Win32_ComputerSystemProduct", "UUID");
                hardwareId += systemGuid;

                // 2
                string processorId = GetHardwareIdentifier("Win32_Processor", "ProcessorId");
                hardwareId += processorId;

                // 3
                string motherboardId = GetHardwareIdentifier("Win32_BaseBoard", "SerialNumber");
                hardwareId += motherboardId;

                // 4
                string hardDriveId = GetHardwareIdentifier("Win32_DiskDrive", "SerialNumber");
                hardwareId += hardDriveId;

                // 5
                string osSerial = GetHardwareIdentifier("Win32_OperatingSystem", "SerialNumber");
                hardwareId += osSerial;

                // 6
                string ethernetId = "";
                ManagementObjectSearcher ethernetSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter=True");
                ManagementObjectCollection ethernetResults = ethernetSearcher.Get();
                foreach (ManagementObject ethernetResult in ethernetResults)
                {
                    ethernetId += ethernetResult["MACAddress"] + " ";
                }
                hardwareId += ethernetId;


                // Hash the result
                byte[] data = Encoding.ASCII.GetBytes(hardwareId);
                byte[] hash = new SHA256Managed().ComputeHash(data);
                string result = Convert.ToBase64String(hash);

                ExceptionLogger.Write(LogEvent.Info, "basic: "+hardwareId + " encrypted: "+result, false);

                return result;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, $"error at: {ex.ToString()}", false);
                return null;
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
