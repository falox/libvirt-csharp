using Xunit;

namespace libvirt.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void GetVersion_ReturnVersion()
        {
            // Act
            var version = Libvirt.Version;

            // Assert
            Assert.True(version > new System.Version(0,0,1));
        }
    }
}