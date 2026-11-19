using TerminoBooking.Domain;

namespace TerminoBooking.UnitTests
{
    public class ClientTests
    {
        [Fact]
        public void Constructor_WithValidData_CreatesClient()
        {
            // Act
            var client = new Client("Ana Kovač", "ana@example.com", "091 123 4567");

            // Assert
            Assert.Equal("Ana Kovač", client.Name);
            Assert.Equal("ana@example.com", client.Email);
            Assert.Equal("091 123 4567", client.Telephone);
            Assert.NotEqual(Guid.Empty, client.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_Throws(string? name)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Client(name!, "ana@example.com", null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("ana")]
        [InlineData("ana@")]
        [InlineData("Ana Kovač <ana@example.com>")]
        public void Constructor_WithInvalidEmail_Throws(string? email)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Client("Ana Kovač", email!, null));
        }

        [Fact]
        public void Constructor_TrimsSpacesFromNameAndEmail()
        {
            // Act
            var client = new Client("  Ana Kovač  ", "  ana@example.com  ", null);

            // Assert
            Assert.Equal("Ana Kovač", client.Name);
            Assert.Equal("ana@example.com", client.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithEmptyTelephone_StoresNull(string? telephone)
        {
            // Act
            var client = new Client("Ana Kovač", "ana@example.com", telephone);

            // Assert
            Assert.Null(client.Telephone);
        }

        [Fact]
        public void Constructor_TrimsSpacesFromTelephone()
        {
            // Act
            var client = new Client("Ana Kovač", "ana@example.com", "  091 123 4567  ");

            // Assert
            Assert.Equal("091 123 4567", client.Telephone);
        }
    }
}
