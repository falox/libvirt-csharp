using System;
using Xunit;
using libvirt;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace libvirt.Tests
{
    public class ConnectTests
    {
        private const string URI_VALID = "test:///default";
        private const string URI_INVALID = "wrong";
        private Connect _conn;

        public ConnectTests()
        {
            _conn = new Connect(URI_VALID);
        }

        ~ConnectTests()
        {
            _conn.Close();
        }

        [Fact]
        public void Connect_InitialState()
        {
            Assert.Equal(URI_VALID, _conn.Uri);
            Assert.False(_conn.IsOpen);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Open_OpensConnection(bool readOnly)
        {
            // Act
            _conn.Open(readOnly);

            // Assert
            Assert.True(_conn.IsOpen);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Open_ThrowsExceptionIfUriIsInvalid(bool readOnly)
        {
            // Arrange
            Connect conn = new Connect(URI_INVALID);
            
            // Act
            Action action = () => conn.Open(readOnly);

            // Assert
            Assert.Throws<LibvirtException>(action);
            Assert.False(conn.IsOpen);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Close_ClosesConnection(bool readOnly)
        {
            // Arrange
            _conn.Open(readOnly);

            // Act
            _conn.Close();

            // Assert
            Assert.False(_conn.IsOpen);
        }

        [Fact]
        public void Close_IsIgnoredIfNotOpen()
        {
            // Act
            _conn.Close();

            // Assert
            Assert.False(_conn.IsOpen);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Dispose_ClosesConnection(bool readOnly)
        {
            // Arrange
            _conn.Open(readOnly);

            // Act
            _conn.Dispose();

            // Assert
            Assert.False(_conn.IsOpen);
        }

        [Fact]
        public void Dispose_ClosesConnectionOnlyIfOpen()
        {
            // Act
            _conn.Dispose();

            // Assert
            Assert.False(_conn.IsOpen);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ImplicitDispose_ClosesConnection(bool readOnly)
        {
            Connect conn = null;
            using (conn = new Connect(URI_VALID))
            {
                conn.Open(readOnly);
            } // Implicit Dispose()
            
            Assert.False(conn.IsOpen);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDomains_ReturnsDomains(bool readOnly)
        {
            // Arrange
            _conn.Open(readOnly);

            // Act
            var domains = _conn.GetDomains();

            // Assert
            Assert.NotEmpty(domains);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDomains_ReturnsEmptyListIfNoDomainsAreFound(bool readOnly)
        {
            // Arrange
            _conn.Open(readOnly);

            // Act
            var domains = _conn.GetDomains(virConnectListAllDomainsFlags.VIR_CONNECT_LIST_DOMAINS_INACTIVE);

            // Assert
            Assert.Empty(domains);
        }

        [Fact]
        public void CreateDomain_CreatesAndRunsDomain()
        {
            // Arrange
            _conn.Open();
            var xml = XDocument.Parse(_conn.GetDomains().Single(x => x.Id == 1).Xml);
            xml.Element("domain").Attribute("id").Remove();
            xml.Element("domain").Element("name").Value = "test-" + Guid.NewGuid();
            xml.Element("domain").Element("uuid").Remove();

            // Act
            var domain = _conn.CreateDomain(xml.ToString());

            // Assert
            Assert.NotEqual(-1, domain.Id);
        }

        
        [Fact]
        public void TestSaveAndRestoreDomain()
        {
            // Arrange
             _conn.Open();
            var xml = XDocument.Parse(_conn.GetDomains().Single(x => x.Id == 1).Xml);
            xml.Element("domain").Attribute("id").Remove();
            xml.Element("domain").Element("name").Value = "test-" + Guid.NewGuid();
            xml.Element("domain").Element("uuid").Remove();
            var domain = _conn.CreateDomain(xml.ToString());
            var expectedDomainUUID = domain.UUID;
            var file = Path.GetTempFileName();

            // Act
            domain.Save(file);
            _conn.RestoreDomain(file);
            
            // Assert
            var actualDomain = _conn.GetDomains(virConnectListAllDomainsFlags.VIR_CONNECT_LIST_DOMAINS_TRANSIENT).Single(x => x.UUID == expectedDomainUUID);
            Assert.Equal(-1, domain.Id);
            Assert.NotEqual(-1, actualDomain.Id);

            // Clean
            File.Delete(file);
        }

        [Fact]
        public void CreateDomain_ThrowsExceptionWithInvalidXML()
        {
            // Arrange
            _conn.Open();

            // Act
            Action action = () => _conn.CreateDomain("<domain />");

            // Assert
            Assert.Throws<LibvirtException>(action);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestConnectProperties(bool openAsReadOnly) 
        {
            TestStringProperty(openAsReadOnly,
                x => x.Hostname,
                x => x.Capabilities,
                x => x.Type
            );
        }

        private void TestStringProperty(bool openAsReadonly, params Func<Connect, string>[] funcs)
        {
            // Property is null if the connection is closed
            foreach (var func in funcs)
            {
                Assert.Null(func(_conn));
            }

            // Property is not null if the connection is closed
            _conn.Open(openAsReadonly);
            foreach (var func in funcs)
            {
                string value = func(_conn);
                Assert.NotNull(value);
            }
        }
    }
}