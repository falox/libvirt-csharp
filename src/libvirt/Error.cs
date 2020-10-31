using System;
using System.Runtime.InteropServices;

namespace libvirt
{
    /// <summary>
    /// the virError object
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class Error
    {
        /// <summary>
        /// The error code, a virErrorNumber.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public ErrorNumber code;
        /// <summary>
        /// What part of the library raised this error.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int domain;
        /// <summary>
        /// Human-readable informative error message.
        /// </summary>
        private IntPtr message;
        /// <summary>
        /// Human-readable informative error message.
        /// </summary>
        public string Message
        {
            get { return Marshal.PtrToStringAnsi(message); }
        }

        /// <summary>
        /// How consequent is the error.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public ErrorLevel level;
        /// <summary>
        /// Connection if available, deprecated see note above.
        /// </summary>
        public IntPtr conn;
        /// <summary>
        /// Domain if available, deprecated see note above.
        /// </summary>
        public IntPtr dom;
        /// <summary>
        /// Extra string information.
        /// </summary>
        private IntPtr str1;
        /// <summary>
        /// Extra string information.
        /// </summary>
        public string Str1 { get { return Marshal.PtrToStringAnsi(str1); } }
        /// <summary>
        /// Extra string information.
        /// </summary>
        [MarshalAs(UnmanagedType.SysInt)]
        private IntPtr str2;
        /// <summary>
        /// Extra string information.
        /// </summary>
        public string Str2 { get { return Marshal.PtrToStringAnsi(str2); } }
        /// <summary>
        /// Extra string information.
        /// </summary>
        private IntPtr str3;
        /// <summary>
        /// Extra string information.
        /// </summary>
        public string Str3 { get { return Marshal.PtrToStringAnsi(str3); } }
        /// <summary>
        /// Extra number information.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int int1;
        /// <summary>
        /// Extra number information.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int int2;
        /// <summary>
        /// Network if available, deprecated see note above.
        /// </summary>
        public IntPtr net;
    }
}