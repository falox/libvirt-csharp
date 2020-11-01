using System;
using Xunit;
using libvirt;

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
            _conn.Open();

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
    }
}