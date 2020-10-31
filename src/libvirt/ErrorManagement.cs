using System;
using System.Runtime.InteropServices;

namespace libvirt 
{
    public static class ErrorManagement
    {
        public static void ThrowExceptionOnError(int result) =>  ThrowExceptionOn(() => result == -1);

        public static void ThrowExceptionOnError(IntPtr result) => ThrowExceptionOn(() => result == IntPtr.Zero);

        public static void ThrowExceptionOn(Func<bool> predicate)
        {
            if (predicate())
            {
                var error = GetLastError();

                if (error != null && error.level == ErrorLevel.VIR_ERR_ERROR)
                {
                    throw new LibvirtException(error);
                }
            }
        }

        private static Error GetLastError()
        {
            IntPtr errPtr = Libvirt.virGetLastError();

            if (errPtr == IntPtr.Zero) 
            {
                return null;
            }

            return (Error) Marshal.PtrToStructure(errPtr, typeof(Error));
        }
    }
}