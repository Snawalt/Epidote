using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Epidote.Protection
{
    public static class AntiDebugging
    {
        public static class DebuggingDetector
        {
            [StructLayout(LayoutKind.Sequential)]
            struct ObjectInformation
            {
                public int HandleAttributes;
                public int GrantedAccess;
                public int HandleCount;
                public int PointerCount;
                public int PagedPoolCharge;
                public int NonPagedPoolCharge;
                public IntPtr Object;
                public IntPtr Creator;
            }

            [Flags]
            enum SystemFlags
            {
                DebuggerPresent = 1
            }

            [StructLayout(LayoutKind.Sequential)]
            struct SystemInformation
            {
                public SystemFlags SystemFlags;
            }

            [DllImport("kernel32.dll")]
            public static extern IntPtr GetCurrentThread();

            [DllImport("kernel32.dll")]
            private static extern bool IsDebuggerPresent();

            [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
            private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

            [DllImport("ntdll.dll")]
            private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ProcessDebugPort processDebugPort, int processDebugPortSize, out int returnLength);

            [DllImport("ntdll.dll")]
            private static extern int NtSetInformationThread(IntPtr hThread, int threadInformationClass, ref int threadInformation, int threadInformationLength);

            [DllImport("ntdll.dll")]
            private static extern int NtQueryObject(IntPtr hObject, int objectInformationClass, ref ObjectInformation objectInformation, int objectInformationLength, ref int returnLength);

            [DllImport("ntdll.dll")]
            private static extern int NtQuerySystemInformation(int systemInformationClass, ref SystemInformation systemInformation, int systemInformationLength, ref int returnLength);

            public static bool CheckDebugPort()
            {
                int returnLength;
                ProcessDebugPort processDebugPort = new ProcessDebugPort();
                int status = NtQueryInformationProcess(Process.GetCurrentProcess().Handle, 7, ref processDebugPort, Marshal.SizeOf(processDebugPort), out returnLength);
                return status == 0 && processDebugPort.DebugPort != 0;
            }


            public static bool CheckThreadDebugging()
            {
                int threadInfo = 0;
                IntPtr handle = GetCurrentThread();
                int status = NtSetInformationThread(handle, 0x11, ref threadInfo, 4);
                return status == 0;
            }

            public static bool CheckSystemDebugging()
            {
                int returnLength = 0;
                SystemInformation systemInfo = new SystemInformation();
                int status = NtQuerySystemInformation(0x1F, ref systemInfo, Marshal.SizeOf(systemInfo), ref returnLength);
                return status == 0 && systemInfo.SystemFlags.HasFlag(SystemFlags.DebuggerPresent);
            }

            public static bool CheckHandleDebugging()
            {
                int returnLength = 0;
                ObjectInformation objectInfo = new ObjectInformation();
                int status = NtQueryObject(Process.GetCurrentProcess().Handle, 2, ref objectInfo, Marshal.SizeOf(objectInfo), ref returnLength);
                return status == 0 && objectInfo.HandleAttributes == 0x80;
            }

            public static bool CheckDebuggerRegistryKeys()
            {
                try
                {
                    using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\AeDebug"))
                    {
                        return key.GetValue("Debugger").ToString().Length > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }


            public static readonly string[] DebuggerProcessNames =
            {
            "windbg",
            "ollydbg",
            "ida",
            "mdb",
            "dotPeek",
            "dnSpy",
            "SharpDevelop",
            "ilspy"
            };

            public static bool IsDebuggerRunning()
            {
                return DebuggerProcessNames.Any(debuggerProcessName => Process.GetProcessesByName(debuggerProcessName).Length > 0);
            }

            public static bool IsDebuggerAttached()
            {
                if (IsDebuggerPresent())
                {
                    return true;
                }

                bool isDebuggerPresent = false;
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
                if (isDebuggerPresent)
                {
                    return true;
                }

                if (CheckDebugPort() || CheckThreadDebugging() || CheckHandleDebugging() || CheckSystemDebugging() || IsDebuggerRunning())
                {
                    return true;
                }

                return false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct ProcessDebugPort
        {
            public int DebugPort;
        }
    }
}
