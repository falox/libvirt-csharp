using System;

namespace libvirt
{
    [Serializable]
    public class LibvirtException : Exception
    {
        public virError Error { get; }

        public LibvirtException(virError error) : base(error.Message)
        {
            this.Error = error;
        }
    }
}