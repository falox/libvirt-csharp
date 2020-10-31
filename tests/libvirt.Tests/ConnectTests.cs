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

        [Fact]
        public void Open_OpensConnection()
        {
            // Act
            _conn.Open();

            // Assert
            Assert.True(_conn.IsOpen);
        }

        [Fact]
        public void Open_ThrowsExceptionIfUriIsInvalid()
        {
            // Arrange
            Connect conn = new Connect(URI_INVALID);
            
            // Act
            Action action = () => conn.Open();

            // Assert
            Assert.Throws<LibvirtException>(action);
            Assert.False(conn.IsOpen);
        }

        [Fact]
        public void Close_ClosesConnection()
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

        [Fact]
        public void Dispose_ClosesConnection()
        {
            // Arrange
            _conn.Open();

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

        [Fact]
        public void ImplicitDispose_ClosesConnection()
        {
            Connect conn = null;
            using (conn = new Connect(URI_VALID))
            {
                conn.Open();
            } // Implicit Dispose()
            
            Assert.False(conn.IsOpen);
        }
    }
}