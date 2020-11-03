using System;
using Xunit;
using libvirt;
using System.Linq;

namespace libvirt.Tests
{
    public class DomainTests
    {
        private const string URI_VALID = "test:///default";
        private Connect _conn;
        private Domain _domain;

        public DomainTests()
        {
            _conn = new Connect(URI_VALID);
            _conn.Open();
            _domain =_conn.GetDomains().First();
        }

        ~DomainTests()
        {
            _conn.Close();
        }

        [Fact]
        public void TestDomainProperties()
        {
            Assert.Equal(1, _domain.Id);
            Assert.Equal("test", _domain.Name);
            Assert.Equal("6695eb01-f6a4-8304-79aa-97f2502e193f", _domain.UUID);
            Assert.Equal("linux", _domain.OSType);
        }

        [Fact]
        public void TestDispose()
        {
            _domain.Dispose();

            Assert.Throws<ObjectDisposedException>(() => _domain.Id);
            Assert.Throws<ObjectDisposedException>(() => _domain.Name);
            Assert.Throws<ObjectDisposedException>(() => _domain.UUID);
            Assert.Throws<ObjectDisposedException>(() => _domain.OSType);
        }
    }
}