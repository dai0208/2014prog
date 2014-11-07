using System;
using System.Runtime.InteropServices;

namespace CpuCoreCount
{
    public class CpuCoreCount
    {
        [DllImport("kernel32")]
        static extern void GetSystemInfo(ref SYSTEM_INFO ptmpsi);

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }

        /// <summary>
        /// CPUCoreの数を返します
        /// </summary>
        /// <returns>CPUCoreの数</returns>
        static public int GetCpuCoreCount()
        {
            SYSTEM_INFO sysInfo = new SYSTEM_INFO();
            GetSystemInfo(ref sysInfo);
            return (int)sysInfo.dwNumberOfProcessors;
        }
    }
}
