using System;
using System.Runtime.InteropServices;

namespace libvirt
{
    /// <summary>
    /// Libvirt base object
    /// </summary>
    public class LibvirtObject : Disposable
    {
        protected void EnsureObjectIsNotDisposed()
        {
            if (IsDisposed || IsDisposing)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        protected string GetString(Func<string> func)
        {
            EnsureObjectIsNotDisposed();

            string result = func();

            ThrowExceptionOnError(result);

            return result;
        }

        protected string GetUUID(Func<char[], int> func)
        {
            EnsureObjectIsNotDisposed();

            char[] uuid = new char[Libvirt.VIR_UUID_BUFLEN];
            
            var result = func(uuid);

            ThrowExceptionOnError(result);

            return new string(uuid);
        }

        protected int GetInt32(Func<int> func)
        {
            EnsureObjectIsNotDisposed();

            int result = func();

            ThrowExceptionOnError(result);

            return result;
        }

        protected void ThrowExceptionOnError(int result) =>  ThrowExceptionOn(() => result == -1);

        protected void ThrowExceptionOnError(IntPtr result) => ThrowExceptionOn(() => result == IntPtr.Zero);

        protected void ThrowExceptionOnError(string result) => ThrowExceptionOn(() => result == null);

        private void ThrowExceptionOn(Func<bool> predicate)
        {
            if (predicate())
            {
                var error = GetLastError();

                if (error != null && error.level == virErrorLevel.VIR_ERR_ERROR)
                {
                    throw new LibvirtException(error);
                }
            }
        }

        private virError GetLastError()
        {
            IntPtr errPtr = Libvirt.virGetLastError();

            if (errPtr == IntPtr.Zero) 
            {
                return null;
            }

            return (virError) Marshal.PtrToStructure(errPtr, typeof(virError));
        }
    }
}