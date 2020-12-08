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

        protected virtual string GetString(Func<string> func)
        {
            EnsureObjectIsNotDisposed();

            string result = func();

            ThrowExceptionOnError(result);

            return result;
        }

        protected Guid GetUUID(Func<IntPtr, int> func)
        {
            EnsureObjectIsNotDisposed();

            //Can not directly cast for different endian.

            IntPtr uuidStringBuffer = Marshal.AllocHGlobal(Libvirt.VIR_UUID_STRING_BUFLEN);

            int result = func(uuidStringBuffer);
            ThrowExceptionOnError(result);

            string uuidString = Marshal.PtrToStringUTF8(uuidStringBuffer);

            Marshal.FreeHGlobal(uuidStringBuffer);

            if (uuidString is null)
            {
                return default;
            }

            return Guid.Parse(uuidString);
        }

        protected int GetInt32(Func<int> func)
        {
            EnsureObjectIsNotDisposed();

            int result = func();

            ThrowExceptionOnError(result);

            return result;
        }

        protected void ThrowExceptionOnError(int result) => LibvirtHelper.ThrowExceptionOnError(result);

        protected void ThrowExceptionOnError(IntPtr result) => LibvirtHelper.ThrowExceptionOnError(result);

        protected void ThrowExceptionOnError(string result) => LibvirtHelper.ThrowExceptionOnError(result);

    }
}