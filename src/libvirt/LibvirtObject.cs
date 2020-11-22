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

        protected void ThrowExceptionOnError(int result) => LibvirtHelper.ThrowExceptionOnError(result);

        protected void ThrowExceptionOnError(IntPtr result) => LibvirtHelper.ThrowExceptionOnError(result);

        protected void ThrowExceptionOnError(string result) => LibvirtHelper.ThrowExceptionOnError(result);

    }
}