using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace libvirt
{
    /// <summary>
    /// Represents a Connect object
    /// </summary>
    public class Connect : LibvirtObject
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

        public IEnumerable<Domain> GetDomains(virConnectListAllDomainsFlags flags = default(virConnectListAllDomainsFlags))
        {
            int result = Libvirt.virConnectListAllDomains(_conn, out IntPtr[] ptrDomains, flags);

            ThrowExceptionOnError(result);

            if (result == 0)
            {
                return new Domain[0];
            }
            else
            {
                return ptrDomains
                    .Select(x => new Domain(_conn, x))
                    .ToList();
            }
        }

        public string Capabilities => GetString(() => Libvirt.virConnectGetCapabilities(_conn));

        public string Hostname => GetString(() => Libvirt.virConnectGetHostname(_conn));

        public string Type => GetString(() => Libvirt.virConnectGetType(_conn));

        private new string GetString(Func<string> func)
        {
            if (!IsOpen)
            {
                return null;
            }

            string result = func();

            ThrowExceptionOnError(result);

            return result;
        }

        protected override void DisposeInternal()
        {
            Close(ignoreErrors: true);
        }
    }
}