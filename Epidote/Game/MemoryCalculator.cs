using System;
using System.Runtime.InteropServices;

namespace Epidote.Game
{
    // This class is responsible for calculating the amount of memory to allocate to the JVM (Java Virtual Machine).

    public class MemoryCalculator
    {
        // The ratio of memory that should be allocated to the JVM.
        private const double MemoryAllocationRatio = 5.36916270687;

        // The minimum amount of memory that should be allocated to the JVM when the system has less than 2GB of memory.
        private const double MinimumMemoryAllocation = 1000;

        // Import the "GetPhysicallyInstalledSystemMemory" function from the "kernel32.dll" library.
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryInKilobytes);

        // Declare a variable to store the calculated memory allocation for the JVM.
        static double javaMemoryAllocation;

        // Calculate the amount of memory to allocate to the JVM.
        public static int CalculateJavaMemoryAllocation()
        {
            // Get the total amount of physically installed system memory.
            long totalMemoryInKilobytes;
            GetPhysicallyInstalledSystemMemory(out totalMemoryInKilobytes);

            // If the operating system is 64-bit:
            if (Environment.Is64BitOperatingSystem)
            {
                // If the total system memory is less than 2GB:
                if (totalMemoryInKilobytes / 1024 / MemoryAllocationRatio < 2)
                {
                    // Allocate the minimum memory to the JVM.
                    javaMemoryAllocation = MinimumMemoryAllocation;
                }
                else
                {
                    // Calculate the memory allocation for the JVM.
                    javaMemoryAllocation = totalMemoryInKilobytes / 1024 / MemoryAllocationRatio;
                }
            }

            // Return the calculated memory allocation as an integer.
            return Convert.ToInt32(javaMemoryAllocation);
        }
    }

}
