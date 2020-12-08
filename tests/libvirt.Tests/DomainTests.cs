using System;
using Xunit;
using libvirt;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Xunit.Abstractions;

namespace libvirt.Tests
{
    public class DomainTests
    {
        private const string URI_VALID = "test:///default";
        private Connect _conn;
        private Domain _domain;

        private readonly ITestOutputHelper _testOutputHelper;

        public DomainTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            _conn = new Connect(URI_VALID);
            _conn.Open();
            var xml = XDocument.Parse(_conn.GetDomains().Single(x => x.Id == 1).Xml);
            xml.Element("domain").Attribute("id").Remove();
            xml.Element("domain").Element("name").Value = "test-" + Guid.NewGuid();
            xml.Element("domain").Element("uuid").Remove();
            _domain = _conn.CreateDomain(xml.ToString());
        }

        ~DomainTests()
        {
            _conn.Close();
        }

        [Fact]
        public void TestDomainProperties()
        {
            var domain = _conn.GetDomains().SingleOrDefault(x => x.Id == 1);
            Assert.Equal(1, domain.Id);
            Assert.Equal("test", domain.Name);
            Assert.Equal(Guid.Parse("6695eb01-f6a4-8304-79aa-97f2502e193f"), domain.UUID);
            Assert.Equal("linux", domain.OSType);
            Assert.Equal(virDomainState.VIR_DOMAIN_RUNNING, domain.Info.State);
            Assert.Equal(2, domain.Info.nrVirtCpu);
        }

        [Fact]
        public void TestDispose()
        {
            _domain.Dispose();

            Assert.Throws<ObjectDisposedException>(() => _domain.Id);
            Assert.Throws<ObjectDisposedException>(() => _domain.Name);
            Assert.Throws<ObjectDisposedException>(() => _domain.UUID);
            Assert.Throws<ObjectDisposedException>(() => _domain.OSType);
            Assert.Throws<ObjectDisposedException>(() => _domain.Info);
        }

        [Fact]
        public void TestDestroy()
        {
            _domain.Destroy();
            Assert.Equal(-1, _domain.Id);
        }

        [Fact]
        public void TestShutdown()
        {
            _domain.Shutdown();
            Assert.Equal(-1, _domain.Id);            
        }

        [Fact]
        public void TestReboot()
        {
            _domain.Reboot();
            Assert.Equal(virDomainState.VIR_DOMAIN_RUNNING, _domain.Info.State);
        }

        [Fact]
        public void TestSuspendAndResume()
        {
            _domain.Suspend();
            Assert.Equal(virDomainState.VIR_DOMAIN_PAUSED, _domain.Info.State);

            _domain.Resume();
            Assert.Equal(virDomainState.VIR_DOMAIN_RUNNING, _domain.Info.State);
        }

        [Fact]
        public void TestCannotResumeIfNotSuspended()
        {
            Assert.Throws<LibvirtException>(() => _domain.Resume());
        }
    }
}