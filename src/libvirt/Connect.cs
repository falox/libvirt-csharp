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

        public bool IsOpen { get; private set; }

        public void Open(bool readOnly = false)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("Connect", "Cannot open a disposed connection.");
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

        public List<Domain> GetDomains(virConnectListAllDomainsFlags flags = default)
        {
            int result = Libvirt.virConnectListAllDomains(_conn, out IntPtr ptrDomains, flags);

            ThrowExceptionOnError(result);

            List<Domain> domains = new List<Domain>();

            for (int i = 0; i < result; i++)
            {
                IntPtr ptrDomain = Marshal.ReadIntPtr(ptrDomains, i * IntPtr.Size);
                domains.Add(new Domain(_conn, ptrDomain));
            }

            Libvirt.virFree(ptrDomains);

            return domains;
        }

        public Domain CreateDomain(string xml)
        {
            IntPtr result = Libvirt.virDomainCreateXML(_conn, xml);

            ThrowExceptionOnError(result);

            return new Domain(_conn, result);
        }

        public void RestoreDomain(string file) => ThrowExceptionOnError(Libvirt.virDomainRestore(_conn, file));

        public string Capabilities => GetString(() => Libvirt.virConnectGetCapabilities(_conn));

        public string Hostname => GetString(() => Libvirt.virConnectGetHostname(_conn));

        public string Type => GetString(() => Libvirt.virConnectGetType(_conn));

        protected override string GetString(Func<string> func)
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