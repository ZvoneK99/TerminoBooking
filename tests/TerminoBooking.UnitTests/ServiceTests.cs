using TerminoBooking.Domain;

namespace TerminoBooking.UnitTests
{
    public class ServiceTests
    {
        [Fact]
        public void Constructor_WithValidData_CreatesService()
        {
            //Arrange
            var duration = TimeSpan.FromMinutes(45);

            //Act
            var service = new Service("Prvi pregled", duration, 50m);

            //Assert
            Assert.Equal("Prvi pregled", service.Name);
            Assert.Equal(duration, service.Duration);
            Assert.Equal(50m, service.Price);
            Assert.NotEqual(Guid.Empty, service.Id);
        }

        [Fact]
        public void Constructor_WithNegativePrice_Throws()
        {
            // Arrange
            var duration = TimeSpan.FromMinutes(45);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Service("Prvi pregled", duration, -10m));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_Throws(string? name)
        {
            // Arrange
            var duration = TimeSpan.FromMinutes(45);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Service(name!, duration, 50m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-15)]
        [InlineData(540)]
        public void Constructor_WithInvalidDuration_Throws(int minutes)
        {
            var duration = TimeSpan.FromMinutes(minutes);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Service("Prvi pregled", duration, 50m));
        }


    }
}
