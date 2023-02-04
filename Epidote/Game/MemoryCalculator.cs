using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Epidote.Game
{
    public class MemoryCalculator
    {
        // The ratio of memory that should be allocated to the JVM.
        private const double MemoryAllocationRatio = 5.36916270687;

        // The minimum amount of memory that should be allocated to the JVM when the system has less than 2GB of memory.
        private const double MinimumMemoryAllocation = 1000;

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryInKilobytes);


        // Calculate the amount of memory to allocate to the JVM.
        static double javaMemoryAllocation;
        public static int CalculateJavaMemoryAllocation()
        {
            // Get the total amount of physically installed system memory.
            long totalMemoryInKilobytes;
            GetPhysicallyInstalledSystemMemory(out totalMemoryInKilobytes);


            if (Environment.Is64BitOperatingSystem)
            {
                if (totalMemoryInKilobytes / 1024 / MemoryAllocationRatio < 2)
                {
                    javaMemoryAllocation = MinimumMemoryAllocation;
                }
                else
                {
                    javaMemoryAllocation = totalMemoryInKilobytes / 1024 / MemoryAllocationRatio;
                }
            }
            return Convert.ToInt32(javaMemoryAllocation);
        }
    }

}
