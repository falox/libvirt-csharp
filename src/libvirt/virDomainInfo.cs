using System;
using System.Runtime.InteropServices;

namespace libvirt
{
    /// <summary>
    /// Structure to handle domain informations
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class virDomainInfo
    {
        /// <summary>
        /// The running state, one of virDomainState.
        /// </summary>
        private byte state;
        /// <summary>
        /// The maximum memory in KBytes allowed.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr maxMem;
        /// <summary>
        /// The memory in KBytes used by the domain.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr memory;
        /// <summary>
        /// The number of virtual CPUs for the domain.
        /// </summary>
        public ushort nrVirtCpu;
        /// <summary>
        /// The CPU time used in nanoseconds.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr cpuTime;
        /// <summary>
        /// The running state, one of virDomainState.
        /// </summary>
        public virDomainState State { get { return (virDomainState)state; } }
    }
}