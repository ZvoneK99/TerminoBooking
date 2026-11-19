using TerminoBooking.Domain;

namespace TerminoBooking.UnitTests
{
    public class StaffMemberTests
    {
        [Fact]
        public void Constructor_WithValidName_CreatesStaffMember()
        {
            // Arrange
            var name = "Ivan Horvat";

            // Act
            var staffMember = new StaffMember(name);

            // Assert
            Assert.Equal(name, staffMember.Name);
            Assert.NotEqual(Guid.Empty, staffMember.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_Throws(string? name)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StaffMember(name!));
        }
    }
}
