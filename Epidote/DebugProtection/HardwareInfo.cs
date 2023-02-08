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
                // Initialize an empty string to store the combined result of all hardware information
                string hardwareId = "";

                // Get the unique identifier for the computer system product and add it to the hardwareId string
                string systemGuid = GetHardwareIdentifier("Win32_ComputerSystemProduct", "UUID");
                hardwareId += systemGuid;

                // Get the processor id and add it to the hardwareId string
                string processorId = GetHardwareIdentifier("Win32_Processor", "ProcessorId");
                hardwareId += processorId;

                // Get the motherboard serial number and add it to the hardwareId string
                string motherboardId = GetHardwareIdentifier("Win32_BaseBoard", "SerialNumber");
                hardwareId += motherboardId;

                // Get the hard drive serial number and add it to the hardwareId string
                string hardDriveId = GetHardwareIdentifier("Win32_DiskDrive", "SerialNumber");
                hardwareId += hardDriveId;

                // Get the operating system serial number and add it to the hardwareId string
                string osSerial = GetHardwareIdentifier("Win32_OperatingSystem", "SerialNumber");
                hardwareId += osSerial;

                // Get the MAC address of the physical Ethernet adapter and add it to the hardwareId string
                string ethernetId = "";
                ManagementObjectSearcher ethernetSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter=True");
                ManagementObjectCollection ethernetResults = ethernetSearcher.Get();
                foreach (ManagementObject ethernetResult in ethernetResults)
                {
                    ethernetId += ethernetResult["MACAddress"] + " ";
                }
                hardwareId += ethernetId;

                // Compute the SHA-256 hash of the combined hardware information and convert it to a base64 string
                byte[] data = Encoding.ASCII.GetBytes(hardwareId);
                byte[] hash = new SHA256Managed().ComputeHash(data);
                string result = Convert.ToBase64String(hash);

                // Log the original and encrypted hardware information
                ExceptionLogger.Write(LogEvent.Info, "basic: " + hardwareId + " encrypted: " + result, false);

                // Return the encrypted result
                return result;
            }
            catch (Exception ex)
            {
                // Log the error message if 
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
