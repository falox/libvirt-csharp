using System;

namespace libvirt
{
    [Serializable]
    public class LibvirtException : Exception
    {
        public Error Error { get; }

        public LibvirtException(Error error) : base(error.Message)
        {
            this.Error = error;
        }
    }
}