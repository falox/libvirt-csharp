using Xunit;

namespace libvirt.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void GetVersion_ReturnVersion()
        {
            Libvirt.GetVersion();
        }
    }
}