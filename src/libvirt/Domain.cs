using System;

namespace libvirt
{
    /// <summary>
    /// Represents a Domain object
    /// </summary>
    public class Domain : LibvirtObject
    {
        private readonly IntPtr _ptrConnect;
        private readonly IntPtr _ptrDomain;

        internal Domain(IntPtr ptrConnect, IntPtr ptrDomain)
        {
            if (ptrConnect == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ptrConnect));
            }

            if (ptrDomain == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ptrDomain));
            }

            _ptrConnect = ptrConnect;
            _ptrDomain = ptrDomain;
        }

        public int Id => GetInt32(() => Libvirt.virDomainGetID(_ptrDomain));

        public string Name => GetString(() => Libvirt.virDomainGetName(_ptrDomain));

        protected override void DisposeInternal()
        {
            Libvirt.virDomainFree(_ptrDomain);
        }
    }
}