using System;
using System.Runtime.InteropServices;

namespace libvirt
{
    public static class LibvirtHelper
    {
        public static void ThrowExceptionOnError(int result) => ThrowExceptionOn(() => result == -1);

        public static void ThrowExceptionOnError(IntPtr result) => ThrowExceptionOn(() => result == IntPtr.Zero);

        public static void ThrowExceptionOnError(string result) => ThrowExceptionOn(() => result == null);

        private static void ThrowExceptionOn(Func<bool> predicate)
        {
            if (predicate())
            {
                virError error = GetLastError();

                if (error?.level == virErrorLevel.VIR_ERR_ERROR)
                {
                    throw new LibvirtException(error);
                }
            }
        }

        private static virError GetLastError()
        {
            IntPtr errPtr = Libvirt.virGetLastError();

            return Marshal.PtrToStructure<virError>(errPtr);
        }
    }
}