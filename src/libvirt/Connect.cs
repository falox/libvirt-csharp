using System;
using static libvirt.ErrorManagement;

namespace libvirt
{
    /// <summary>
    /// Represents a Connect object
    /// </summary>
    public class Connect : Disposable
    {
        private IntPtr _conn;

        public Connect(string uri)
        {
            this.Uri = uri;
        }

        public string Uri { get; }

        public bool IsOpen { get; set; }

        public void Open(bool readOnly = false)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("Cannot open a disposed Connect");
            }

            if (readOnly)
            {
                _conn = Libvirt.virConnectOpenReadOnly(Uri);
            }
            else
            {
                _conn = Libvirt.virConnectOpen(Uri);
            }

            ThrowExceptionOnError(_conn);

            IsOpen = true;
        }

        public void Close(bool ignoreErrors = false)
        {
            if (!IsOpen)
            {
                return;
            }

            int result = Libvirt.virConnectClose(_conn);

            if (!ignoreErrors) 
            {
                ThrowExceptionOnError(result);
            }

            IsOpen = false;
        }

        protected override void DisposeInternal()
        {
            Close(ignoreErrors: true);
        }
    }
}